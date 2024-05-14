using UnityEngine;

namespace SlayTheKing
{
    [System.Serializable]
    public class Card
    {
        [SerializeField] private Stat<string> id;
        [SerializeField] private Stat<string> _name;
        [SerializeField] private Stat<string> description;
        [SerializeField] private Stat<int> cost;
        [SerializeField] private Stat<Sprite> art;
        [SerializeField] private Stat<Rarity> rarity;
        [SerializeField] private Stat<int> manaCost;

        public Card()
        {
            Id = new Stat<string>();
            Name = new Stat<string>();
            Description = new Stat<string>();
            Cost = new Stat<int>();
            Art = new Stat<Sprite>();
            Rarity = new Stat<Rarity>();
            ManaCost = new Stat<int>();
        }

        public void Reset()
        {
            Id.Reset();
            Name.Reset();
            Description.Reset();
            Cost.Reset();
            Art.Reset();
            Rarity.Reset();
            ManaCost.Reset();
        }

        public void CopyFrom(Card other)
        {
            if (other == null)
            {
                Debug.LogError($"{nameof(other)} {nameof(Card)} is Null.");
                return;
            }

            Id.CopyFrom(other.Id);
            Name.CopyFrom(other.Name);
            Description.CopyFrom(other.Description);
            Cost.CopyFrom(other.Cost);
            Art.CopyFrom(other.Art);
            Rarity.CopyFrom(other.Rarity);
            ManaCost.CopyFrom(other.ManaCost);
        }

        public Stat<string> Id { get => id; set => id = value; }
        public Stat<string> Name { get => _name; set => _name = value; }
        public Stat<string> Description { get => description; set => description = value; }
        public Stat<int> Cost { get => cost; set => cost = value; }
        public Stat<Sprite> Art { get => art; set => art = value; }
        public Stat<Rarity> Rarity { get => rarity; set => rarity = value; }
        public Stat<int> ManaCost { get => manaCost; set => manaCost = value; }
    }
}