using Chess.Models.Pieces;
using System.Collections.Generic;
using System.Linq;

namespace Chess.Models.MoveTypes
{
    public class CheckMate : Move
    {
        private readonly IPiece rivalKing;
        private readonly IPlayerRelocationHelper capture;

        public CheckMate(string move, IEnumerable<IPiece> possibilities, IPiece rivalKing, IPlayerRelocationHelper helper) : base(move, possibilities)
        {
            this.rivalKing = rivalKing;
            this.capture = helper;
        }

        public override bool Act(RelocationType type)
        {
            var assault = possibilities.FirstOrDefault(piece => piece.TryMove(NextPosition, RelocationType.Move));
            return assault != null && capture.IsCheckMate(rivalKing);
        }
    }
}
