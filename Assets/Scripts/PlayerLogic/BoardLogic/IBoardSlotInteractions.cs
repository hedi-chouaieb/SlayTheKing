namespace SlayTheKing
{
    public interface IBoardSlotInteractions
    {
        bool IsOccupied();
        bool Highlight(CardData cardData);
        void Exit();
        void Play(CardData cardData);
    }
}