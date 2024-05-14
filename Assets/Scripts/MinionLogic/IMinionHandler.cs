namespace SlayTheKing
{
    public interface IMinionHandler : IReceiveDamage, IDealDamage
    {
        // public void InitializeMinion(CardData cardData);
        bool IsAlive();
        bool Attack(IBoardSlotHandler[] targets);
    }
}