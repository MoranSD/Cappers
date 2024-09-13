using Infrastructure.Routine;
using System.Collections.Generic;
using System.Collections;
using System;
using UnityEngine;

namespace Gameplay.Panels
{
    public class PanelsManager
    {
        public PanelType ActivePanel { get; private set; }

        private readonly PanelType defaultPanelType;
        private readonly ICoroutineRunner coroutineRunner;
        private Dictionary<PanelType, IPanel> activePanels;

        private bool IsChanging = false;

        public PanelsManager(PanelType defaultPanelType, ICoroutineRunner coroutineRunner)
        {
            activePanels = new Dictionary<PanelType, IPanel>();
            this.defaultPanelType = defaultPanelType;
            this.coroutineRunner = coroutineRunner;
        }

        public void RegisterPanel(IPanel panel)
        {
            activePanels.Add(panel.Type, panel);
        }
        public void UnregisterPanel(IPanel panel)
        {
            if(activePanels.ContainsKey(panel.Type) == false)
                throw new Exception(panel.Type.ToString());

            if(activePanels[panel.Type] != panel)
                throw new Exception(panel.Type.ToString());

            activePanels.Remove(panel.Type);

            if (ActivePanel == panel.Type)
                ActivePanel = default;
        }

        public void ShowDefault() => ShowPanel(defaultPanelType);
        public IEnumerator ShowDefaultRoutine()
        {
            yield return ShowPanelRoutine(defaultPanelType);
        }
        public IEnumerator ShowPanelRoutine(PanelType panelType)
        {
            if (activePanels.ContainsKey(panelType) == false)
                throw new Exception(panelType.ToString());

            yield return new WaitWhile(() => IsChanging);
            yield return ChangePanelProcess(panelType);
        }
        public void ShowPanel(PanelType panelType) => ShowPanelRoutine(panelType);
        private IEnumerator ChangePanelProcess(PanelType panelType)
        {
            IsChanging = true;

            if (ActivePanel != default)
                yield return activePanels[ActivePanel].Hide();

            yield return activePanels[panelType].Show();

            ActivePanel = panelType;
            IsChanging = false;
        }
    }
}
