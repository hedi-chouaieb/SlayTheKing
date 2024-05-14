using UnityEngine;

namespace SlayTheKing
{
    [System.Serializable]
    public class Stat<T>
    {
        [SerializeField] private FieldListener<T> defaultValue;
        [SerializeField] private FieldListener<T> maxValue;
        [SerializeField] private FieldListener<T> currentValue;

        public FieldListener<T> DefaultValue => defaultValue;
        public FieldListener<T> MaxValue { get => maxValue; set => maxValue = value; }
        public FieldListener<T> CurrentValue { get => currentValue; set => currentValue = value; }

        public Stat()
        {
            this.defaultValue = new FieldListener<T>();
            this.maxValue = new FieldListener<T>();
            this.currentValue = new FieldListener<T>();
        }

        public Stat(FieldListener<T> defaultValue)
        {
            this.defaultValue = defaultValue;
            this.maxValue = new FieldListener<T>();
            this.currentValue = new FieldListener<T>();
            Reset();
        }

        public Stat(FieldListener<T> defaultValue, FieldListener<T> maxValue, FieldListener<T> currentValue)
        {
            this.defaultValue = defaultValue;
            this.maxValue = maxValue;
            this.currentValue = currentValue;
        }

        public void Reset()
        {
            MaxValue.Value = DefaultValue.Value;
            CurrentValue.Value = DefaultValue.Value;
        }

        public void CopyFrom(Stat<T> other)
        {
            if (other == null)
            {
                Debug.LogError($"{nameof(other)} {nameof(Stat<T>)} is Null.");
                return;
            }
            DefaultValue.CopyFrom(other.DefaultValue);
            MaxValue.CopyFrom(other.MaxValue);
            CurrentValue.CopyFrom(other.CurrentValue);
        }

        public void UpdateCurrent(T val)
        {
            CurrentValue.Value = val;
        }

        public static implicit operator T(Stat<T> stat)
        {
            return stat.currentValue;
        }
    }
}