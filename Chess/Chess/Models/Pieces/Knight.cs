using Chess.Models.MoveTypes;
using System.Linq;

namespace Chess.Models.Pieces
{
    public class Knight : IPiece
    {
        public Knight(IBoardPathVerifier verifier)
        {
            Verifier = verifier;
        }

        public IBoardPathVerifier Verifier { get; }

        public string Id { get; set; }

        public Position Current { get; set; }

        public bool TryMove(Position upcoming, DisambiguatingData data, RelocationType type)
        {
            return type switch
            {
                RelocationType.DisambiguousMove => TryMoveDisambiguating(data, upcoming),
                RelocationType.DisambiguousCapture => TryCaptureDisambiguating(data, upcoming),
                _ => false
            };
        }

        public bool TryMove(Position upcoming, RelocationType type)
        {
            return type switch
            {
                RelocationType.Move => TryMoveTo(upcoming, false),
                RelocationType.Check => IsRightPath(upcoming, true),
                RelocationType.Capture => TryCaptureOn(upcoming),
                _ => false
            };
        }

        private bool TryMoveTo(Position next, bool capture)
        {
            if (!IsRightPath(next, capture))
            {
                return false;
            }

            Verifier.TryUpdatePath(Current, next);
            Current = next;
            return true;
        }

        private bool TryCaptureOn(Position next)
        {
            return TryMoveTo(next, true);
        }

        private bool TryCaptureDisambiguating(DisambiguatingData data, Position next)
        {
            return data.IsDisambiguating(this) && TryMoveTo(next, true);
        }

        private bool TryMoveDisambiguating(DisambiguatingData data, Position next)
        {
            return data.IsDisambiguating(this) && TryMoveTo(next, false);
        }

        private bool IsRightPath(Position next, bool capture)
        {
            int[] pattern = new[] { 2, -2, 1, -1 };
            var canMove = pattern.SelectMany(row => pattern
                                 .Except(new[] { row, row * -1 })
                                 .Select(col => new Position(Current.Rank + row, Current.File + col)))
                                 .Any(position => position.Equals(next));

            if (!capture)
            {
                return canMove && Verifier.IsFreePath(next);
            }

            return canMove;
        }
    }
}
