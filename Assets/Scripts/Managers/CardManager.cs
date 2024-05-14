using UnityEngine;

namespace SlayTheKing
{
    public class CardManager : MonoBehaviour
    {
        [SerializeField] private CardData[] cardDatas;

        private void Awake()
        {
            ResetCards();
        }

        public void ResetCards()
        {
            foreach (var cardData in cardDatas)
            {
                cardData.Reset();
            }
        }
    }
}