using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Network;

namespace Chess.ViewModel
{
    public class RelocationsGridViewModel : ViewModelBase
    {
        private const string DefaultBorderBrush = "Transparent";
        private readonly IEventAggregator eventAggregator;
        private ISender sender;
        private IEnumerable<RelocationSet> relocationsCollection;
        private int scrollOffset;
        private int currentMoveIndex;
        private int lastMoveIndex;

        public RelocationsGridViewModel()
        {
            currentMoveIndex = -1;
            lastMoveIndex = -1;
            eventAggregator = EventAggregatorSingleton.Instance;
            eventAggregator.RegisterHandler<InitRelocationsCollectionMessage>(InitRelocationsCollectionEventHandler);
            eventAggregator.RegisterHandler<LoadCurrentMoveMessage>(LoadNextRelocationEventHandler);
            eventAggregator.RegisterHandler<HighlightCurrentMoveMessage>(HighlightCurrentMoveEventHandler);
            eventAggregator.RegisterHandler<ResetRelocationsGridMessage>(ResetRelocationsGridEventHandler);
            eventAggregator.RegisterHandler<ConnectServerMessage>(ConnectServerHandler);
            eventAggregator.RegisterHandler<StopServerMessage>(StopServerEventHandler);
        }

        public IEnumerable<RelocationSet> RelocationsCollection
        {
            get => relocationsCollection;
            set
            {
                relocationsCollection = value;
                OnPropertyChanged(nameof(RelocationsCollection));
            }
        }

        public int RelocationsScrollOffset
        {
            get => scrollOffset;
            set
            {
                scrollOffset = value;
                OnPropertyChanged(nameof(RelocationsScrollOffset));
            }
        }

        private void StopServerEventHandler(StopServerMessage message)
        {
            sender.Stop();
        }

        private void ResetRelocationsGridEventHandler(ResetRelocationsGridMessage message)
        {
            currentMoveIndex = -1;
            RelocationsScrollOffset = 0;
            if (RelocationsCollection == null)
            {
                return;
            }

            RemoveHighlight();
            RelocationsCollection = new List<RelocationSet>(RelocationsCollection);
        }

        private void HighlightCurrentMoveEventHandler(HighlightCurrentMoveMessage message)
        {
            if (currentMoveIndex == -1)
            {
                RemoveHighlight();
                return;
            }

            RemoveHighlight();
            AddHighlight(message.Status ? "Green" : "Red");
        }

        private void ConnectServerHandler(ConnectServerMessage message)
        {
            sender = new Sender(message.Ip);
            sender.AcceptClientsAsync();
        }

        private void LoadNextRelocationEventHandler(LoadCurrentMoveMessage message)
        {
            var moves = RelocationsCollection.ToList();
            currentMoveIndex = message.Forward ? currentMoveIndex + 1 : currentMoveIndex - 1;
            if (IsReachedLimit())
            {
                return;
            }

            var move = currentMoveIndex % (1 + 1) == 0 ? moves[currentMoveIndex / (1 + 1)].White : moves[currentMoveIndex / (1 + 1)].Black;
            sender.Broadcast(move, currentMoveIndex, message.Forward);
            eventAggregator.SendMessage(new ExecuteMoveMessage(move, currentMoveIndex, message.Forward));
        }

        private void InitRelocationsCollectionEventHandler(InitRelocationsCollectionMessage message)
        {
            var relocationslist = new List<RelocationSet>();
            foreach (var line in message.Message)
            {
                var moves = line.Split(" ");
                relocationslist.Add(new RelocationSet
                {
                    White = moves[0],
                    Black = moves.Length > 1 ? moves[1] : string.Empty
                });
            }

            RelocationsCollection = relocationslist;
        }

        private void AddHighlight(string color)
        {
            var (index, modulo) = Math.DivRem(currentMoveIndex, 1 + 1);
            if (modulo == 0)
            {
                RelocationsCollection.ElementAt(index).WhiteBorderBrush = color;
            }
            else
            {
                RelocationsCollection.ElementAt(index).BlackBorderBrush = color;
            }

            RelocationsCollection = new List<RelocationSet>(RelocationsCollection);
            UpdateScrollOfsset();
        }

        private void RemoveHighlight()
        {
            foreach (var relocation in RelocationsCollection)
            {
                relocation.BlackBorderBrush = DefaultBorderBrush;
                relocation.WhiteBorderBrush = DefaultBorderBrush;
            }
        }

        private void UpdateScrollOfsset()
        {
            const int offset = 10;
            if (currentMoveIndex > lastMoveIndex)
            {
                RelocationsScrollOffset += offset;
            }
            else
            {
                RelocationsScrollOffset -= offset + offset / (1 + 1);
            }

            lastMoveIndex = currentMoveIndex;
        }

        private bool IsReachedLimit()
        {
            var upperLimit = RelocationsCollection.Count();
            var (quotient, reminder) = Math.DivRem(currentMoveIndex, 1 + 1);
            var isReachedUpperLimit = quotient + reminder >= upperLimit;
            var isReachedLowerLimit = quotient <= -1;
            if (isReachedUpperLimit || isReachedLowerLimit)
            {
                eventAggregator.SendMessage(new EndOfMovesMessage(isReachedLowerLimit, isReachedUpperLimit));
                return true;
            }

            eventAggregator.SendMessage(new EndOfMovesMessage(false, false));
            return false;
        }
    }
}
