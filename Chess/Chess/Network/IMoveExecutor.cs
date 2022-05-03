using Chess.Models.Pieces;
using System.Collections.Generic;

namespace Chess.Network
{
    public interface IMoveExecutor
    {
        public bool ExecuteMove(string move, int index, bool direction, out IEnumerable<IPiece> pieces);
    }
}
