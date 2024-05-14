using DependencyInjection;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    public abstract class BaseStateHandler<T> : MonoBehaviour, IInitialize, IPrepare, IState where T : struct
    {
        [SerializeField] private UnityEvent onEnter;
        [SerializeField] private UnityEvent onExit;

        [Inject(InjectSource.Parent)] protected IObserverData<T> stateObserverData;
        private bool isActiveState = false;

        public virtual void Prepare()
        {
            if (stateObserverData == null)
            {
                Debug.LogError($" '{nameof(stateObserverData)}' is Null'.", gameObject);
                return;
            }
            stateObserverData.AddListener(CheckToExitState);
        }

        public virtual void Initialize()
        {
            stateObserverData?.AddListener(CheckToEnterState);
        }

        protected virtual void OnDestroy()
        {
            stateObserverData?.RemoveListener(CheckToEnterState);
            stateObserverData?.RemoveListener(CheckToExitState);
        }

        public virtual void EnterState()
        {
            onEnter?.Invoke();
        }

        public virtual void ExitState()
        {
            onExit?.Invoke();
        }

        protected abstract T GetMyState();

        private void CheckToExitState(T state)
        {
            if (!isActiveState || state.Equals(GetMyState()))
            {
                return;
            }
            isActiveState = false;
            ExitState();
        }

        private void CheckToEnterState(T state)
        {
            if (!state.Equals(GetMyState()) || isActiveState)
            {
                return;
            }
            isActiveState = true;
            EnterState();
        }
    }
}
