using System.Collections;

namespace Infrastructure.Panels
{
    public interface IPanel
    {
        PanelType Type { get; }
        IEnumerator Show();
        IEnumerator Hide();
    }
}
