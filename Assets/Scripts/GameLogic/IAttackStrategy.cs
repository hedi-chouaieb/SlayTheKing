namespace SlayTheKing
{
    public interface IAttackStrategy
    {
        void ExecuteAttack(IMinionHandler attacker, IBoardSlotHandler[] targets);
    }
}