using Xunit;
using Chess.ViewModel;
using System.Linq;
using System.Collections.Generic;

namespace ChessTests.Tests.ViewModelTests
{
    public class RelocationsGridViewModelTests
    {
        [Fact]
        public void RelocationsGridViewModelTests_CheckRelocationCollection_ShouldReturnTrue()
        {
            var relocationViewModel = new RelocationsGridViewModel();

            Assert.Null(relocationViewModel.RelocationsCollection);

        }

        [Fact]
        public void RelocationsGridViewModelTests_CheckIfTheRelocationCollectionHasChanged_ShouldReturnTrue()
        {
            var relocationViewModel = new RelocationsGridViewModel();
            relocationViewModel.RelocationsCollection = new List<RelocationSet> { new RelocationSet { White = "e4", Black = "e6" } };

            Assert.Single(relocationViewModel.RelocationsCollection);
            Assert.Equal(new RelocationSet { 
                White = "e4", 
                Black = "e6" 
            }, relocationViewModel.RelocationsCollection.First(), new RelocationSetEqualityComparer());

        }

        [Fact]
        public void RelocationsGridViewModelTests_CheckTheDefaultRelocationScrollOffset_ShouldReturnTrue()
        {
            var relocationViewModel = new RelocationsGridViewModel();

            Assert.Equal(0, relocationViewModel.RelocationsScrollOffset);
        }

        [Fact]
        public void RelocationsGridViewModelTests_CheckIfTheRelocationScrollOffsetHasChanged_ShouldReturnTrue()
        {
            var relocationViewModel = new RelocationsGridViewModel
            {
                RelocationsScrollOffset = 10
            };
            Assert.Equal(10, relocationViewModel.RelocationsScrollOffset);

            relocationViewModel.RelocationsScrollOffset = -10;
            Assert.Equal(-10, relocationViewModel.RelocationsScrollOffset);
        }
    }
}
