using Chess.Models.Pieces;
using System.Collections.Generic;

namespace Chess.Models.MoveTypes
{
    public class CapturingMove : Move
    {
        private readonly IPlayerRelocationHelper helper;

        public CapturingMove(string move, IEnumerable<IPiece> possibilities, IPlayerRelocationHelper helper) : base(move, possibilities)
        {
            this.helper = helper;
        }

        public override bool Act(RelocationType type)
        {
            return base.Act(type) && helper.TryRemoveFrom(NextPosition);
        }
    }
}
