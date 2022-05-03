using Chess.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChessTests.Tests
{
    public class RelocationSetEqualityComparer : IEqualityComparer<RelocationSet>
    {
        public bool Equals(RelocationSet x, RelocationSet y)
        {
            return x.White == y.White &&
                   x.Black == y.Black &&
                   x.BlackBorderBrush == y.BlackBorderBrush &&
                   x.WhiteBorderBrush == y.WhiteBorderBrush;
        }

        public int GetHashCode([DisallowNull] RelocationSet obj)
        {
            throw new NotImplementedException();
        }
    }
}
