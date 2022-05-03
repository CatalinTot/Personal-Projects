using Xunit;
using Chess.ViewModel;
using System.Linq;
using System.Collections.Generic;
using Chess.Models.Pieces;

namespace ChessTests.Tests.ViewModelTests
{
    public class PlayerPiecesViewModelTests
    {
        [Fact]
        public void PlayerPiecesViewModelTests_CheckIfThePiecesCollectionHasAllPieces_ShouldReturnTrue()
        {
            var viewModel = new PlayerPiecesViewModel();

            Assert.Equal(32, viewModel.SourceCollection.Count());
        }

        [Fact]
        public void PlayerPiecesViewModelTests_CheckIfThePiecesCollectionHasChanged_ShouldReturnTrue()
        {
            var viewModel = new PlayerPiecesViewModel();
            viewModel.UpdateInterface(new List<IPiece> {
                new Pawn(null) {
                    Id = "00",
                    Current = new Chess.Models.Pieces.Position(4, 4),
                    IsMovedOnce = true }
            });

            Assert.Single(viewModel.SourceCollection);
            Assert.Equal(new Icon
            {
                IconPath = "pack://application:,,,/Chess;component/Files/Pieces/Rook-black.png",
                Id = "00",
                Position = new Chess.ViewModel.Position(4, 4)
            }, viewModel.SourceCollection.First(), new PieceIconComparer());
        }
    }
}
