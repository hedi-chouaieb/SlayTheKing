using UnityEngine;

namespace SlayTheKing
{
    public class SelectedCardInteractionState : BaseStateHandler<CardInteractionState>
    {
        protected override CardInteractionState GetMyState()
        {
            return CardInteractionState.SELECTED;
        }
    }
}