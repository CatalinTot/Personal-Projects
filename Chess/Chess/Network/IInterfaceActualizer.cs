using Chess.Models.Pieces;
using System.Collections.Generic;

namespace Chess.Network
{
    public interface IInterfaceActualizer
    {
        public void UpdateInterface(IEnumerable<IPiece> pieces);
    }
}
