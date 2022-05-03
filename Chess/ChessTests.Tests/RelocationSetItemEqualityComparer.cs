using Chess.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace ChessTests.Tests
{
    internal class RelocationSetItemEqualityComparer : IEqualityComparer<RelocationSet>
    {
        public bool Equals(RelocationSet x, RelocationSet y)
        {
            return x.White == y.White &&
                   x.Black == y.Black &&
                   x.WhiteBorderBrush == y.WhiteBorderBrush &&
                   x.BlackBorderBrush == y.BlackBorderBrush;
        }

        public int GetHashCode([DisallowNull] RelocationSet obj)
        {
            throw new NotImplementedException();
        }
    }
}
