using Chess.Models.Pieces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Models.MoveTypes
{
    public class CapturingPromotionMove : IMove
    {
        private readonly DisambiguatingCaptureMove capture;
        private readonly char promo;
        private readonly IPlayerRelocationHelper team;
        private readonly IEnumerable<IPiece> possibilities;

        public CapturingPromotionMove(string move, IEnumerable<IPiece> possibilities, IPlayerRelocationHelper rivalHelper, IPlayerRelocationHelper teamHelper)
        {
            if (move == null)
            {
                throw new ArgumentNullException(nameof(move));
            }

            this.team = teamHelper;
            this.possibilities = possibilities;
            promo = move[^1];
            capture = new DisambiguatingCaptureMove(move[..move.IndexOf('=')], possibilities, rivalHelper);
        }

        public bool Act(RelocationType type)
        {
            var piece = possibilities?.FirstOrDefault(piece => piece.TryMove(capture.NextPosition, capture.Data, RelocationType.DisambiguousCapture));
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
