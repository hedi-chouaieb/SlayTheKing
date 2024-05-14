using DependencyInjection;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    public class DraggingCardInteractionState : BaseStateHandler<CardInteractionState>
    {
        [SerializeField] private UnityEvent onHideCard;
        [SerializeField] private UnityEvent onDisplayCard;
        [Inject(InjectSource.Parent)] private IObserverData<IBoardSlotInteractions> boardSlotObserverData;
        [Inject(InjectSource.Parent)] private IObserverData<CardData> cardObserverData;
        private IBoardSlotInteractions currentBoardSlot;

        public override void Initialize()
        {
            base.Initialize();
            boardSlotObserverData?.AddListener(BoardSlotUpdated);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            boardSlotObserverData?.RemoveListener(BoardSlotUpdated);
        }

        protected override CardInteractionState GetMyState()
        {
            return CardInteractionState.DRAGGING;
        }

        public void BoardSlotUpdated(IBoardSlotInteractions boardSlot)
        {
            currentBoardSlot?.Exit();
            currentBoardSlot = boardSlot;
            var isHighlighted = currentBoardSlot?.Highlight(cardObserverData?.Data);
            (isHighlighted.HasValue && isHighlighted.Value ? onHideCard : onDisplayCard)?.Invoke();
        }
    }
}