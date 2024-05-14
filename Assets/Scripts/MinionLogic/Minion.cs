using UnityEngine;

namespace SlayTheKing
{
    [System.Serializable]
    public class Minion
    {
        [SerializeField] private Stat<int> attackDamage;
        [SerializeField] private Stat<int> health;
        [SerializeField] private Stat<int> armor;
        [SerializeField] private Stat<int> attackSpeed;
        [SerializeField] private Stat<int[][]> range;
        [SerializeField] private Stat<Ability[]> abilities;
        [SerializeField] private Stat<AttackStrategy> attackStrategy;

        public Minion()
        {
            AttackDamage = new Stat<int>();
            Health = new Stat<int>();
            Armor = new Stat<int>();
            AttackSpeed = new Stat<int>();
            Range = new Stat<int[][]>();
            Abilities = new Stat<Ability[]>();
            AttackStrategy = new Stat<AttackStrategy>();
        }

        public void Reset()
        {
            AttackDamage.Reset();
            Health.Reset();
            Armor.Reset();
            AttackSpeed.Reset();
            Range.Reset();
            Abilities.Reset();
            AttackStrategy.Reset();
        }

        public void CopyFrom(Minion other)
        {
            if (other == null)
            {
                Debug.LogError($"{nameof(other)} {nameof(Minion)} is Null.");
                return;
            }

            AttackDamage.CopyFrom(other.AttackDamage);
            Health.CopyFrom(other.Health);
            Armor.CopyFrom(other.Armor);
            AttackSpeed.CopyFrom(other.AttackSpeed);
            Range.CopyFrom(other.Range);
            Abilities.CopyFrom(other.Abilities);
            AttackStrategy.CopyFrom(other.AttackStrategy);
        }

        public Stat<int> AttackDamage { get => attackDamage; set => attackDamage = value; }
        public Stat<int> Health { get => health; set => health = value; }
        public Stat<int> Armor { get => armor; set => armor = value; }
        public Stat<int> AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
        public Stat<int[][]> Range { get => range; set => range = value; }
        public Stat<Ability[]> Abilities { get => abilities; set => abilities = value; }
        public Stat<AttackStrategy> AttackStrategy { get => attackStrategy; set => attackStrategy = value; }
    }
}