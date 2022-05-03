namespace Chess.Models.MoveTypes
{
    public interface IMove
    {
        public bool Act(RelocationType type);
    }
}
