using Chess.Models.Pieces;

namespace Chess.Models
{
    public interface IPlayerRelocationHelper
    {
        public bool TryRemoveFrom(Position upcoming);

        public bool IsCheckMate(IPiece rivalKing);

        public bool TryPromote(IPiece pawn, IPiece promote);
    }
}
