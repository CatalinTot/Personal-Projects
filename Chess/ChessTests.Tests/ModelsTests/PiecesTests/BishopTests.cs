using Xunit;
using Chess.Models.Pieces;
using Chess.Models.MoveTypes;
using Chess.Models;
using ChessTests.Tests.Stubs;

namespace ChessTests.Tests.ModelsTests.PiecesTests
{
    public class BishopTests
    {
        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateOnaFreeDiagonalPath_ShouldReturnTrue()
        {
            var bishop = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var move = new Move("a6", new[] { bishop });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(bishop.Current, new Position('6', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateBackOnAFreeDiagonalPath_ShouldReturnTrue()
        {
            var bishop = new Bishop(new StubBoardPath(true)) { Current = new Position('6', 'a')};
            var move = new Move("f1", new[] { bishop });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(bishop.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateOnAOccupiedDiagonalPath_ShouldReturnFalse()
        {
            var bishop = new Bishop(new StubBoardPath(false)) { Current = new Position('1', 'f') };
            var move = new Move("a6", new[] { bishop });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(bishop.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateOnAFreeDiagonalPathThatExceedsBoundaries_ShouldReturnFalse()
        {
            var bishop = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var move = new Move("i4", new[] { bishop });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(bishop.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateOnAStraightFreePath_ShouldReturnFalse()
        {
            var bishop = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var move = new Move("f6", new[] { bishop });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(bishop.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanCaptureOnAFreeDiagonalPath_ShouldReturnTrue()
        {
            var bishop = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var move = new CapturingMove("Bxd3", new[] { bishop }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(bishop.Current, new Position('3', 'd'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanCaptureOnAnOccupiedDiagonalPath_ShouldReturnFalse()
        {
            var bishop = new Bishop(new StubBoardPath(false)) { Current = new Position('1', 'f') };
            var move = new CapturingMove("Bxd3", new[] { bishop }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.False(isMoved);
            Assert.Equal(bishop.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateDisambiguatingByRankOnAFreeDiagonalPath_ShouldReturnTrue()
        {
            var bishopFirst = new Bishop(new StubBoardPath(true)) { Current = new Position('5', 'b') };
            var bishopSecond = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var move = new DisambiguatingMove("B1d3", new[] { bishopFirst, bishopSecond });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(bishopFirst.Current, new Position('5', 'b'), new PiecePositionEqualityComparer());
            Assert.Equal(bishopSecond.Current, new Position('3', 'd'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateDisambiguatingByRankOnAnOccupiedDiagonalPath_ShouldReturnTrue()
        {
            var bishopFirst = new Bishop(new StubBoardPath(false)) { Current = new Position('5', 'b') };
            var bishopSecond = new Bishop(new StubBoardPath(false)) { Current = new Position('1', 'f') };
            var move = new DisambiguatingMove("B1d3", new[] { bishopFirst, bishopSecond });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.False(isMoved);
            Assert.Equal(bishopFirst.Current, new Position('5', 'b'), new PiecePositionEqualityComparer());
            Assert.Equal(bishopSecond.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateDisambiguatingByFileOnAFreeDiagonalPath_ShouldReturnTrue()
        {
            var bishopFirst = new Bishop(new StubBoardPath(true)) { Current = new Position('5', 'b') };
            var bishopSecond = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var move = new DisambiguatingMove("Bfd3", new[] { bishopFirst, bishopSecond });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(bishopFirst.Current, new Position('5', 'b'), new PiecePositionEqualityComparer());
            Assert.Equal(bishopSecond.Current, new Position('3', 'd'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanRelocateDisambiguatingByFileOnAnOccupiedDiagonalPath_ShouldReturnTrue()
        {
            var bishopFirst = new Bishop(new StubBoardPath(false)) { Current = new Position('5', 'b') };
            var bishopSecond = new Bishop(new StubBoardPath(false)) { Current = new Position('1', 'f') };
            var move = new DisambiguatingMove("Bfd3", new[] { bishopFirst, bishopSecond });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.False(isMoved);
            Assert.Equal(bishopFirst.Current, new Position('5', 'b'), new PiecePositionEqualityComparer());
            Assert.Equal(bishopSecond.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanCaptureDisambiguatingByRankOnAFreeDiagonalPath_ShouldReturnTrue()
        {
            var bishopFirst = new Bishop(new StubBoardPath(true)) { Current = new Position('5', 'b') };
            var bishopSecond = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var move = new DisambiguatingCaptureMove("B1xd3", new[] { bishopFirst, bishopSecond }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.DisambiguousCapture);

            Assert.True(isMoved);
            Assert.Equal(bishopFirst.Current, new Position('5', 'b'), new PiecePositionEqualityComparer());
            Assert.Equal(bishopSecond.Current, new Position('3', 'd'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanCaptureDisambiguatingByRankOnAnOccupiedDiagonalPath_ShouldReturnTrue()
        {
            var bishopFirst = new Bishop(new StubBoardPath(false)) { Current = new Position('5', 'b') };
            var bishopSecond = new Bishop(new StubBoardPath(false)) { Current = new Position('1', 'f') };
            var move = new DisambiguatingCaptureMove("B1xd3", new[] { bishopFirst, bishopSecond }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.DisambiguousCapture);

            Assert.False(isMoved);
            Assert.Equal(bishopFirst.Current, new Position('5', 'b'), new PiecePositionEqualityComparer());
            Assert.Equal(bishopSecond.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void BishopTests_CheckIfABishopCanCaptureDisambiguatingByFileOnAFreeDiagonalPath_ShouldReturnTrue()
        {
            var bishopFirst = new Bishop(new StubBoardPath(true)) { Current = new Position('5', 'b') };
            var bishopSecond = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var move = new DisambiguatingCaptureMove("Bfxd3", new[] { bishopFirst, bishopSecond }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.DisambiguousCapture);

            Assert.True(isMoved);
            Assert.Equal(bishopFirst.Current, new Position('5', 'b'), new PiecePositionEqualityComparer());
            Assert.Equal(bishopSecond.Current, new Position('3', 'd'), new PiecePositionEqualityComparer());
        }


        [Fact]
        public void BishopTests_CheckIfABishopCanCheckAnOponentOnAFreePath_ShouldReturnTrue()
        {
            var bishop = new Bishop(new StubBoardPath(true)) { Current = new Position('1', 'f') };
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'e') };
            var move = new CheckMove("Bb5+", new[] { bishop }, king);
            var isMoved = move.Act(RelocationType.Check);

            Assert.True(isMoved);
            Assert.Equal(bishop.Current, new Position('5', 'b'), new PiecePositionEqualityComparer());
        }


        [Fact]
        public void BishopTests_CheckIfABishopCanCheckAnOponentOnAnOccupiedPath_ShouldReturnFalse()
        {
            var bishop = new Bishop(new StubBoardPath(false)) { Current = new Position('1', 'f') };
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'e') };
            var move = new CheckMove("Bb5+", new[] { bishop }, king);
            var isMoved = move.Act(RelocationType.Check);

            Assert.False(isMoved);
            Assert.Equal(bishop.Current, new Position('1', 'f'), new PiecePositionEqualityComparer());
        }
    }
}
