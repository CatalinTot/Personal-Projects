using Chess.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Models.MoveTypes
{
    public class DisambiguatingMove : Move
    {
        public DisambiguatingMove(string move, IEnumerable<IPiece> possibilities) : base(move, possibilities)
        {
            var disambiguatingData = move.Replace("x", string.Empty).Trim("RNBQK".ToCharArray())[0..^(1 + 1)];
            Data = new DisambiguatingData(disambiguatingData);
        }

        public DisambiguatingData Data { get; }

        public override bool Act(RelocationType type)
        {
            return possibilities?.Any(piece => piece.TryMove(NextPosition, Data, type)) == true;
        }
    }
}
