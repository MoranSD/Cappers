namespace Gameplay.LevelLoad
{
    public interface ILevelLoadService
    {
        bool IsLoading { get; }
        void LoadLocation(int locationId);
    }
}
