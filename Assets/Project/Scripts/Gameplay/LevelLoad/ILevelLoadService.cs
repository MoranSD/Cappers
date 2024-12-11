using Cysharp.Threading.Tasks;
using System;

namespace Gameplay.LevelLoad
{
    public interface ILevelLoadService
    {
        event Action OnBeginChangeLocation;
        event Action OnEndChangeLocation;
        bool IsLoading { get; }
        void LoadLocation(int locationId);
        UniTask LoadLocationAsync(int locationId);
    }
}
