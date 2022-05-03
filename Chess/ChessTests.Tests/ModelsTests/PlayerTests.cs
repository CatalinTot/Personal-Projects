using Xunit;
using Chess.Models;
using Chess.Models.Pieces;
using System.Linq;
using ChessTests.Tests.Stubs;

namespace ChessTests.Tests.ModelsTests
{
    public class PlayerTests
    {
        [Fact]
        public void PlayerTests_TryRemoveAPieceThatHasACertainPositionFromThePlayer_ShouldReturnTrue()
        {
            var player = new Player(new[] { 0, 1 }, new StubBoardPath(true));
            var position = new Position('8', 'a');

            Assert.True(player.TryRemoveFrom(position));
            Assert.Null(player.FirstOrDefault(x => x.Current.Equals(position)));
        }

        [Fact]
        public void PlayerTests_TryRemoveAPieceThatHasACertainPositionFromThePlayerIfThePositionIsNull_ShouldReturnTrue()
        {
            var player = new Player(new[] { 0, 1 },new StubBoardPath(true));
            Position position = null;

            Assert.False(player.TryRemoveFrom(position));
        }

        [Fact]
        public void PlayerTests_CheckIfAPieceFromAPlayerCanPromote_ShouldReturnTrue()
        {
            var player = new Player(new[] { 0, 1 }, new StubBoardPath(true));
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            var queen = new Queen(new StubBoardPath(true));

            Assert.False(player.TryPromote(pawn, queen));
        }

        [Fact]
        public void PlayerTests_CheckIfAPlayerCanPlaceTheRivalInMate_ShouldReturnTrue()
        {
            var player = new Player(new[] { 0, 1 }, new StubBoardPath(true));
            var queen = player.First(x => x.GetType().Name == "Queen");
            var rook = player.First(x => x.GetType().Name == "Rook");
            rook.Current = new Position('3', 'h');
            queen.Current = new Position('2', 'h');
            var king = new King(new StubBoardPath(true)) { Current = new Position('1', 'h'), IsMovedOnce = false };

            Assert.True(player.IsCheckMate(king));
        }

        [Fact]
        public void PlayerTests_CheckIfAPlayerCanPlaceTheRivalInMate_ShouldReturnFalse()
        {
            var player = new Player(new[] { 0, 1 }, new StubBoardPath(true));
            var king = new King(new StubBoardPath(true)) { Current = new Position('1', 'h'), IsMovedOnce = false };

            Assert.False(player.IsCheckMate(king));
        }
    }
}
