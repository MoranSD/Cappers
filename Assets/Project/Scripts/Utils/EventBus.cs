using System.Collections.Generic;
using System;
using UnityEngine;
using System.Linq;

namespace Utils
{
    public class CallbackWithPriority
    {
        /// <summary>
        /// Чем выше Priority, тем раньше вызовется ивент
        /// </summary>
        public readonly int Priority;
        public readonly object Callback;

        public CallbackWithPriority(int priority, object callback)
        {
            Priority = priority;
            Callback = callback;
        }
    }
    public static class EventBus
    {
        private static Dictionary<string, List<CallbackWithPriority>> _signalCallbacks = new Dictionary<string, List<CallbackWithPriority>>();

        public static void Subscribe<T>(Action<T> callback, int priority = 0)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                _signalCallbacks[key].Add(new CallbackWithPriority(priority, callback));
            }
            else
            {
                _signalCallbacks.Add(key, new List<CallbackWithPriority>() { new(priority, callback) });
            }

            _signalCallbacks[key] = _signalCallbacks[key].OrderByDescending(x => x.Priority).ToList();
        }

        public static void Invoke<T>(T signal)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                foreach (var obj in _signalCallbacks[key])
                {
                    var callback = obj.Callback as Action<T>;
                    callback?.Invoke(signal);
                }
            }
        }

        public static void Unsubscribe<T>(Action<T> callback)
        {
            string key = typeof(T).Name;
            if (_signalCallbacks.ContainsKey(key))
            {
                var callbackToDelete = _signalCallbacks[key].FirstOrDefault(x => x.Callback.Equals(callback));
                if (callbackToDelete != null)
                {
                    _signalCallbacks[key].Remove(callbackToDelete);
                }
            }
            else
            {
                Debug.LogErrorFormat("Trying to unsubscribe for not existing key! {0} ", key);
            }
        }
    }
}
