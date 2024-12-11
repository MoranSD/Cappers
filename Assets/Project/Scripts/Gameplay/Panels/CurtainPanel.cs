using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Panels
{
    public class CurtainPanel : MonoBehaviour, IPanel
    {
        public PanelType Type => PanelType.curtain;

        [SerializeField] private GameObject curtainObject;

        public async UniTask Hide()
        {
            curtainObject.SetActive(false);
            await UniTask.Delay(0);
        }

        public async UniTask Show()
        {
            curtainObject.SetActive(true);
            await UniTask.Delay(0);
        }
    }
}
