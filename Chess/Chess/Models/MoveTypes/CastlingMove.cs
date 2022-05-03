using System;
using System.Collections.Generic;
using System.Linq;
using Chess.Models.Pieces;

namespace Chess.Models.MoveTypes
{
    public class CastlingMove : IMove
    {
        private readonly IEnumerable<IPiece> rooks;
        private readonly IPiece king;
        private readonly int direction;
        private readonly char file;

        public CastlingMove(string move, IPiece king, IEnumerable<IPiece> rooks)
        {
            VerifiyNullParam(move);
            this.king = king;
            this.rooks = rooks;
            (direction, file) = move.Equals("O-O") ? (1, 'g') : (-1, 'c');
        }

        public bool Act(RelocationType type)
        {
            var kingNext = KingNext(king.Current);
            var rookNext = RookNext(king.Current);
            return rooks.Any(rook => rook.TryMove(rookNext, type)) && king.TryMove(kingNext, type);
        }

        private Position KingNext(Position kingCurrent)
        {
            VerifiyNullParam(kingCurrent);
            return new Position(kingCurrent.Rank, "abcdefgh".IndexOf(file));
        }

        private Position RookNext(Position kingCurrent)
        {
            VerifiyNullParam(kingCurrent);
            return new Position(kingCurrent.Rank, kingCurrent.File + direction);
        }

        private void VerifiyNullParam(object param)
        {
            if (param != null)
            {
                return;
            }

            throw new ArgumentNullException(nameof(param));
        }
    }
}
