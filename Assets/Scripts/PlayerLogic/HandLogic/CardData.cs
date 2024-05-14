using UnityEngine;

namespace SlayTheKing
{
    public class CardData : ScriptableObject
    {
        [SerializeField] private Card card;

        public virtual void Reset()
        {
            card.Reset();
        }

        public virtual CardData NewCopy()
        {
            var cardData = CreateInstance<CardData>();
            cardData.Card.CopyFrom(Card);
            return cardData;
        }

        public Card Card { get => card; set => card = value; }
    }
}