using DependencyInjection;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    public class CardVisual : MonoBehaviour, IInitialize
    {
        [SerializeField] private UnityEvent<Sprite> onUpdateImage;
        [SerializeField] private UnityEvent<string> onUpdateName;
        [Inject(InjectSource.Parent)] private IObserverData<CardData> cardObserverData;

        public void Initialize()
        {
            cardObserverData?.AddListener(InitializeCardVisual);
        }

        private void OnDestroy()
        {
            cardObserverData?.RemoveListener(InitializeCardVisual);
        }

        public void InitializeCardVisual(CardData card)
        {
            onUpdateImage?.Invoke(card?.Card.Art.CurrentValue.Value);
            onUpdateName?.Invoke(card?.Card.Name.CurrentValue.Value);
        }
    }
}
