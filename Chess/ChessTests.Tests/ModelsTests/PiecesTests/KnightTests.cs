using Xunit;
using Chess.Models.Pieces;
using Chess.Models.MoveTypes;
using Chess.Models;
using ChessTests.Tests.Stubs;

namespace ChessTests.Tests.ModelsTests.PiecesTests
{
    public class KnightTests
    {
        [Fact]
        public void Knight_CheckIfAKnightCanRelocateOnAFreePath_ShouldReturnTrue()
        {
            var knight = new Knight(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new Move("Nf6", new[] { knight });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(knight.Current, new Position('6', 'f'), new PiecePositionEqualityComparer());

            move = new Move("Nd7", new[] { knight });
            isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(knight.Current, new Position('7', 'd'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanRelocateOnAWrongPath_ShouldReturnFalse()
        {
            var knight = new Knight(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new Move("Nd5", new[] { knight });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(knight.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanRelocateOnAnOccupiedPath_ShouldReturnFalse()
        {
            var knight = new Knight(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var move = new Move("Nf6", new[] { knight });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(knight.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanCaptureOnARightPath_ShouldReturnTrue()
        {
            var knight = new Knight(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Nxf6", new[] { knight }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(knight.Current, new Position('6', 'f'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanCapture_ShouldReturnFalse()
        {
            var knight = new Knight(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var move = new CapturingMove("Nxf5", new[] { knight }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.False(isMoved);
            Assert.Equal(knight.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanRelocateDisambiguatingByRankOnAFreePath_ShouldReturnTrue()
        {
            var firstKnight = new Knight(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var secondKnight = new Knight(new StubBoardPath(true)) { Current = new Position('8', 'e') };
            var move = new DisambiguatingMove("N8f6", new[] { firstKnight, secondKnight });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstKnight.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(secondKnight.Current, new Position('6', 'f'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanRelocateDisambiguatingByFileOnAFreePath_ShouldReturnTrue()
        {
            var firstKnight = new Knight(new StubBoardPath(true)) { Current = new Position('8', 'd') };
            var secondKnight = new Knight(new StubBoardPath(true)) { Current = new Position('8', 'f') };
            var move = new DisambiguatingMove("Nde6", new[] { firstKnight, secondKnight });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstKnight.Current, new Position('6', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(secondKnight.Current, new Position('8', 'f'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanCaptureDisambiguatingByFile_ShouldReturnTrue()
        {
            var firstKnight = new Knight(new StubBoardPath(true)) { Current = new Position('8', 'd') };
            var secondKnight = new Knight(new StubBoardPath(true)) { Current = new Position('8', 'f') };
            var move = new DisambiguatingMove("Ndxe6", new[] { firstKnight, secondKnight });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstKnight.Current, new Position('6', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(secondKnight.Current, new Position('8', 'f'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanCaptureDisambiguatingByRank_ShouldReturnTrue()
        {
            var firstKnight = new Knight(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var secondKnight = new Knight(new StubBoardPath(true)) { Current = new Position('8', 'e') };
            var move = new DisambiguatingMove("N8xf6", new[] { firstKnight, secondKnight });
            var isMoved = move.Act(RelocationType.DisambiguousMove);

            Assert.True(isMoved);
            Assert.Equal(firstKnight.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(secondKnight.Current, new Position('6', 'f'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanCheckAnOponentOnAFreePath_ShouldReturnTrue()
        {
            var knight = new Knight(new StubBoardPath(true)) { Current = new Position('4', 'e') };
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'g') };
            var move = new CheckMove("Nf6+", new[] { knight }, king);
            var isMoved = move.Act(RelocationType.Check);

            Assert.True(isMoved);
            Assert.Equal(knight.Current, new Position('6', 'f'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void Knight_CheckIfAKnightCanCheckAnOponentOnAnOccupiedPath_ShouldReturnFalse()
        {
            var knight = new Knight(new StubBoardPath(false)) { Current = new Position('4', 'e') };
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'g')};
            var move = new CheckMove("Nf6+", new[] { knight }, king);
            var isMoved = move.Act(RelocationType.Check);

            Assert.False(isMoved);
            Assert.Equal(knight.Current, new Position('4', 'e'), new PiecePositionEqualityComparer());
        }
    }
}
