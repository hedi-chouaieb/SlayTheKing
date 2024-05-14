using System.Linq;
using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class MinionsBoard : MonoBehaviour, IMinionsBoard, IDependencyProvider
    {
        [Inject(InjectSource.All)] private IBoardSlotHandler[] allMinions;
        private MinionsHolder playerMinions;
        private MinionsHolder enemyMinions;

        public IBoardSlotHandler[] AllMinions { get => allMinions; }
        public MinionsHolder PlayerMinions { get => playerMinions; }
        public MinionsHolder EnemyMinions { get => enemyMinions; }

        public void RefreshMinions()
        {
            // allMinions = GetComponentsInChildren<IBoardSlotHandler>(true);
            playerMinions = new MinionsHolder(allMinions.Where(m => m.BoardSlot.Holder == HolderType.PLAYER).ToArray());
            enemyMinions = new MinionsHolder(allMinions.Where(m => m.BoardSlot.Holder == HolderType.ENEMY).ToArray());
        }

        [Provide]
        private IMinionsBoard ProvideMinionsBoard()
        {
            return this;
        }
    }
}