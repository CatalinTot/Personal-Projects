using Chess.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChessTests.Tests
{
    public class PiecePositionEqualityComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position x, Position y)
        {
            return x.File == y.File && x.Rank == y.Rank;
        }

        public int GetHashCode([DisallowNull] Position obj)
        {
            throw new NotImplementedException();
        }
    }
}
