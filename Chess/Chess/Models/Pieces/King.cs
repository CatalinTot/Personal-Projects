using Chess.Models.MoveTypes;
using System;

namespace Chess.Models.Pieces
{
    public class King : IPiece
    {
        public King(IBoardPathVerifier verifier)
        {
            Verifier = verifier;
        }

        public IBoardPathVerifier Verifier { get; }

        public bool IsMovedOnce { get; set; }

        public string Id { get; set; }

        public Position Current { get; set; }

        public bool TryMove(Position upcoming, DisambiguatingData data, RelocationType type)
        {
            throw new NotImplementedException();
        }

        public bool TryMove(Position upcoming, RelocationType type)
        {
            return type switch
            {
                RelocationType.Move => TryMoveTo(upcoming),
                RelocationType.Check => IsRightPath(upcoming, true),
                RelocationType.Capture => TryCaptureOn(upcoming),
                RelocationType.Castling => TryCastle(upcoming),
                _ => false
            };
        }

        private bool TryCastle(Position next)
        {
            if (IsMovedOnce)
            {
                return false;
            }

            Verifier.TryUpdatePath(Current, next);
            Current = next;
            return true;
        }

        private bool TryMoveTo(Position next)
        {
            if (!IsRightPath(next, false))
            {
                return false;
            }

            Verifier.TryUpdatePath(Current, next);
            Current = next;
            return true;
        }

        private bool TryCaptureOn(Position next)
        {
            if (!IsRightPath(next, true))
            {
                return false;
            }

            Verifier.TryUpdatePath(Current, next);
            Current = next;
            return true;
        }

        private bool IsRightPath(Position next, bool capture)
        {
            return Math.Abs(Current.File - next.File) <= 1 &&
                   Math.Abs(Current.Rank - next.Rank) <= 1 &&
                   Verifier.IsFreePath(Current, next, capture);
        }
    }
}
