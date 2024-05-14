using UnityEngine;

namespace SlayTheKing
{
    public class EmptyCardSlotState : BaseStateHandler<CardSlotState>
    {
        protected override CardSlotState GetMyState()
        {
            return CardSlotState.EMPTY;
        }
    }
}