using UnityEngine;

namespace SlayTheKing
{
    [CreateAssetMenu(fileName = "New MinionData", menuName = "Minion Data")]
    public class MinionData : CardData
    {
        [SerializeField] private Minion minion;

        public override void Reset()
        {
            base.Reset();
            minion.Reset();
        }

        public override CardData NewCopy()
        {
            var minionData = CreateInstance<MinionData>();
            minionData.Card = new Card();
            minionData.Minion = new Minion();
            minionData.Card.CopyFrom(Card);
            minionData.Minion.CopyFrom(Minion);
            return minionData;
        }

        public Minion Minion { get => minion; set => minion = value; }
    }
}