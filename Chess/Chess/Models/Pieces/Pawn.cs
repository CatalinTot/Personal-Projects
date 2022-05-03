using Chess.Models.MoveTypes;
using System;

namespace Chess.Models.Pieces
{
    public class Pawn : IPiece
    {
        public Pawn(IBoardPathVerifier verifier)
        {
            Verifier = verifier;
        }

        public IBoardPathVerifier Verifier { get; }

        public string Id { get; set; }

        public Position Current { get; set; }

        public bool IsMovedOnce { get; set; }

        public bool TryMove(Position upcoming, DisambiguatingData data, RelocationType type)
        {
            return type == RelocationType.DisambiguousCapture && TryCaptureOn(upcoming, data.File);
        }

        public bool TryMove(Position upcoming, RelocationType type)
        {
            return type switch
            {
                RelocationType.Move => TryMoveTo(upcoming),
                RelocationType.Check => IsRightCapturePath(upcoming, Current.File),
                _ => false
            };
        }

        private bool TryMoveTo(Position next)
        {
            if (!IsRightPath(next) || !Verifier.IsFreePath(Current, next, false))
            {
                return false;
            }

            Verifier.TryUpdatePath(Current, next);
            Current = next;
            IsMovedOnce = true;
            return true;
        }

        private bool TryCaptureOn(Position next, int file)
        {
            if (!IsRightCapturePath(next, file))
            {
                return false;
            }

            Verifier.TryUpdatePath(Current, next);
            Current = next;
            return true;
        }

        private bool IsRightPath(Position next)
        {
            const int MaxMove = 2;
            if (next.File != Current.File)
            {
                return false;
            }

            if (!Verifier.IsFreePath(Current, next, false))
            {
                return false;
            }

            var distance = Math.Abs(next.Rank - Current.Rank);
            return (IsMovedOnce && distance == 1) ||
                   (!IsMovedOnce && distance <= MaxMove);
        }

        private bool IsRightCapturePath(Position next, int file)
        {
            return Math.Abs(next.Rank - Current.Rank) == 1 &&
                   Math.Abs(next.File - Current.File) == 1 &&
                   Current.File == file &&
                   Verifier.IsFreePath(Current, next, true);
        }
    }
}
