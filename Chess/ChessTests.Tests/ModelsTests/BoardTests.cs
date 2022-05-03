using Xunit;
using Chess.Models;
using System.Collections.Generic;
using Chess.Models.Pieces;
using System.Linq;

namespace ChessTests.Tests.ModelsTests
{
    public class BoardTests
    {
        [Fact]
        public void BoardTests_CheckIfTheBoardCanExecuteARightMove_ShouldReturnTrue()
        {
            var board = new Board();
            var move = "e4";
            var index = 0;
            var success = board.ExecuteMove(move, index, true, out IEnumerable<IPiece> pieces);
            var piece = pieces.FirstOrDefault(x => x.Current.Equals(new Position('4', 'e')));

            Assert.True(success);
            Assert.NotNull(piece);
        }

        [Fact]
        public void BoardTests_CheckIfTheBoardCanExecuteAWrongMove_ShouldReturnFalse()
        {
            var board = new Board();
            var move = "asdgdafh";
            var index = 0;
            var success = board.ExecuteMove(move, index, true, out IEnumerable<IPiece> pieces);

            Assert.False(success);
            Assert.Null(pieces);
        }

        [Fact]
        public void BoardTests_CheckIfTheBoardCanExecuteBack_ShouldReturnTrue()
        {
            var board = new Board();
            var move = "e4";
            var index = 0;
            var success = board.ExecuteMove(move, index, true, out IEnumerable<IPiece> pieces);
            Assert.True(success);

            success = board.ExecuteMove(move, index - 1, false, out pieces);
            var piece = pieces.FirstOrDefault(x => x.Current.Equals(new Position('4', 'e')));
            Assert.True(success);
            Assert.Null(piece);
        }

        [Fact]
        public void BoardTests_CheckIfTheBoardCanExecuteBackIfTheIndexIsOutOfRange_ShouldReturnTrue()
        {
            var board = new Board();
            var move = "e4";
            var index = -2;
            var success = board.ExecuteMove(move, index, false, out IEnumerable<IPiece> pieces);

            Assert.False(success);
            Assert.Null(pieces);
        }

        [Fact]
        public void BoardTests_CheckIfACertainPathFromTheBoardIsFree_ShouldReturnTrue()
        {
            var board = new Board();
            var current = new Position('5', 'a');
            var next = new Position('5', 'd');
            var success = board.IsFreePath(current, next, false);

            Assert.True(success);
        }

        [Fact]
        public void BoardTests_CheckIfACertainPathFromTheBoardIsFree_ShouldReturnFalse()
        {
            var board = new Board();
            var current = new Position('8', 'a');
            var next = new Position('8', 'd');
            var success = board.IsFreePath(current, next, false);

            Assert.False(success);
        }

        [Fact]
        public void BoardTests_CheckIfACertainPathWhenThePositionsAreNull_ShouldReturnFalse()
        {
            var board = new Board();
            var success = board.IsFreePath(null, null, false);

            Assert.False(success);
        }

        [Fact]
        public void BoardTests_CheckIfACertainPathCanBeUpdated_ShouldReturnTrue()
        {
            var board = new Board();
            var current = new Position('8', 'a');
            var next = new Position('8', 'd');
            var success = board.TryUpdatePath(current, next);

            Assert.True(success);
        }
    }
}
