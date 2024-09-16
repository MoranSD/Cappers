using System.Threading.Tasks;

namespace Gameplay.Panels
{
    public interface IPanel
    {
        PanelType Type { get; }
        Task Show();
        Task Hide();
    }
}
