using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace SlayTheKing
{
    public static class UnityEventExtensions
    {
        private static Dictionary<UnityAction<string>, object> actionMap = new();

        public static void AddListener<T>(this UnityEvent<T> unityEvent, UnityAction<string> call)
        {
            UnityAction<T> wrappedCall = (T value) => call(value.ToString());

            unityEvent.AddListener(wrappedCall);

            // Store the mapping between original and wrapped actions for later removal
            actionMap[call] = wrappedCall;
            Debug.Log($"{nameof(UnityEventExtensions)} {nameof(AddListener)}");
        }

        public static void RemoveListener<T>(this UnityEvent<T> unityEvent, UnityAction<string> call)
        {
            if (actionMap.TryGetValue(call, out object wrappedAction))
            {
                unityEvent.RemoveListener((UnityAction<T>)wrappedAction);
                actionMap.Remove(call);
            }
            else
            {
                Debug.LogWarning("Listener not found or already removed.");
            }
            Debug.Log($"{nameof(UnityEventExtensions)} {nameof(RemoveListener)}");
        }
    }
}