using System.Threading.Tasks;

namespace Gameplay.LevelLoad
{
    public interface ILevelLoadService
    {
        bool IsLoading { get; }
        void LoadLocation(int locationId);
        Task LoadLocationAsync(int locationId);
    }
}
