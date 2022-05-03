using Xunit;
using Chess.Models.Pieces;

namespace ChessTests.Tests.ModelsTests.PiecesTests
{
    public class PositionTests
    {
        [Fact]
        public void PositionTests_CheckIfTwoPositionsAreEqual_ShouldReturnTrue()
        {
            var firstPosition = new Position('3', 'a');
            var secondPosition = new Position('3', 'a');

            Assert.Equal(firstPosition, secondPosition, new PiecePositionEqualityComparer());
        }

        [Fact]
        public void PositionTests_CheckIfAPositionIsOutOfBounds_ShouldReturnTrue()
        {
            var firstPosition = new Position(-1, 100);

            Assert.True(firstPosition.IsOutOfBounds());
        }
    }
}
