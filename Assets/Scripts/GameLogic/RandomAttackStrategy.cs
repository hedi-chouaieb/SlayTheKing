using UnityEngine;

namespace SlayTheKing
{
    [CreateAssetMenu(menuName = "Attack Strategies/Random Attack")]
    public class RandomAttackStrategy : AttackStrategy
    {
        public override void ExecuteAttack(IMinionHandler attacker, IBoardSlotHandler[] targets)
        {
            int targetIndex = Random.Range(0, targets.Length);
            int startingIndex = targetIndex;

            while (!targets[targetIndex].MinionHandler.IsAlive())
            {
                targetIndex = (targetIndex + 1) % targets.Length;
                if (targetIndex == startingIndex)
                {
                    Debug.LogError($"No target is alive", this);
                    return;
                }
            }

            attacker.DealDamage(targets[targetIndex].MinionHandler);
            targets[targetIndex].MinionHandler.DealDamage(attacker);
        }
    }
}