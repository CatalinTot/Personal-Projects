using Chess.EventAggregatorHandler;
using Chess.EventAggregatorMessages;
using System.Windows;
using System.Windows.Input;

namespace Chess.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ICommand closeCommand;
        private string currentFile;
        private string userInteractions = "Ready for commands!";
        private Visibility bufferVisibility = Visibility.Hidden;
        private bool controlMenuAvailability = true;

        public MainWindowViewModel()
        {
            IEventAggregator eventAggregator = EventAggregatorSingleton.Instance;
            eventAggregator.RegisterHandler<RelocationsFileChangingMessage>(FilenameChangedMessageHandler);
            eventAggregator.RegisterHandler<UpdatesMessage>(UpdateMessagesHandler);
        }

        public ICommand CloseAppCommand
        {
            get
            {
                closeCommand ??= new RelayCommand(param => CloseApp(param));
                return closeCommand;
            }
        }

        public string UserInteractions
        {
            get => userInteractions;
            set
            {
                userInteractions = value;
                OnPropertyChanged(nameof(UserInteractions));
            }
        }

        public Visibility BufferVisibilty
        {
            get => bufferVisibility;
            set
            {
                bufferVisibility = value;
                OnPropertyChanged(nameof(BufferVisibilty));
            }
        }

        public bool ControlMenuAvailability
        {
            get => controlMenuAvailability;
            set
            {
                controlMenuAvailability = value;
                OnPropertyChanged(nameof(ControlMenuAvailability));
            }
        }

        public string CurrentFile
        {
            get => currentFile;
            set
            {
                currentFile = value;
                OnPropertyChanged(nameof(CurrentFile));
            }
        }

        private void UpdateMessagesHandler(UpdatesMessage message)
        {
            UserInteractions = message.Message;
            BufferVisibilty = message.BufferVisibility;
            ControlMenuAvailability = message.Availability;
        }

        private void CloseApp(object obj)
        {
            if (obj is not Window window)
            {
                return;
            }

            window.Close();
        }

        private void FilenameChangedMessageHandler(RelocationsFileChangingMessage message)
        {
            CurrentFile = message.Message;
        }
    }
}
