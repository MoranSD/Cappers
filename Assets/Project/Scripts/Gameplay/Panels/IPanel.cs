using Cysharp.Threading.Tasks;

namespace Gameplay.Panels
{
    public interface IPanel
    {
        PanelType Type { get; }
        UniTask Show();
        UniTask Hide();
    }
}
