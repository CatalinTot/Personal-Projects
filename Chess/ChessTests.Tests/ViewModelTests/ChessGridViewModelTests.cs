using Xunit;
using Chess.ViewModel;

namespace ChessTests.Tests.ViewModelTests
{
    public class ChessGridViewModelTests
    {
        [Fact]
        public void ChessGridViewModelTests_CheckIfTheGridWasCreated_ShouldReturnTrue()
        {
            var viewModel = new ChessGridViewModel();
            Assert.Equal(64, viewModel.SourceCollection.Count);
        }
        
        [Fact]
        public void ChessGridViewModelTests_CheckThePropertiesOfAnEvenGridField_ShouldReturnTrue()
        {
            var viewModel = new ChessGridViewModel();
            var gridSquare = viewModel.SourceCollection[0];

            Assert.Equal(0, gridSquare.Position.Rank);
            Assert.Equal(0, gridSquare.Position.File);
            Assert.Equal("#FFCE9E", gridSquare.Color);
        }
        
        [Fact]
        public void ChessGridViewModelTests_CheckThePropertiesOfAnOddGridField_ShouldReturnTrue()
        {
            var viewModel = new ChessGridViewModel();
            var gridSquare = viewModel.SourceCollection[1];
            Assert.Equal(0, gridSquare.Position.Rank);
            Assert.Equal(1, gridSquare.Position.File);
            Assert.Equal("#D18B47", gridSquare.Color);
        }
    }
}
