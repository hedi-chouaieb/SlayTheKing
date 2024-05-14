namespace SlayTheKing
{
    public interface IEnemyBoard
    {
        BoardSlot BoardSlotDetails();
        bool IsOccupied();
        void SummonMinion(MinionData minionData);
    }
}