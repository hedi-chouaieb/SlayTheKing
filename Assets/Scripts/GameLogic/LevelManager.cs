using UnityEngine;

namespace SlayTheKing
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private DeckData enemyDeckData;
        [SerializeField] private DeckData currentEnemyDeckData;

        public void SetLevel()
        {
            currentEnemyDeckData.InitializeDeck(enemyDeckData);
        }
    }
}