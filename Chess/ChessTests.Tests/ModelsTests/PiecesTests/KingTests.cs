using Xunit;
using Chess.Models.Pieces;
using Chess.Models.MoveTypes;
using Chess.Models;
using ChessTests.Tests.Stubs;

namespace ChessTests.Tests.ModelsTests.PiecesTests
{
    public class KingTests
    {
        [Fact]
        public void KingTests_CheckIfAKingCanRelocateOneSquareOnAFreeStraightPath_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('4', 'd') };
            var move = new Move("Kd5", new[] { king });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(king.Current, new Position('5', 'd'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanRelocateOneSquareOnAnOccupiedStraightPath_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(false)) { Current = new Position('4', 'd') };
            var move = new Move("Kd5", new[] { king });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(king.Current, new Position('4', 'd'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanRelocateOneSquareOnADiagonalPath_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('4', 'd') };
            var move = new Move("Kc5", new[] { king });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(king.Current, new Position('5', 'c'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanRelocateMoreThanOneSquareOnAStraightPath_ShouldReturnFalse()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('4', 'd'), };
            var move = new Move("Kd8", new[] { king });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(king.Current, new Position('4', 'd'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanRelocateMoreThanOneSquareOnADiagonalPath_ShouldReturnFalse()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('4', 'd') };
            var move = new Move("Ka7", new[] { king });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(king.Current, new Position('4', 'd'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanCaptureOnAFreeStraightPath_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('4', 'd') };
            var move = new CapturingMove("Kxd5", new[] { king }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(king.Current, new Position('5', 'd'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanCaptureOnAFreeDiagonalPath_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('4', 'd') };
            var move = new CapturingMove("Kxc5", new[] { king }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.Capture);

            Assert.True(isMoved);
            Assert.Equal(king.Current, new Position('5', 'c'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanCastleOnKingSideOnAFreePath_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'e'), IsMovedOnce = false };
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            var secondRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'h'), IsMovedOnce = false };
            var move = new CastlingMove("O-O", king, new[] { firstRook, secondRook });
            var isMoved = move.Act(RelocationType.Castling);

            Assert.True(isMoved);
            Assert.Equal(king.Current, new Position('8', 'g'), new PiecePositionEqualityComparer());
            Assert.Equal(firstRook.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('8', 'f'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanCastleOnQueenSideOnAFreePath_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'e'), IsMovedOnce = false }; 
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            var secondRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'h'), IsMovedOnce = false };
            var move = new CastlingMove("O-O-O", king, new[] { firstRook, secondRook });
            var isMoved = move.Act(RelocationType.Castling);

            Assert.True(isMoved);
            Assert.Equal(king.Current, new Position('8', 'c'), new PiecePositionEqualityComparer());
            Assert.Equal(firstRook.Current, new Position('8', 'd'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('8', 'h'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanCastleOnQueenSideOnAFreePathIfTheKingIsMovedOnce_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'e'), IsMovedOnce = true };
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            var secondRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'h'), IsMovedOnce = false };
            var move = new CastlingMove("O-O-O", king, new[] { firstRook, secondRook });
            var isMoved = move.Act(RelocationType.Castling);

            Assert.False(isMoved);
            Assert.Equal(king.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(firstRook.Current, new Position('8', 'd'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('8', 'h'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanCastleOnQueenSideOnAFreePathIfTheRightRookIsMovedOnce_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'e'), IsMovedOnce = false };
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = true };
            var secondRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'h'), IsMovedOnce = false };
            var move = new CastlingMove("O-O-O", king, new[] { firstRook, secondRook });
            var isMoved = move.Act(RelocationType.Castling);

            Assert.False(isMoved);
            Assert.Equal(king.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(firstRook.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('8', 'h'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void KingTests_CheckIfAKingCanCastleOnQueenSideOnAFreePathIfThereIsACollision_ShouldReturnTrue()
        {
            var king = new King(new StubBoardPath(true)) { Current = new Position('8', 'e'), IsMovedOnce = false };
            var firstRook = new Rook(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = true };
            var secondRook = new Rook(new StubBoardPath(false)) { Current = new Position('8', 'h'), IsMovedOnce = false };
            var move = new CastlingMove("O-O-O", king, new[] { firstRook, secondRook });
            var isMoved = move.Act(RelocationType.Castling);

            Assert.False(isMoved);
            Assert.Equal(king.Current, new Position('8', 'e'), new PiecePositionEqualityComparer());
            Assert.Equal(firstRook.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
            Assert.Equal(secondRook.Current, new Position('8', 'h'), new PiecePositionEqualityComparer());
        }
    }
}
