using Chess.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChessTests.Tests
{
    public class IconPositionEqualityComparer : IEqualityComparer<Position>
    {
        public bool Equals(Position x, Position y)
        {
            return y.Rank == x.Rank && y.File == x.File;
        }

        public int GetHashCode([DisallowNull] Position obj)
        {
            throw new NotImplementedException();
        }
    }
}
