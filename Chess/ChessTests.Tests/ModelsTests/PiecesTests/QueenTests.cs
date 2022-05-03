using Xunit;
using Chess.Models.Pieces;
using Chess.Models.MoveTypes;
using Chess.Models;
using ChessTests.Tests.Stubs;

namespace ChessTests.Tests.ModelsTests.PiecesTests
{
    public class QueenTests
    {
        [Fact]
        public void QueenTests_CheckIfAQueenCanRelocateOnAVerticalFreePath_ShouldReturnTrue()
        {
            var queen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new Move("e8", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(queen.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfANullQueenCanRelocateOnAVerticalFreePath_ShouldReturnFalse()
        {
            Queen queen = null;
            var move = new Move("e8", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Null(queen);
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanRelocateOnAVerticalOccupiedPath_ShouldReturnFalse()
        {
            var queen = new Queen(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new Move("e8", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanRelocateOnAHorizontalFreePath_ShouldReturnTrue()
        {
            var queen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new Move("a4", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanRelocateOnAHorizontalOccupiedPath_ShouldReturnFalse()
        {
            var queen = new Queen(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new Move("a4", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanRelocateOnADiagonalFreePath_ShouldReturnTrue()
        {
            var queen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new Move("h7", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(queen.Current, new Position('7', 'h'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanRelocateOnADiagonalOccupiedPath_ShouldReturnFalse()
        {
            var queen = new Queen(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new Move("h7", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanRelocateOnAWrongPath_ShouldReturnFalse()
        {
            var queen = new Queen(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new Move("d6", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }
        [Fact]
        public void QueenTests_CheckIfAQueenCanCaptureOnAVerticalFreePath_ShouldReturnTrue()
        {
            var queen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Qxe8", new[] { queen }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(queen.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanCaptureOnAVerticalOccupiedPath_ShouldReturnFalse()
        {
            var queen = new Queen(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Qxe8", new[] { queen }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.False(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanCaptureOnAHorizontalFreePath_ShouldReturnTrue()
        {
            var queen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Qxa4", new[] { queen }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanCaptureOnAHorizontalOccupiedPath_ShouldReturnFalse()
        {
            var queen = new Queen(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Qxa4", new[] { queen }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.False(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanCaptureOnADiagonalFreePath_ShouldReturnTrue()
        {
            var queen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Qxh7", new[] { queen }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(queen.Current, new Position('7', 'h'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanCaptureOnADiagonalOccupiedPath_ShouldReturnFalse()
        {
            var queen = new Queen(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Qxh7", new[] { queen }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.False(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanCaptureOnAWrongPath_ShouldReturnFalse()
        {
            var queen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new Move("Qxd6", new[] { queen });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(queen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanMoveDisambiguatingByFile_ShouldReturnTrue()
        {
            var firstQueen = new Queen(new StubBoardPath(true)) { Current = new Position('3', 'd') };
            var secondQueen = new Queen(new StubBoardPath(true)) { Current = new Position('3', 'h') };
            var move = new DisambiguatingMove("Qhf5", new[] { firstQueen, secondQueen });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstQueen.Current, new Position('3', 'd'), new PiecePositionEqualityComparer());
            Assert.Equal(secondQueen.Current, new Position('5', 'f'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanMoveDisambiguatingByRank_ShouldReturnTrue()
        {
            var firstQueen = new Queen(new StubBoardPath(true)) { Current = new Position('6', 'e') };
            var secondQueen = new Queen(new StubBoardPath(true)) { Current = new Position('2', 'e') };
            var move = new DisambiguatingMove("Q2c4", new[] { firstQueen, secondQueen });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstQueen.Current, new Position('6', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(secondQueen.Current, new Position('4', 'c'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanMoveDisambiguatingByRankAndFile_ShouldReturnTrue()
        {
            var firstQueen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var secondQueen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'h') };
            var thirdQueen = new Queen(new StubBoardPath(true)) { Current = new Position('1', 'h') };
            var move = new DisambiguatingMove("Qh4e1", new[] { firstQueen, secondQueen });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstQueen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(secondQueen.Current, new Position('1', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(thirdQueen.Current, new Position('1', 'h'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void QueenTests_CheckIfAQueenCanCaptureDisambiguatingByRankAndFile_ShouldReturnTrue()
        {
            var firstQueen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var secondQueen = new Queen(new StubBoardPath(true)) { Current = new Position('4', 'h') };
            var thirdQueen = new Queen(new StubBoardPath(true)) { Current = new Position('1', 'h') };
            var move = new DisambiguatingCaptureMove("Qh4xe1", new[] { firstQueen, secondQueen }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.DisambiguousCapture);

            Assert.True(isMoved);
            Assert.Equal(firstQueen.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(secondQueen.Current, new Position('1', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(thirdQueen.Current, new Position('1', 'h'), new PiecePositionEqualityComparer());
        }
    }
}
