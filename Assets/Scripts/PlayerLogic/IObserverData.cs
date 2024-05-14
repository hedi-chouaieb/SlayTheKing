using UnityEngine.Events;

namespace SlayTheKing
{
    public interface IObserverData<T>
    {
        void AddListener(UnityAction<T> action);
        void RemoveListener(UnityAction<T> action);
        T Data { get; set; }
    }
}