using System.Collections;

namespace Gameplay.Panels
{
    public interface IPanel
    {
        PanelType Type { get; }
        IEnumerator Show();
        IEnumerator Hide();
    }
}
