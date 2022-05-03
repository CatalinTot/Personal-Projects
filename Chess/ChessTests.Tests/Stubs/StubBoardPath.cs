using Chess.Models;
using Chess.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChessTests.Tests.Stubs
{
    internal class StubBoardPath : IBoardPathVerifier
    {
        private readonly bool isFreePath;

        public StubBoardPath(bool isFreePath)
        {
            this.isFreePath = isFreePath;
        }

        public bool IsFreePath(Position current, Position next, bool capture)
        {
            return isFreePath;
        }

        public bool IsFreePath(Position upcoming)
        {
            return isFreePath;
        }

        public bool IsPossibleEnPassant()
        {
            return true;
        }

        public bool TryUpdatePath(Position current, Position next)
        {
            return false;
        }
    }
}
