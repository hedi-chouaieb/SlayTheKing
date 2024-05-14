using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class BoardSlotHandler : MonoBehaviour, IBoardSlotHandler, IDependencyProvider
    {
        [SerializeField] private BoardSlot boardSlot;
        [Inject(InjectSource.Child)] private IMinionHandler minionHandler;
        [Inject(InjectSource.Parent)] private IObserverData<CardData> cardObserverData;

        public BoardSlot BoardSlot => boardSlot;
        public IMinionHandler MinionHandler => minionHandler;

        public IObserverData<CardData> CardObserverData => cardObserverData;

        [Provide]
        private IBoardSlotHandler ProvideBoardSlotHandler()
        {
            return this;
        }
    }
}