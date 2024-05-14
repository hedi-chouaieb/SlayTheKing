using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    [System.Serializable]
    public class FieldListener<T>
    {
        [SerializeField] private T value;
        [SerializeField] private UnityEvent<T> listener = new UnityEvent<T>();

        public T Value
        {
            get => value;
            set
            {
                this.value = value;
                listener?.Invoke(this.value);
            }
        }

        public UnityEvent<T> Listener { get => listener; }

        public void CopyFrom(FieldListener<T> other)
        {
            if (other == null)
            {
                Debug.LogError($"{nameof(other)} {nameof(FieldListener<T>)} is Null.");
                return;
            }
            Value = other.Value;
        }

        public static implicit operator T(FieldListener<T> listener)
        {
            return listener.Value;
        }
    }
}