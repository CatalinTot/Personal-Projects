using Chess.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChessTests.Tests
{
    internal class PieceIconComparer : IEqualityComparer<Icon>
    {
        public bool Equals(Icon x, Icon y)
        {
            var comparer = new IconPositionEqualityComparer();
            return x.IconPath == y.IconPath &&
                   x.Id == y.Id &&
                   comparer.Equals(x.Position, y.Position);
        }

        public int GetHashCode([DisallowNull] Icon obj)
        {
            throw new NotImplementedException();
        }
    }
}
