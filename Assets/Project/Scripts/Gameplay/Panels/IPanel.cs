using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.Panels
{
    public interface IPanel
    {
        PanelType Type { get; }
        Task Show(CancellationToken token);
        Task Hide(CancellationToken token);
    }
}
