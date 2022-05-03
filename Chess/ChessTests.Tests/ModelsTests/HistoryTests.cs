using Xunit;
using Chess.Models;
using System.Collections.Generic;
using Chess.Models.Pieces;
using System.Linq;
using ChessTests.Tests.Stubs;

namespace ChessTests.Tests.ModelsTests
{
    public class HistoryTests
    {
        [Fact]
        public void HistoryTests_CheckIfAnItemWasAddedInHistory_ShouldReturnTrue()
        {
            var history = new History();
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            history.Add("", new[] { pawn });

            Assert.Equal(1, history.Count);
        }

        [Fact]
        public void HistoryTests_CheckIfCanGetAnItemFromHistory_ShouldReturnTrue()
        {
            var history = new History();
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            history.Add("", new[] { pawn });

            Assert.Equal("", history[0].Key);
            Assert.Equal(new Position('8', 'a'), history[0].Value.First().Current, new PiecePositionEqualityComparer());
            Assert.Equal(pawn.Verifier, history[0].Value.First().Verifier);
        }

        [Fact]
        public void HistoryTests_TryRetreiveAHistoryItemThatIsOutOfBounds_ShouldReturnTrue()
        {
            var history = new History();
            var pawn = new Pawn(new StubBoardPath(true)) { Current = new Position('8', 'a'), IsMovedOnce = false };
            history.Add("", new[] { pawn });

            Assert.Equal(new KeyValuePair<string, List<IPiece>>("", null), history[-1]);
        }
    }
}
