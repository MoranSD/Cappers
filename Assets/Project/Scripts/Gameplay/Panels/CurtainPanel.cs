using Cysharp.Threading.Tasks;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Panels
{
    public class CurtainPanel : MonoBehaviour, IPanel
    {
        public PanelType Type => PanelType.curtain;

        [SerializeField] private GameObject curtainObject;

        public async UniTask Hide(CancellationToken token)
        {
            curtainObject.SetActive(false);
            await UniTask.Delay(0);
        }

        public async UniTask Show(CancellationToken token)
        {
            curtainObject.SetActive(true);
            await UniTask.Delay(0);
        }
    }
}
