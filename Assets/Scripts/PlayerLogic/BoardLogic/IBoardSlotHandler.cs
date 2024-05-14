namespace SlayTheKing
{
    public interface IBoardSlotHandler
    {
        public BoardSlot BoardSlot { get; }
        public IMinionHandler MinionHandler { get; }
        public IObserverData<CardData> CardObserverData { get; }
    }
}