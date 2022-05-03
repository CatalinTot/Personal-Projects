using Xunit;
using Chess.Models.Pieces;
using Chess.Models.MoveTypes;
using Chess.Models;
using ChessTests.Tests.Stubs;

namespace ChessTests.Tests.ModelsTests.PiecesTests
{
    public class RookTests
    {
        [Fact]
        public void RookTests_CheckIfARookCanRelocateOnAVerticalFreePath_ShouldReturnTrue()
        {
            var rook = new Rook(new StubBoardPath(true)) { Current = new Position('4', 'e')};
            var move = new Move("Re8", new[] { rook });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(rook.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanRelocateOnAWrongPath_ShouldReturnTrue()
        {
            var rook = new Rook(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new Move("Rg7", new[] { rook });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(rook.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanRelocateOnAHorizontalFreePath_ShouldReturnTrue()
        {
            var rook = new Rook(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new Move("Rh4", new[] { rook });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(rook.Current, new Position('4', 'h'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanRelocateOnAVerticalOccupiedPath_ShouldReturnFalse()
        {
            var rook = new Rook(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new Move("Re8", new[] { rook });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(rook.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanRelocateOnAHorizontalOccupiedPath_ShouldReturnFalse()
        {
            var rook = new Rook(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new Move("Rh4", new[] { rook });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(rook.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanCaptureOnAVerticalFreePath_ShouldReturnTrue()
        {
            var rook = new Rook(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Rxe8", new[] { rook }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(rook.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanCaptureOnAHorizontalFreePath_ShouldReturnTrue()
        {
            var rook = new Rook(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Rxh4", new[] { rook }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(rook.Current, new Position('4', 'h'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanCaptureOnAVerticalOccupiedPath_ShouldReturnFalse()
        {
            var rook = new Rook(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Rxe8", new[] { rook }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.False(isMoved);
            Assert.Equal(rook.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanCaptureOnAHorizontalOccupiedPath_ShouldReturnFalse()
        {
            var rook = new Rook(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Rxh4", new[] { rook }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.False(isMoved);
            Assert.Equal(rook.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanRelocateDisambiguatingByFileOnAFreePath_ShouldReturnTrue()
        {
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a') };
            var secondRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'h') };
            var move = new DisambiguatingMove("Rhe8", new[] { firstRook, secondRook });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstRook.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanRelocateDisambiguatingByRankOnAFreePath_ShouldReturnTrue()
        {
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a') };
            var secondRook = new Rook(new StubBoardPath(true)) { Current = new Position('1', 'a') };
            var move = new DisambiguatingMove("R1a5", new[] { firstRook, secondRook });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstRook.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('5', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanCaptureDisambiguatingByFileOnAFreePath_ShouldReturnTrue()
        {
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a') };
            var secondRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'h') };
            var move = new DisambiguatingCaptureMove("Rhxe8", new[] { firstRook, secondRook }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.DisambiguousCapture);

            Assert.True(isMoved);
            Assert.Equal(firstRook.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void RookTests_CheckIfARookCanCaptureDisambiguatingByRankOnAFreePath_ShouldReturnTrue()
        {
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a') };
            var secondRook = new Rook(new StubBoardPath(true)) { Current = new Position('1', 'a') };
            var move = new DisambiguatingCaptureMove("R1xa5", new[] { firstRook, secondRook }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.DisambiguousCapture);

            Assert.True(isMoved);
            Assert.Equal(firstRook.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('5', 'a'), new PiecePositionEqualityComparer());
        }
    }
}
