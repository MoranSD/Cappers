using System;
using System.Threading.Tasks;

namespace Gameplay.LevelLoad
{
    public interface ILevelLoadService
    {
        event Action OnBeginChangeLocation;
        event Action OnEndChangeLocation;
        bool IsLoading { get; }
        void LoadLocation(int locationId);
        Task LoadLocationAsync(int locationId);
    }
}
