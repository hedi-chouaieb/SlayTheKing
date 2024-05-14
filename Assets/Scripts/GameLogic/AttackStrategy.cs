using UnityEngine;

namespace SlayTheKing
{
    public abstract class AttackStrategy : ScriptableObject, IAttackStrategy
    {
        public abstract void ExecuteAttack(IMinionHandler attacker, IBoardSlotHandler[] targets);
    }
}