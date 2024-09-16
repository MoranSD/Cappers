﻿using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Utils;

namespace Gameplay.Panels
{
    public class PanelsManager
    {
        public PanelType? ActivePanel { get; private set; } = null;

        private readonly PanelType defaultPanelType;
        private Dictionary<PanelType, IPanel> activePanels;

        private bool IsChanging = false;

        public PanelsManager(PanelType defaultPanelType)
        {
            activePanels = new Dictionary<PanelType, IPanel>();
            this.defaultPanelType = defaultPanelType;
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
                ActivePanel = null;
        }

        public async void ShowDefault() => await ShowPanelAsync(defaultPanelType);
        public async Task ShowDefaultAsync() => await ShowPanelAsync(defaultPanelType);
        public async void ShowPanel(PanelType panelType) => await ShowPanelAsync(panelType);
        public async Task ShowPanelAsync(PanelType panelType)
        {
            if (activePanels.ContainsKey(panelType) == false)
                throw new Exception(panelType.ToString());

            await TaskUtils.WaitWhile(() => IsChanging);

            await ChangePanelProcess(panelType);
        }
        private async Task ChangePanelProcess(PanelType panelType)
        {
            IsChanging = true;

            if (ActivePanel != null)
                await activePanels[ActivePanel.Value].Hide();

            await activePanels[panelType].Show();

            ActivePanel = panelType;
            IsChanging = false;
        }
    }
}
