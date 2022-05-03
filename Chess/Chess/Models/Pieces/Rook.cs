using Chess.Models.MoveTypes;
using System;

namespace Chess.Models.Pieces
{
    public class Rook : IPiece
    {
        public Rook(IBoardPathVerifier verifier)
        {
            Verifier = verifier;
        }

        public IBoardPathVerifier Verifier { get; }

        public string Id { get; set; }

        public Position Current { get; set; }

        public bool IsMovedOnce { get; set; }

        public bool TryMove(Position upcoming, DisambiguatingData data, RelocationType type)
        {
            return type switch
            {
                RelocationType.DisambiguousMove => TryMoveDisambiguating(data, upcoming),
                RelocationType.DisambiguousCapture => TryCaptureDisambiguating(data, upcoming),
                _ => throw new NotImplementedException()
            };
        }

        public bool TryMove(Position upcoming, RelocationType type)
        {
            return type switch
            {
                RelocationType.Move => TryMoveTo(upcoming),
                RelocationType.Check => IsRightPath(upcoming, true),
                RelocationType.Capture => TryCaptureOn(upcoming),
                RelocationType.Castling => TryCastle(upcoming),
                _ => throw new NotImplementedException()
            };
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
            IsMovedOnce = true;
            return true;
        }

        private bool TryCaptureDisambiguating(DisambiguatingData data, Position next)
        {
            return TryMoveDisambiguating(data, next);
        }

        private bool TryMoveDisambiguating(DisambiguatingData data, Position next)
        {
            return data.IsDisambiguating(this) && TryMoveTo(next);
        }

        private bool TryCastle(Position next)
        {
            if (!IsRightCastlingPath(next))
            {
                return false;
            }

            Verifier.TryUpdatePath(Current, next);
            Current = next;
            return true;
        }

        private bool IsRightPath(Position next, bool capture)
        {
            return (Current.File == next.File ||
                    Current.Rank == next.Rank) &&
                    Verifier.IsFreePath(Current, next, capture);
        }

        private bool IsRightCastlingPath(Position next)
        {
            const int maxCastlingDistance = 3;
            return Math.Abs(Current.File - next.File) <= maxCastlingDistance &&
                   Verifier.IsFreePath(Current, next, false) && !IsMovedOnce;
        }
    }
}
