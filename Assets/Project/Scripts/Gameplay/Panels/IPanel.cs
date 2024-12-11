using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.Panels
{
    public interface IPanel
    {
        PanelType Type { get; }
        UniTask Show(CancellationToken token);
        UniTask Hide(CancellationToken token);
    }
}
