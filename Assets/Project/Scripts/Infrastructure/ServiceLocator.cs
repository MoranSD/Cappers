using System.Collections.Generic;
using UnityEngine;

namespace Infrastructure
{
    public class ServiceLocator
    {
        private static Dictionary<string, object> services = new Dictionary<string, object>();

        public static void Register<T>(T service)
        {
            var serviceTag = typeof(T).ToString();

            if (!services.ContainsKey(serviceTag))
            {
                services.Add(serviceTag, service);
            }
            else
            {
                Debug.LogError($"ServiceLocator.Register: already have component '{service.GetType()}' with tag={serviceTag}");
            }
        }

        public static bool TryGet<T>(out T service)
        {
            service = Get<T>();
            return service != null;
        }

        public static T Get<T>()
        {
            var serviceTag = typeof(T).ToString();

            if (services.TryGetValue(serviceTag, out var service))
            {
                return (T)service;
            }

            Debug.LogError($"ServiceLocator.Get: not found tag={serviceTag}");
            return default;
        }

        public static void Remove<T>()
        {
            var serviceTag = typeof(T).ToString();

            if (services.ContainsKey(serviceTag))
                services.Remove(serviceTag);
        }
    }
}
