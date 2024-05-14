using DependencyInjection;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    public class BaseObserverData<T> : MonoBehaviour, IObserverData<T>, IDependencyProvider
    {
        private readonly UnityEvent<T> onUpdateData = new();
        private T data;

        public T Data { get => data; set => SetData(value); }

        public void AddListener(UnityAction<T> action)
        {
            onUpdateData?.AddListener(action);
        }

        public void RemoveListener(UnityAction<T> action)
        {
            onUpdateData?.RemoveListener(action);
        }

        private void SetData(T data)
        {
            this.data = data;
            onUpdateData?.Invoke(this.data);
        }

        [Provide]
        protected IObserverData<T> ProvideObserverData()
        {
            return this;
        }
    }
}