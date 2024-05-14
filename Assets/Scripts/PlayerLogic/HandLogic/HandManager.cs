using System.Collections.Generic;
using System.Linq;
using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class HandManager : MonoBehaviour, IInitialize
    {
        [SerializeField] private DeckData deck;
        [SerializeField] private DeckData currentDeck;
        [SerializeField] private int startingHandCount;

        [Inject(InjectSource.Children)] private IObserverData<CardData>[] playerHand;

        private bool IsHandFull => playerHand.All(cardObserver => cardObserver.Data != null);

        private void OnValidate()
        {
            if (UnityEditor.PrefabUtility.IsPartOfPrefabAsset(gameObject))
                return;
            if (deck == null)
            {
                Debug.LogError($"{nameof(deck)} reference is required.", gameObject);
            }
            if (currentDeck == null)
            {
                Debug.LogError($"{nameof(currentDeck)} reference is required.", gameObject);
            }
        }

        public void Initialize()
        {
            foreach (var card in playerHand)
            {
                card.Data = null;
            }
        }

        public void InitializeHandManager()
        {
            if (deck == null)
            {
                Debug.LogError($"{nameof(deck)} not assigned to HandManager.", gameObject);
                return;
            }
            if (currentDeck == null)
            {
                Debug.LogError($"{nameof(currentDeck)} not assigned to HandManager.", gameObject);
                return;
            }
            currentDeck.InitializeDeck(deck);
            DrawStartingHand(startingHandCount);
        }

        private void DrawStartingHand(int numCards)
        {
            if (currentDeck.GetDeckSize() == 0)
            {
                Debug.LogWarning("Deck is empty. Cannot draw cards.", gameObject);
                return;
            }

            List<CardData> cards = currentDeck.DrawInitialHand(numCards);
            foreach (var card in cards)
            {
                if (!AddCardToHand(card))
                {
                    Debug.LogWarning("Hand is full. Cannot add more cards.", gameObject);
                    return;
                }
            }
        }

        public bool AddCardToHand(CardData card)
        {
            if (IsHandFull)
            {
                Debug.LogWarning("Hand is full. Cannot add more cards.", gameObject);
                return false;
            }

            foreach (var cardObserver in playerHand)
            {
                if (cardObserver.Data == null)
                {
                    cardObserver.Data = card;
                    return true;
                }
            }

            return false; // Hand should not reach here under normal conditions.
        }
    }
}
