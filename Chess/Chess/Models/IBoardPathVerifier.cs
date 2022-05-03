using Chess.Models.Pieces;

namespace Chess.Models
{
    public interface IBoardPathVerifier
    {
        public bool IsFreePath(Position current, Position upcoming, bool capture);

        public bool IsFreePath(Position upcoming);

        public bool TryUpdatePath(Position current, Position upcoming);

        public bool IsPossibleEnPassant();
    }
}
