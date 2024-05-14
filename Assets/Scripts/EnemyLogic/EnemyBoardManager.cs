using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class EnemyBoardManager : MonoBehaviour, IInitialize
    {
        [SerializeField] private DeckData currentEnemyDeckData;
        [Inject(InjectSource.All)] private IEnemyBoard[] enemyBoards;

        public void Initialize()
        {
            currentEnemyDeckData.AddListener(DeckUpdated);
        }

        private void OnDestroy()
        {
            currentEnemyDeckData.RemoveListener(DeckUpdated);
        }

        public void DeckUpdated()
        {
            var length = Mathf.Min(currentEnemyDeckData.GetDeckSize(), enemyBoards.Length);
            for (int i = 0; i < length; i++)
            {
                if (enemyBoards[i].IsOccupied()) continue;
                enemyBoards[i].SummonMinion((MinionData)currentEnemyDeckData.DrawCard());
            }
        }
    }
}