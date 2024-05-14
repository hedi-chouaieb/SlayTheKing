using System.Reflection;
using DependencyInjection;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    public class PropertyListener<T> : MonoBehaviour, IInitialize
    {
        [SerializeField] protected string propertyToListen = "Health";
        [SerializeField] protected UnityEvent<T> onValueChanged = new UnityEvent<T>();
        [SerializeField] protected UnityEvent<string> onValueChangedString = new UnityEvent<string>();

        [Inject(InjectSource.Parent)] protected IObserverData<CardData> cardObserverData;
        private FieldListener<T> fieldListener;

        public void Initialize()
        {
            if (cardObserverData == null)
            {
                Debug.LogError($"{nameof(IObserverData<CardData>)} not found. Check if the component is attached.", gameObject);
                return;
            }
            cardObserverData.AddListener(InitializePropertyListener);
        }

        protected void OnDestroy()
        {
            fieldListener?.Listener?.RemoveListener(InvokeValueChanged);
            cardObserverData.RemoveListener(InitializePropertyListener);
        }

        protected void InitializePropertyListener(CardData cardData)
        {
            fieldListener?.Listener?.RemoveListener(InvokeValueChanged);
            fieldListener = GetFieldListener();
            if (fieldListener != null) InvokeValueChanged(fieldListener.Value);
            fieldListener?.Listener?.AddListener(InvokeValueChanged);
        }

        protected void InvokeValueChanged(T value)
        {
            onValueChanged?.Invoke(value);
            onValueChangedString?.Invoke(value.ToString());
        }

        private FieldListener<T> GetFieldListener()
        {
            if (cardObserverData == null)
            {
                return null;
            }

            var cardData = cardObserverData.Data;

            if (cardData == null)
            {
                return null;
            }

            PropertyInfo[] properties = cardData.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);

            foreach (var prop in properties)
            {
                PropertyInfo property = prop.PropertyType.GetProperty(propertyToListen);

                if (property == null)
                {
                    continue;
                }

                var currentValueProperty = property.PropertyType.GetProperty(nameof(Stat<T>.CurrentValue));

                if (currentValueProperty == null)
                {
                    continue;
                }

                var currentValue = currentValueProperty.GetValue(property.GetValue(prop.GetValue(cardData)));

                return currentValue as FieldListener<T>;
            }

            return null;
        }
    }
}
