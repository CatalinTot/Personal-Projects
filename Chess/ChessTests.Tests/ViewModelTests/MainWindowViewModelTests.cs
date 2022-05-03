using Xunit;
using Chess.ViewModel;
using System.Windows;

namespace ChessTests.Tests.ViewModelTests
{
    public class MainWindowViewModelTests
    {
        [Fact]
        public void MainWindowViewModelTests_CheckUserInteractionMessage_ShouldReturnTrue()
        {
            var viewModel = new MainWindowViewModel();

            Assert.Equal("Ready for commands!", viewModel.UserInteractions);
        }

        [Fact]
        public void MainWindowViewModelTests_CheckIfTheUserInteractionMessageHasChanged_ShouldReturnTrue()
        {
            var viewModel = new MainWindowViewModel();
            viewModel.UserInteractions = "Messaged Changed!";

            Assert.Equal("Messaged Changed!", viewModel.UserInteractions);
        }

        [Fact]
        public void MainWindowViewModelTests_CheckDefaultBufferVisibility_ShouldReturnTrue()
        {
            var viewModel = new MainWindowViewModel();

            Assert.Equal(Visibility.Hidden, viewModel.BufferVisibilty);
        }

        [Fact]
        public void MainWindowViewModelTests_CheckIfTheBufferVisibilityHasChanged_ShouldReturnTrue()
        {
            var viewModel = new MainWindowViewModel();
            viewModel.BufferVisibilty = Visibility.Hidden;

            Assert.Equal(Visibility.Hidden, viewModel.BufferVisibilty);
        }

        [Fact]
        public void MainWindowViewModelTests_CheckTheControlMenuAvailability_ShouldReturnTrue()
        {
            var viewModel = new MainWindowViewModel();

            Assert.True(viewModel.ControlMenuAvailability);
        }

        [Fact]
        public void MainWindowViewModelTests_CheckTheDefaultControlMenuAvailabilityHasChanged_ShouldReturnFalse()
        {
            var viewModel = new MainWindowViewModel();
            viewModel.ControlMenuAvailability = false;

            Assert.False(viewModel.ControlMenuAvailability);
        }

        [Fact]
        public void MainWindowViewModelTests_CheckTheDefaultFilePath_ShouldReturnTrue()
        {
            var viewModel = new MainWindowViewModel();

            Assert.Null(viewModel.CurrentFile);
        }
    

        [Fact]
        public void MainWindowViewModelTests_CheckTheFilePathHasChanged_ShouldReturnTrue()
        {
            var viewModel = new MainWindowViewModel();
            viewModel.CurrentFile = "TestFilePath"; 

            Assert.Equal("TestFilePath", viewModel.CurrentFile);
        }
    }
}
