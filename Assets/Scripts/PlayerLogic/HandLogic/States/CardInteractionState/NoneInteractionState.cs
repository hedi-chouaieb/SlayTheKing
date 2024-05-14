using UnityEngine;

namespace SlayTheKing
{
    public class NoneInteractionState : BaseStateHandler<CardInteractionState>
    {
        protected override CardInteractionState GetMyState()
        {
            return CardInteractionState.NONE;
        }
    }
}