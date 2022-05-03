using System.Collections.Generic;
using System.Linq;
using Chess.Models.Pieces;

namespace Chess.Models.MoveTypes
{
    public class PromotionMove : IMove
    {
        private readonly Move relocation;
        private readonly char promo;
        private readonly IPlayerRelocationHelper team;
        private readonly IEnumerable<IPiece> possibilities;

        public PromotionMove(string move, IEnumerable<IPiece> possibilities, IPlayerRelocationHelper helper)
        {
            if (move == null)
            {
                return;
            }

            this.team = helper;
            this.possibilities = possibilities;
            promo = move[^1];
            relocation = new Move(move[..move.IndexOf('=')], possibilities);
        }

        public bool Act(RelocationType type)
        {
            var piece = possibilities.FirstOrDefault(piece => piece.TryMove(relocation.NextPosition, RelocationType.Move));
            if (piece == null)
            {
                return false;
            }

            return team.TryPromote(piece, Promote(piece));
        }

        private IPiece Promote(IPiece piece)
        {
            return promo switch
            {
                'R' => new Rook(piece.Verifier) { Current = piece.Current, Id = piece.Id },
                'N' => new Knight(piece.Verifier) { Current = piece.Current, Id = piece.Id },
                'B' => new Bishop(piece.Verifier) { Current = piece.Current, Id = piece.Id },
                'Q' => new Queen(piece.Verifier) { Current = piece.Current, Id = piece.Id },
                _ => null
            };
        }
    }
}
