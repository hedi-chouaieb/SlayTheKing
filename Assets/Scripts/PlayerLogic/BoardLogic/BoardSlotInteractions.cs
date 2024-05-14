using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class BoardSlotInteractions : MonoBehaviour, IBoardSlotInteractions
    {
        [Inject(InjectSource.Parent)] private IObserverData<CardData> cardObserverData;
        [Inject(InjectSource.Child)] private IObserverData<BoardSlotState> boardSlotStateObserverData;

        public bool Highlight(CardData cardData)
        {
            if (boardSlotStateObserverData.Data == BoardSlotState.OCCUPIED)
                return false;
            cardObserverData.Data = cardData;
            boardSlotStateObserverData.Data = BoardSlotState.HIGHLIGHTED;
            return true;
        }

        public void Exit()
        {
            if (boardSlotStateObserverData.Data == BoardSlotState.OCCUPIED)
                return;
            cardObserverData.Data = null;
            boardSlotStateObserverData.Data = BoardSlotState.EMPTY;
        }

        public void Play(CardData cardData)
        {
            if (boardSlotStateObserverData.Data == BoardSlotState.OCCUPIED)
                return;
            cardObserverData.Data = cardData;
            boardSlotStateObserverData.Data = BoardSlotState.OCCUPIED;
        }

        public bool IsOccupied()
        {
            return boardSlotStateObserverData.Data == BoardSlotState.OCCUPIED;
        }
    }
}