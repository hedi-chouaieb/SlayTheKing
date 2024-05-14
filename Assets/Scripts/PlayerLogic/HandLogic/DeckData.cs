using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    [CreateAssetMenu(fileName = "New Deck Data", menuName = "Deck Data")]
    public class DeckData : ScriptableObject
    {
        [SerializeField] private List<CardData> deck;
        [SerializeField] private UnityEvent onDeckUpdated;

        public void InitializeDeck(List<CardData> initialCards)
        {
            deck = new List<CardData>();
            foreach (var cardData in initialCards)
            {
                deck.Add(cardData.NewCopy());
            }
            ShuffleDeck();
            onDeckUpdated?.Invoke();
        }

        public void InitializeDeck(DeckData other)
        {
            InitializeDeck(other.deck);
        }

        public void AddListener(UnityAction onDeckUpdated)
        {
            this.onDeckUpdated?.AddListener(onDeckUpdated);
        }

        public void RemoveListener(UnityAction onDeckUpdated)
        {
            this.onDeckUpdated?.RemoveListener(onDeckUpdated);
        }

        public void ShuffleCard(CardData card)
        {
            if (deck == null)
            {
                deck = new List<CardData>();
            }

            deck.Add(card);
        }

        public CardData DrawCard()
        {
            if (deck != null && deck.Count > 0)
            {
                CardData card = deck[0];
                deck.RemoveAt(0);
                return card;
            }
            else
            {
                return null; // Or handle empty deck
            }
        }

        public void ShuffleDeck()
        {
            if (deck == null)
            {
                return;
            }

            System.Random rng = new System.Random();
            int n = deck.Count;

            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                CardData temp = deck[k];
                deck[k] = deck[n];
                deck[n] = temp;
            }
        }

        public int GetDeckSize()
        {
            return deck != null ? deck.Count : 0;
        }

        public bool CheckCardAvailability(CardData card)
        {
            return deck != null && deck.Contains(card);
        }

        public void ClearDeck()
        {
            deck?.Clear();
        }

        public void SortDeck()
        {
            // Implement sorting logic based on specific criteria
            // Example: deck.Sort((card1, card2) => card1.Cost.CompareTo(card2.Cost));
        }

        public List<CardData> DrawInitialHand(int handSize)
        {
            if (deck == null)
            {
                return null;
            }
            int count = Mathf.Min(handSize, deck.Count);
            List<CardData> initialHand = deck.GetRange(0, count);
            deck.RemoveRange(0, count);
            return initialHand;
        }

        // public CardData GetCardByID(string cardID)
        // {
        //     return deck?.Find(card => card.ID == cardID);
        // }

        // public void RemoveCardByID(string cardID)
        // {
        //     deck?.RemoveAll(card => card.ID == cardID);
        // }
    }

    // public class DeckManager : MonoBehaviour
    // {
    //     [SerializeField] private DeckData startingDeck;
    //     [SerializeField] private DeckData currentDeck;

    //     public void InitializeDeck()
    //     {
    //         currentDeck.InitializeDeck(startingDeck);
    //     }
    // }
}