using System.Linq;
using DependencyInjection;
using UnityEngine;

namespace SlayTheKing
{
    public class CombatManger : MonoBehaviour
    {
        [Inject] private IMinionsBoard minionsBoard;

        public void StartCombat()
        {
            minionsBoard.RefreshMinions();
            int playerIndex = 0;
            int enemyIndex = 0;

            var playerMinions = minionsBoard.PlayerMinions.Minions;
            var enemyMinions = minionsBoard.EnemyMinions.Minions;

            while (AreAnyMinionsAlive(playerMinions) && AreAnyMinionsAlive(enemyMinions))
            {
                playerIndex = PerformSideTurn(playerMinions, enemyMinions, playerIndex);
                enemyIndex = PerformSideTurn(enemyMinions, playerMinions, enemyIndex);
            }
        }

        private int PerformSideTurn(IBoardSlotHandler[] attackers, IBoardSlotHandler[] targets, int index)
        {
            while (index < attackers.Length && AreAnyMinionsAlive(targets))
            {
                if (attackers[index].MinionHandler.IsAlive())
                {
                    attackers[index].MinionHandler.Attack(targets);
                    break;
                }
                index++;
            }

            return (index + 1) % attackers.Length;
        }

        private bool AreAnyMinionsAlive(IBoardSlotHandler[] minions)
        {
            foreach (IBoardSlotHandler minion in minions)
            {
                if (minion.MinionHandler.IsAlive())
                {
                    return true;
                }
            }
            return false;
        }
    }
}