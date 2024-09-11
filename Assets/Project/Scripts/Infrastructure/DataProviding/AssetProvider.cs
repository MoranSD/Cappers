using UnityEngine;

namespace Infrastructure.DataProviding
{
    public class AssetProvider : IAssetProvider
    {
        public T Load<T>(string path) where T : Object
        {
            T asset = Resources.Load<T>(path);

            if (asset == null)
                Debug.LogError($"Не удалось загрузить ресурс по пути: {path}");

            return asset;
        }
    }
}
