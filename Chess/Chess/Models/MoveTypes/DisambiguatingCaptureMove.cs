using Chess.Models.Pieces;
using System.Collections.Generic;

namespace Chess.Models.MoveTypes
{
    public class DisambiguatingCaptureMove : DisambiguatingMove
    {
        private readonly IPlayerRelocationHelper capture;

        public DisambiguatingCaptureMove(string move, IEnumerable<IPiece> possibilities, IPlayerRelocationHelper helper) : base(move, possibilities)
        {
            this.capture = helper;
        }

        public override bool Act(RelocationType type)
        {
            return base.Act(type) && capture.TryRemoveFrom(NextPosition);
        }
    }
}
