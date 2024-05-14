using System.Linq;

namespace SlayTheKing
{
    public class MinionsHolder
    {
        private IBoardSlotHandler[] minions;
        private IBoardSlotHandler[] front;
        private IBoardSlotHandler[] back;

        public MinionsHolder(IBoardSlotHandler[] minions)
        {
            this.minions = minions;
            this.front = minions.Where(m => m.BoardSlot.Direction == DirectionType.FRONT).ToArray();
            this.back = minions.Where(m => m.BoardSlot.Direction == DirectionType.BACK).ToArray();
        }

        public IBoardSlotHandler[] Minions { get => minions; }
        public IBoardSlotHandler[] Front { get => front; }
        public IBoardSlotHandler[] Back { get => back; }
    }
}