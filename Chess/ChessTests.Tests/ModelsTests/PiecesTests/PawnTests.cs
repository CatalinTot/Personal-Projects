using Xunit;
using Chess.Models.Pieces;
using Chess.Models.MoveTypes;
using Chess.Models;
using System.Linq;
using System.Collections.Generic;
using ChessTests.Tests.Stubs;

namespace ChessTests.Tests.ModelsTests.PiecesTests
{
    public class PawnTests
    {
        [Fact]
        public void PawnTests_CheckIfAPawnCanRelocateTwoSquaresAtTheFirstMoveOnAFreePath_ShouldReturnTrue()
        {
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            var move = new Move("a6", new[] { pawn });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(pawn.Current, new Position('6', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void PawnTests_CheckIfAPawnCanRelocateTwoSquaresAtTheFirstMoveOnAnOccupiedPath_ShouldReturnFalse()
        {
            var pawn = new Pawn(new StubBoardPath(false)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            var move = new Move("a6", new[] { pawn });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(pawn.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void PawnTests_CheckIfAPawnCanRelocateTwoSquaresAtTheSecondMoveOnAFreePath_ShouldReturnFalse()
        {
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = true };
            var move = new Move("a6", new[] { pawn });
            var isMoved = move.Act(RelocationType.Move);
            Assert.False(isMoved);
            Assert.Equal(pawn.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void PawnTests_CheckIfAPawnCanRelocateOneSquareAtTheSecondMoveOnAFreePath_ShouldReturnTrue()
        {
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = true };
            var move = new Move("a7", new[] { pawn });
            var isMoved = move.Act(RelocationType.Move);

            Assert.True(isMoved);
            Assert.Equal(pawn.Current, new Position('7', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void PawnTests_CheckIfAPawnCanRelocateOneSquareAtTheSecondMoveOnAnOccupiedPath_ShouldReturnFalse()
        {
            var pawn = new Pawn(new StubBoardPath(false)) { Current = new Position('8', 'a'), IsMovedOnce = true };
            var move = new Move("a7", new[] { pawn });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(pawn.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void PawnTests_CheckIfAPawnCanRelocateMoreThanPossiblePawnSquaresOnAFreePath_ShouldReturnFalse()
        {
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = true };
            var move = new Move("a5", new[] { pawn });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(pawn.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
        }

        [Fact]
        public void PawnTests_CheckIfAPawnCanRelocateOnDiagonalOnAFreePathWhenNotCapturing_ShouldReturnFalse()
        {
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = true };
            var move = new Move("b7", new[] { pawn });
            var isMoved = move.Act(RelocationType.Move);

            Assert.False(isMoved);
            Assert.Equal(pawn.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void PawnTests_CheckIfAPawnCanRelocateOnDiagonalOnAFreePathWhenCapturing_ShouldReturnTrue()
        {
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = true };
            var move = new DisambiguatingCaptureMove("axb7", new[] { pawn }, new StubRelocationHelper());
            var isMoved = move.Act(RelocationType.DisambiguousCapture);

            Assert.True(isMoved);
            Assert.Equal(pawn.Current, new Position('7', 'b'), new PiecePositionEqualityComparer());
        }
        
        [Fact]
        public void PawnTests_CheckIfAPawnCanPromoteOnCaptureWithAQueen_ShouldReturnTrue()
        {
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('7', 'a'), IsMovedOnce = true };
            var possibilities = new List<IPiece> { pawn };
            var move = new CapturingPromotionMove("axb8=Q", possibilities, new StubRelocationHelper(), new StubRelocationHelper(possibilities));
            var isMoved = move.Act(RelocationType.CapturingPromotion);
            var promo = possibilities.First();

            Assert.True(isMoved);
            Assert.Equal(promo.Current, new Position('8', 'b'), new PiecePositionEqualityComparer());
            Assert.Equal("Queen", promo.GetType().Name);
        }
        
        [Fact]
        public void PawnTests_CheckIfAPawnCanPromoteForARook_ShouldReturnTrue()
        {
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('7', 'a'), IsMovedOnce = true };
            var possibilities = new List<IPiece> { pawn };
            var move = new PromotionMove("a8=R", possibilities, new StubRelocationHelper(possibilities));
            var isMoved = move.Act(RelocationType.Promotion);
            var promo = possibilities.First();

            Assert.True(isMoved);
            Assert.Equal(promo.Current, new Position('8', 'a'), new PiecePositionEqualityComparer());
            Assert.Equal("Rook", promo.GetType().Name);
        }
    }
}