using DependencyInjection;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    public class HighlightedBoardSlotState : BaseStateHandler<BoardSlotState>
    {
        [SerializeField] private UnityEvent<Sprite> onUpdateArt;

        [Inject(InjectSource.Parent)] private IObserverData<CardData> cardObserverData;

        public override void EnterState()
        {
            base.EnterState();
            onUpdateArt?.Invoke(cardObserverData?.Data?.Card.Art.CurrentValue.Value);
        }

        protected override BoardSlotState GetMyState()
        {
            return BoardSlotState.HIGHLIGHTED;
        }
    }
}