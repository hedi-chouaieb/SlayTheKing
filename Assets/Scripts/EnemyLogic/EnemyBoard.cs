using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class EnemyBoard : MonoBehaviour, IEnemyBoard, IDependencyProvider
    {
        [Inject(InjectSource.Child)] private IObserverData<BoardSlotState> boardSlotStateObserverData;
        [Inject(InjectSource.Parent)] private IBoardSlotHandler boardSlotHandler;

        public BoardSlot BoardSlotDetails() => boardSlotHandler.BoardSlot;

        public bool IsOccupied() => boardSlotStateObserverData.Data == BoardSlotState.OCCUPIED;

        [Provide]
        private IEnemyBoard ProvideEnemyBoard()
        {
            return this;
        }

        public void SummonMinion(MinionData minionData)
        {
            if (boardSlotStateObserverData.Data == BoardSlotState.OCCUPIED)
            {
                Debug.LogError($"{nameof(EnemyBoard)} is already occupied.", gameObject);
                return;
            }

            boardSlotHandler.CardObserverData.Data = minionData;
            boardSlotStateObserverData.Data = BoardSlotState.OCCUPIED;
        }
    }
}