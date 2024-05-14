namespace SlayTheKing
{
    public class HighlightedCardInteractionState : BaseStateHandler<CardInteractionState>
    {
        protected override CardInteractionState GetMyState()
        {
            return CardInteractionState.HIGHLIGHTED;
        }
    }
}