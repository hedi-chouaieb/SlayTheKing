using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    // CardSlot: deals with the mechanics of where the card resides or can be placed, managing the slot's state and functionality.
    // Would manage the spaces in the player's hand or on the game board where cards can be placed, 
    // potentially handling multiple cards or their positions within a specific area.

    // Responsibility: CardSlot manages the space or location where a card resides, 
    // often within the player's hand or on the game board.

    // Functionality: It handles the logic related to the slot itself, 
    // such as allowing a card to be placed in it, positioning cards, 
    // or maintaining the state of the slot (occupied or empty).

    // Example Functions: It might include functions to check if the slot is available for a card, 
    // to place a card into the slot, or to trigger actions when a card is removed.
    public class CardSlot : MonoBehaviour, IInitialize
    {
        [Inject(InjectSource.Parent)] private IObserverData<CardData> cardObserverData;
        [Inject(InjectSource.Parent)] private IObserverData<CardSlotState> cardSlotStateObserverData;

        public void Initialize()
        {
            cardObserverData.AddListener(UpdateCardSlot);
        }

        private void OnDestroy()
        {
            cardObserverData.RemoveListener(UpdateCardSlot);
        }

        public void UpdateCardSlot(CardData data)
        {
            if (data == null)
            {
                cardSlotStateObserverData.Data = CardSlotState.EMPTY;
                return;
            }

            cardSlotStateObserverData.Data = CardSlotState.OCCUPIED;
        }
    }
}