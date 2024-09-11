using Infrastructure.Routine;
using System.Collections.Generic;
using System.Collections;
using System;

namespace Infrastructure.Panels
{
    public class PanelsManager
    {
        public PanelType ActivePanel { get; private set; }

        private readonly PanelType defaultPanelType;
        private readonly ICoroutineRunner coroutineRunner;
        private Dictionary<PanelType, IPanel> activePanels;

        public PanelsManager(PanelType defaultPanelType, ICoroutineRunner coroutineRunner)
        {
            activePanels = new Dictionary<PanelType, IPanel>();
            this.defaultPanelType = defaultPanelType;
            this.coroutineRunner = coroutineRunner;
        }

        public void RegisterPanel(IPanel panel)
        {
            if (activePanels.ContainsKey(panel.Type))
                throw new Exception(panel.Type.ToString());

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
        public void ShowPanel(PanelType panelType)
        {
            if (activePanels.ContainsKey(panelType) == false)
                throw new Exception(panelType.ToString());

            coroutineRunner.StartCoroutine(ChangePanelProcess(panelType));
        }
        private IEnumerator ChangePanelProcess(PanelType panelType)
        {
            if (ActivePanel != default)
                yield return activePanels[ActivePanel].Hide();

            yield return activePanels[panelType].Show();

            ActivePanel = panelType;
        }
    }
}
