using Chess.Models.MoveTypes;

namespace Chess.Models.Pieces
{
    public interface IPiece
    {
        public IBoardPathVerifier Verifier { get; }

        public string Id { get; set; }

        public Position Current { get; set; }

        public bool TryMove(Position upcoming, DisambiguatingData data, RelocationType type);

        public bool TryMove(Position upcoming, RelocationType type);
    }
}
