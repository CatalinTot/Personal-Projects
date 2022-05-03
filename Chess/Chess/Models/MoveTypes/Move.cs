using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Models.Pieces;

namespace Chess.Models.MoveTypes
{
    public class Move : IMove
    {
        protected readonly IEnumerable<IPiece> possibilities;

        public Move(string move, IEnumerable<IPiece> possibilities)
        {
            VerifiyNullParam(move);
            move = move.Trim("#+".ToCharArray());
            this.possibilities = possibilities;
            NextPosition = new Position(move[^1], move[^(1 + 1)]);
        }

        public Position NextPosition { get; }

        public virtual bool Act(RelocationType type)
        {
            return possibilities?.Any(piece => piece?.TryMove(NextPosition, type) == true) == true;
        }

        protected void VerifiyNullParam(object param)
        {
            if (param != null)
            {
                return;
            }

            throw new ArgumentNullException(nameof(param));
        }
    }
}
