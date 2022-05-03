using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using Chess.Models;
using Chess.Models.Pieces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Reflection;
using System.Resources;
using Chess.Network;

namespace Chess.ViewModel
{
    public class PlayerPiecesViewModel : ViewModelBase, IInterfaceActualizer
    {
        private const string PiecesImagesPath = "pack://application:,,,/Chess;component/Files/Pieces/";
        private const int BoardLength = 7;
        private readonly int[] piecesRows = new[] { 0, 1, BoardLength, BoardLength - 1 };
        private readonly IEventAggregator eventAggregator;
        private IEnumerable<Icon> piecesCollection;
        private IEnumerable<Icon> backupPiecesCollection;
        private Board board;
        private IReceiver receiver;

        public PlayerPiecesViewModel()
        {
            InitPieces();
            board = new ();
            eventAggregator = EventAggregatorSingleton.Instance;
            eventAggregator.RegisterHandler<ExecuteMoveMessage>(ExecuteMoveEventHandler);
            eventAggregator.RegisterHandler<ResetGameBoardMessage>(ResetGameEventHandler);
            eventAggregator.RegisterHandler<ConnectClientMessage>(ConnectClientEventHandler);
            eventAggregator.RegisterHandler<DisconnectClientMessage>(DisconnectClientEventHandler);
        }

        public IEnumerable<Icon> SourceCollection
        {
            get => piecesCollection;
            set
            {
                piecesCollection = value;
                OnPropertyChanged(nameof(SourceCollection));
            }
        }

        public void UpdateInterface(IEnumerable<IPiece> pieces)
        {
            var list = new List<Icon>();
            foreach (var piece in pieces)
            {
                foreach (var icon in backupPiecesCollection)
                {
                    if (piece.Id == icon.Id)
                    {
                        icon.Position = (Position)piece.Current;
                        list.Add(icon);
                        break;
                    }
                }
            }

            SourceCollection = new ObservableCollection<Icon>(list);
        }

        private void DisconnectClientEventHandler(DisconnectClientMessage message)
        {
            receiver.Stop();
        }

        private void ResetGameEventHandler(ResetGameBoardMessage message)
        {
            InitPieces();
            SourceCollection = new ObservableCollection<Icon>(piecesCollection);
            board = new ();
        }

        private void ExecuteMoveEventHandler(ExecuteMoveMessage message)
        {
            var success = board.ExecuteMove(message.Move, message.Index, message.Forward, out IEnumerable<IPiece> pieces);
            if (pieces == null || !success)
            {
                eventAggregator.SendMessage(new HighlightCurrentMoveMessage(success));
                eventAggregator.SendMessage(new GameOverMessage(success));
                return;
            }

            UpdateInterface(pieces);
            eventAggregator.SendMessage(new HighlightCurrentMoveMessage(success));
        }

        private void InitPieces()
        {
            ResourceManager resource = new ("Chess.Localization.strings", Assembly.GetExecutingAssembly());
            var list = new List<Icon>();
            int count = 1;
            foreach (var row in piecesRows)
            {
                for (int col = 0; col <= BoardLength; col++)
                {
                    var icon = $"{PiecesImagesPath}{resource.GetString($"{count}", CultureInfo.CurrentCulture)}";
                    list.Add(new Icon { IconPath = icon, Position = new (row, col), Id = $"{row}{col}" });
                    count++;
                }
            }

            piecesCollection = list;
            backupPiecesCollection = list;
        }

        private void ConnectClientEventHandler(ConnectClientMessage message)
        {
            receiver = new Receiver(this, board);
            receiver.ConnectTo(message.Ip);
        }
    }
}
