using UnityEngine;

namespace SlayTheKing
{
    public class OccupiedCardSlotState : BaseStateHandler<CardSlotState>
    {
        protected override CardSlotState GetMyState()
        {
            return CardSlotState.OCCUPIED;
        }
    }
}