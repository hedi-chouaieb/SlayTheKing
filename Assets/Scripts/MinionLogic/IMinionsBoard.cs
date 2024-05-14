namespace SlayTheKing
{
    public interface IMinionsBoard
    {
        public IBoardSlotHandler[] AllMinions { get; }
        public MinionsHolder PlayerMinions { get; }
        public MinionsHolder EnemyMinions { get; }
        void RefreshMinions();
    }
}