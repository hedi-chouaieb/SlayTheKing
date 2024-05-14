namespace SlayTheKing
{
    public class OccupiedBoardSlotState : BaseStateHandler<BoardSlotState>, IPrepare
    {
        protected override BoardSlotState GetMyState()
        {
            return BoardSlotState.OCCUPIED;
        }
    }
}