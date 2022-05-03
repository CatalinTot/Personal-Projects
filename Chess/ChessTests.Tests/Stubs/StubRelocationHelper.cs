using Chess.Models;
using Chess.Models.Pieces;
using System.Collections.Generic;

namespace ChessTests.Tests.Stubs
{
    internal class StubRelocationHelper : IPlayerRelocationHelper
    {
        private readonly List<IPiece> team;

        public StubRelocationHelper(List<IPiece> pieces)
        {
            team = pieces;
        }

        public StubRelocationHelper()
        {
        }

        public bool IsCheckMate(IPiece rivalKing)
        {
            throw new System.NotImplementedException();
        }

        public bool TryRemoveFrom(Position next)
        {
            return true;
        }

        public bool TryPromote(IPiece pawn, IPiece promote)
        {
            team.Remove(pawn);
            team.Add(promote);
            return true;
        }
    }
}
