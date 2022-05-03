using Chess.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Models.MoveTypes
{
    public class CheckMove : Move
    {
        private readonly IPiece rivalKing;

        public CheckMove(string move, IEnumerable<IPiece> possibilities, IPiece rivalKing) : base(move, possibilities)
        {
            this.rivalKing = rivalKing;
        }

        public override bool Act(RelocationType type)
        {
            return possibilities?.Any(piece => piece.TryMove(NextPosition, RelocationType.Move) &&
                                               piece.TryMove(rivalKing.Current, RelocationType.Check)) == true;
        }
    }
}
