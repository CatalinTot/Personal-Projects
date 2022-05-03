using Xunit;
using Chess.Models;

namespace ChessTests.Tests.ModelsTests
{
    public class RelocationPatternTests
    {
        [Fact]
        public void RelocationTests_CheckIfThePatternMatchesSimpleMoves_ShouldReturnTrue()
        {
            var pattern = new RelocationPattern();
            Assert.Equal(RelocationType.Move, pattern.Solve("c4"));
            Assert.Equal(RelocationType.Move, pattern.Solve("Rg5"));
            Assert.Equal(RelocationType.Move, pattern.Solve("Nf3"));
            Assert.Equal(RelocationType.Move, pattern.Solve("Ba3"));
            Assert.Equal(RelocationType.Move, pattern.Solve("Qc1"));
            Assert.Equal(RelocationType.Move, pattern.Solve("Kf2"));
        }

        [Fact]
        public void RelocationTests_CheckIfThePatternMatchesADisambiguatingMoves_ShouldReturnTrue()
        {
            var pattern = new RelocationPattern();
            Assert.Equal(RelocationType.DisambiguousMove, pattern.Solve("Nfc3"));
            Assert.Equal(RelocationType.DisambiguousMove, pattern.Solve("Qbc3"));
            Assert.Equal(RelocationType.DisambiguousMove, pattern.Solve("Bbc3"));
            Assert.Equal(RelocationType.DisambiguousMove, pattern.Solve("Rb1c3"));
        }

        [Fact]
        public void RelocationTests_CheckIfThePatternMatchesCaptures_ShouldReturnTrue()
        {
            var pattern = new RelocationPattern();
            Assert.Equal(RelocationType.Capture, pattern.Solve("Rxc3"));
            Assert.Equal(RelocationType.Capture, pattern.Solve("Nxc3"));
            Assert.Equal(RelocationType.Capture, pattern.Solve("Bxc3"));
            Assert.Equal(RelocationType.Capture, pattern.Solve("Qxc3"));
            Assert.Equal(RelocationType.Capture, pattern.Solve("Kxc3"));
        }

        [Fact]
        public void RelocationTests_CheckIfThePatternMatchesADisambiguatingCaptures_ShouldReturnTrue()
        {
            var pattern = new RelocationPattern();
            Assert.Equal(RelocationType.DisambiguousCapture, pattern.Solve("Rfxc3"));
            Assert.Equal(RelocationType.DisambiguousCapture, pattern.Solve("Nf1xc3"));
            Assert.Equal(RelocationType.DisambiguousCapture, pattern.Solve("Bfxc3"));
            Assert.Equal(RelocationType.DisambiguousCapture, pattern.Solve("Qb2xc3"));
            Assert.Equal(RelocationType.DisambiguousCapture, pattern.Solve("fxc3"));
        }

        [Fact]
        public void RelocationTests_CheckIfThePatternMatchesCastlings_ShouldReturnTrue()
        {
            var pattern = new RelocationPattern();
            Assert.Equal(RelocationType.Castling, pattern.Solve("O-O"));
            Assert.Equal(RelocationType.Castling, pattern.Solve("O-O-O"));
        }

        [Fact]
        public void RelocationTests_CheckIfThePatternMatchesCheckMoves_ShouldReturnTrue()
        {
            var pattern = new RelocationPattern();
            Assert.Equal(RelocationType.Check, pattern.Solve("c5+"));
            Assert.Equal(RelocationType.Check, pattern.Solve("Nc3+"));
            Assert.Equal(RelocationType.Check, pattern.Solve("Rf7+"));
            Assert.Equal(RelocationType.Check, pattern.Solve("Ba5+"));
            Assert.Equal(RelocationType.Check, pattern.Solve("Qg3+"));
        }

        [Fact]
        public void RelocationTests_CheckIfThePatternMatchesAPawnCheckMate_ShouldReturnTrue()
        {
            var pattern = new RelocationPattern();
            Assert.Equal(RelocationType.CheckMate, pattern.Solve("c5#"));
            Assert.Equal(RelocationType.CheckMate, pattern.Solve("Nc3#"));
            Assert.Equal(RelocationType.CheckMate, pattern.Solve("Rf7#"));
            Assert.Equal(RelocationType.CheckMate, pattern.Solve("Ba5#"));
            Assert.Equal(RelocationType.CheckMate, pattern.Solve("Qg3#"));
        }

        [Fact]
        public void RelocationTests_CheckIfThePatternMatchesAPromotion_ShouldReturnTrue()
        {
            var pattern = new RelocationPattern();
            Assert.Equal(RelocationType.Promotion, pattern.Solve("c8=Q"));
            Assert.Equal(RelocationType.CapturingPromotion, pattern.Solve("exc8=Q"));
        }
    }
}