using UnityEngine;

namespace SlayTheKing
{
    [System.Serializable]
    public class BoardSlot
    {
        [SerializeField] private int order;
        [SerializeField] private HolderType holder;
        [SerializeField] private DirectionType direction;

        public BoardSlot(int order, HolderType holder, DirectionType direction)
        {
            this.order = order;
            this.holder = holder;
            this.direction = direction;
        }

        public int Order { get => order; }
        public HolderType Holder { get => holder; }
        public DirectionType Direction { get => direction; }
    }
}