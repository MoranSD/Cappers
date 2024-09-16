﻿using System.Threading.Tasks;
using UnityEngine;

namespace Gameplay.Panels
{
    public class CurtainPanel : MonoBehaviour, IPanel
    {
        public PanelType Type => PanelType.curtain;

        [SerializeField] private GameObject curtainObject;

        public async Task Hide()
        {
            curtainObject.SetActive(false);
            await Task.Delay(0);
        }

        public async Task Show()
        {
            curtainObject.SetActive(true);
            await Task.Delay(0);
        }
    }
}
