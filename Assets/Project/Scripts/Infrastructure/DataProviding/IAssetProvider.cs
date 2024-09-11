using UnityEngine;

namespace Infrastructure.DataProviding
{
    public interface IAssetProvider
    {
        T Load<T>(string path) where T : Object;
    }
}
