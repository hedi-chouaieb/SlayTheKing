using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class DropCardInteractionState : BaseStateHandler<CardInteractionState>
    {
        [Inject(InjectSource.Parent)] private IObserverData<IBoardSlotInteractions> boardSlotObserverData;
        [Inject(InjectSource.Parent)] private IObserverData<CardData> cardObserverData;
        private Vector3 startingPosition;

        public override void Initialize()
        {
            base.Initialize();
            startingPosition = transform.position;
        }

        public override void EnterState()
        {
            if (boardSlotObserverData.Data != null && !boardSlotObserverData.Data.IsOccupied())
            {
                PlayCard();
            }

            ReturnToHand();
            stateObserverData.Data = CardInteractionState.NONE;
        }

        public void PlayCard()
        {
            boardSlotObserverData.Data.Play(cardObserverData?.Data);
            cardObserverData.Data = null;
            boardSlotObserverData.Data = null;
        }

        public void ReturnToHand()
        {
            transform.position = startingPosition;
        }

        protected override CardInteractionState GetMyState()
        {
            return CardInteractionState.DROP;
        }
    }
}