using Xunit;
using Chess.ViewModel;
using Chess.EventAggregatorHandler;
using ChessTests.Tests.Stubs;
using Chess.EventAggregatorMessages;
using System.Collections.Generic;

namespace ChessTests.Tests.ViewModelTests
{
    public class ControlMenuViewModelTests
    {
        [Fact]
        public void ControlMenuViewModelTests_CheckTheSpeedsOfTheSpeedSelector_ShouldReturnTrue()
        {
            var viewModel = new ControlMenuViewModel();

            Assert.Equal(new() { "0.25", "0.5", "0.75", "Normal", "1.25", "1.5", "1.75", "2" }, viewModel.ComboBoxItems);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckConnectClientButtonStatus_ShouldReturnFalse()
        {
            var viewModel = new ControlMenuViewModel();

            Assert.False(viewModel.IsDisabledConnectClientButton);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckConnectClientButtonStatusAfterConnection_ShouldReturnTrue()
        {
            var viewModel = new ControlMenuViewModel();
            viewModel.IsDisabledConnectClientButton = true;

            Assert.True(viewModel.IsDisabledConnectClientButton);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckConnectServerButtonStatus_ShouldReturnFalse()
        {
            var viewModel = new ControlMenuViewModel();

            Assert.False(viewModel.IsDisabledConnectServerButton);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckConnectServerButtonStatusAfterConnection_ShouldReturnTrue()
        {
            var viewModel = new ControlMenuViewModel();
            viewModel.IsDisabledConnectServerButton = true;

            Assert.True(viewModel.IsDisabledConnectServerButton);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckComboBoxDefaultImage_ShouldReturnTrue()
        {
            var viewModel = new ControlMenuViewModel();

            Assert.Equal("pack://application:,,,/Chess;component/Files/Buttons/CogButtonImage.png", viewModel.ComboBoxDefaultImage);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckTheDefaultStartStopButtonImage_ShouldReturnTrue()
        {
            var viewModel = new ControlMenuViewModel();

            Assert.Equal("pack://application:,,,/Chess;component/Files/Buttons/PlayButtonImage.png", viewModel.StartStopButtonImage);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckIfTheStartStopButtonImageWasChangedUpponButtonPressed_ShouldReturnTrue()
        {
            var viewModel = new ControlMenuViewModel();

            viewModel.StartStopPlayer.Execute(null);
            Assert.Equal("pack://application:,,,/Chess;component/Files/Buttons/StopButtonImage.png", viewModel.StartStopButtonImage);

            viewModel.StartStopPlayer.Execute(null);
            Assert.Equal("pack://application:,,,/Chess;component/Files/Buttons/PlayButtonImage.png", viewModel.StartStopButtonImage);

            viewModel.StartStopPlayer.Execute(null);
            Assert.Equal("pack://application:,,,/Chess;component/Files/Buttons/StopButtonImage.png", viewModel.StartStopButtonImage);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckTheDefaultComboBoxSpeed_ShouldReturnTrue()
        {
            var viewModel = new ControlMenuViewModel();
            Assert.Equal("Normal", viewModel.ComboBoxSpeed);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckIfTheComboBoxSpeedHasChanged_ShouldReturnTrue()
        {
            var viewModel = new ControlMenuViewModel { ComboBoxSpeed = "1" };
            Assert.Equal("1", viewModel.ComboBoxSpeed);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckIfTheNextMoveWasExecutedAfterTriggeringTheCommand_ShouldReturnTrue()
        {
            var eventAggregator = new EventAggregator();
            var controlViewModel = new ControlMenuViewModel(new StubFileBrowser(), eventAggregator);

            string fileName = string.Empty;
            IEnumerable<string> movesList = new List<string>();
            bool direction = false;

            eventAggregator.RegisterHandler<RelocationsFileChangingMessage>(message => { fileName = message.Message; });
            eventAggregator.RegisterHandler<InitRelocationsCollectionMessage>(message => { movesList = message.Message; });
            eventAggregator.RegisterHandler<LoadCurrentMoveMessage>(message => { direction = message.Forward; });
            
            controlViewModel.SelectFileCommand.Execute(null);
            Assert.Equal("TestFilePath", fileName);
            Assert.Equal(new List<string> { "e4 e6" }, movesList);

            controlViewModel.ExecuteNextRelocation.Execute(null);
            Assert.True(direction);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckIfThePreviousMoveWasExecutedAfterTriggeringTheCommand_ShouldReturnTrue()
        {
            var eventAggregator = new EventAggregator();
            var controlViewModel = new ControlMenuViewModel(new StubFileBrowser(), eventAggregator);
            string fileName = string.Empty;
            IEnumerable<string> movesList = new List<string>();
            bool direction = false;

            eventAggregator.RegisterHandler<RelocationsFileChangingMessage>(message => { fileName = message.Message; });
            eventAggregator.RegisterHandler<InitRelocationsCollectionMessage>(message => { movesList = message.Message; });
            eventAggregator.RegisterHandler<LoadCurrentMoveMessage>(message => { direction = message.Forward; });
            
            controlViewModel.SelectFileCommand.Execute(null);
            Assert.Equal("TestFilePath", fileName);
            Assert.Equal(new List<string> { "e4 e6" }, movesList);

            controlViewModel.ExecutePrevRelocation.Execute(null);
            Assert.False(direction);
        }

        [Fact]
        public void ControlMenuViewModelTests_CheckIfTheGameWasResetUpponResetCommandTrigger_ShouldReturnTrue()
        {
            var eventAggregator = new EventAggregator();
            var controlViewModel = new ControlMenuViewModel(new StubFileBrowser(), eventAggregator);

            bool isResetBoard = false;
            bool isResetGrid = false;

            eventAggregator.RegisterHandler<ResetGameBoardMessage>((message) => isResetBoard = true);
            eventAggregator.RegisterHandler<ResetRelocationsGridMessage>(message => isResetGrid = true);

            controlViewModel.ResetGame.Execute(null);
            Assert.True(isResetBoard);
            Assert.True(isResetGrid);
        }
    }
}
