using UnityEngine;

namespace SlayTheKing
{
    public class EmptyBoardSlotState : BaseStateHandler<BoardSlotState>
    {
        protected override BoardSlotState GetMyState()
        {
            return BoardSlotState.EMPTY;
        }
    }
}