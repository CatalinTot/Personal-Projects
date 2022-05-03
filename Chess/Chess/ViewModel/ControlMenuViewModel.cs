using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;
using System.Windows.Threading;

namespace Chess.ViewModel
{
    public class ControlMenuViewModel : ViewModelBase
    {
        const string ButtonsImagesPath = "pack://application:,,,/Chess;component/Files/Buttons/";
        private readonly List<string> comboBoxItems;
        private readonly IFileBrowser fileBrowser;
        private readonly IEventAggregator eventAggregator;
        private DispatcherTimer timer;
        private string startStopButtonImage;
        private string selectedComboBoxSpeed;
        private bool isHaltCommand;
        private bool isSelectedFile;
        private bool isReachedLowerLimit;
        private bool isReachedUpperLimit;
        private bool isDisabledConnectServerButton;
        private bool isDisabledConnectClientButton;
        private ICommand selectFileCommand;
        private ICommand executeNextRelocation;
        private ICommand executePrevRelocation;
        private ICommand startStopPlayer;
        private ICommand resetGame;

        public ControlMenuViewModel() : this(new FileBrowser(), EventAggregatorSingleton.Instance)
        {
        }

        public ControlMenuViewModel(IFileBrowser fileBrowser, IEventAggregator eventAggregator)
        {
            this.fileBrowser = fileBrowser;
            isReachedLowerLimit = true;
            this.eventAggregator = eventAggregator;
            eventAggregator.RegisterHandler<EndOfMovesMessage>(UpdatePlayerLimitsStatus);
            eventAggregator.RegisterHandler<HaltCommandMessage>(StopCommandHandler);
            eventAggregator.RegisterHandler<DisableConnectClientButtonMessage>(UpdateClientConnectionStatus);
            eventAggregator.RegisterHandler<DisableConnectServerButtonMessage>(UpdateServerConnectionStatus);
            startStopButtonImage = $"{ButtonsImagesPath}PlayButtonImage.png";
            selectedComboBoxSpeed = "Normal";
            comboBoxItems = new ()
            {
                "0.25",
                "0.5",
                "0.75",
                "Normal",
                "1.25",
                "1.5",
                "1.75",
                "2"
            };
        }

        public bool IsDisabledConnectServerButton
        {
            get => isDisabledConnectServerButton;
            set
            {
                isDisabledConnectServerButton = value;
                OnPropertyChanged(nameof(IsDisabledConnectServerButton));
            }
        }

        public bool IsDisabledConnectClientButton
        {
            get => isDisabledConnectClientButton;
            set
            {
                isDisabledConnectClientButton = value;
                OnPropertyChanged(nameof(IsDisabledConnectClientButton));
            }
        }

        public List<string> ComboBoxItems => comboBoxItems;

        public string ComboBoxDefaultImage => $"{ButtonsImagesPath}CogButtonImage.png";

        public string StartStopButtonImage
        {
            get => startStopButtonImage;
            set
            {
                startStopButtonImage = value;
                OnPropertyChanged(nameof(StartStopButtonImage));
            }
        }

        public string ComboBoxSpeed
        {
            get => selectedComboBoxSpeed;
            set
            {
                selectedComboBoxSpeed = value;
                SetPlaySpeed();
                OnPropertyChanged(nameof(ComboBoxSpeed));
            }
        }

        public ICommand SelectFileCommand
        {
            get
            {
                selectFileCommand ??= new RelayCommand(param => SelectFile());
                return selectFileCommand;
            }
        }

        public ICommand ExecuteNextRelocation
        {
            get
            {
                executeNextRelocation ??= new RelayCommand(param => ExecuteNext());
                return executeNextRelocation;
            }
        }

        public ICommand ExecutePrevRelocation
        {
            get
            {
                executePrevRelocation ??= new RelayCommand(param => ExecutePrev());
                return executePrevRelocation;
            }
        }

        public ICommand StartStopPlayer
        {
            get
            {
                startStopPlayer ??= new RelayCommand(param => Start());
                return startStopPlayer;
            }

            set
            {
                startStopPlayer = value;
                OnPropertyChanged(nameof(StartStopPlayer));
            }
        }

        public ICommand ResetGame
        {
            get
            {
                resetGame ??= new RelayCommand(param => Reset());
                return resetGame;
            }
        }

        private void UpdateServerConnectionStatus(DisableConnectServerButtonMessage message)
        {
            IsDisabledConnectServerButton = message.IsConnected;
        }

        private void UpdateClientConnectionStatus(DisableConnectClientButtonMessage message)
        {
            IsDisabledConnectClientButton = message.IsConnected;
        }

        private void StopCommandHandler(HaltCommandMessage obj)
        {
            isHaltCommand = true;
        }

        private void UpdatePlayerLimitsStatus(EndOfMovesMessage message)
        {
            isReachedLowerLimit = message.IsReachedLowerLimit;
            isReachedUpperLimit = message.IsReachedUpperLimit;
        }

        private void Reset()
        {
            if (isHaltCommand)
            {
                return;
            }

            eventAggregator.SendMessage(new ResetGameBoardMessage());
            eventAggregator.SendMessage(new ResetRelocationsGridMessage());
        }

        private void Stop()
        {
            if (isHaltCommand)
            {
                return;
            }

            StartStopButtonImage = $"{ButtonsImagesPath}PlayButtonImage.png";
            StartStopPlayer = new RelayCommand(param => Start());
            timer.Stop();
        }

        private void Start()
        {
            if (isHaltCommand)
            {
                return;
            }

            StartStopPlayer = new RelayCommand(param => Stop());
            StartStopButtonImage = $"{ButtonsImagesPath}StopButtonImage.png";
            timer = new DispatcherTimer();
            timer.Tick += DispatcherTimerTick;
            SetPlaySpeed();
            timer.Start();
        }

        private void SetPlaySpeed()
        {
            const int baseSpeed = 1000;
            if (Equals(timer, null) || isHaltCommand)
            {
                return;
            }

            timer.Interval = double.TryParse(selectedComboBoxSpeed, out double speed) ?
                             new (0, 0, 0, 0, (int)(1 / speed * baseSpeed)) :
                             new (0, 0, 0, 0, baseSpeed);
        }

        private void DispatcherTimerTick(object sender, EventArgs e)
        {
            ExecuteNext();
        }

        private void ExecuteNext()
        {
            if (!isSelectedFile || isReachedUpperLimit || isHaltCommand)
            {
                return;
            }

            eventAggregator.SendMessage(new LoadCurrentMoveMessage(true));
        }

        private void ExecutePrev()
        {
            if (!isSelectedFile || isReachedLowerLimit || isHaltCommand)
            {
                return;
            }

            eventAggregator.SendMessage(new LoadCurrentMoveMessage(false));
        }

        private void SelectFile()
        {
            if (isHaltCommand)
            {
                return;
            }

            var fileName = fileBrowser.ReadFromFile(out IEnumerable<string> content);
            if (fileName == null || content == null)
            {
                return;
            }

            isSelectedFile = true;
            eventAggregator.SendMessage(new RelocationsFileChangingMessage(fileName));
            eventAggregator.SendMessage(new InitRelocationsCollectionMessage(content.ToArray()));
        }
    }
}
