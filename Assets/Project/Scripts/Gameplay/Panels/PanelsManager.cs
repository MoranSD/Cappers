using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using Utils;
using System.Threading;
using Cysharp.Threading.Tasks;
using Gameplay.LevelLoad;

namespace Gameplay.Panels
{
    public class PanelsManager
    {
        public PanelType? ActivePanel { get; private set; } = null;

        private readonly PanelType defaultPanelType;
        private Dictionary<PanelType, IPanel> activePanels;

        private bool IsChanging = false;
        private CancellationTokenSource cancellationTokenSource;

        public PanelsManager(PanelType defaultPanelType)
        {
            activePanels = new Dictionary<PanelType, IPanel>();
            this.defaultPanelType = defaultPanelType;
        }

        public void Initialize()
        {
            cancellationTokenSource = new();
        }

        public void Dispose()
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
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

        public async void ShowDefault()
        {
            await ShowPanelAsync(defaultPanelType, cancellationTokenSource.Token);
        }
        public async UniTask ShowDefaultAsync(CancellationToken token) => await ShowPanelAsync(defaultPanelType, token);
        public async void ShowPanel(PanelType panelType)
        {
            await ShowPanelAsync(panelType, cancellationTokenSource.Token);
        }
        public async UniTask ShowPanelAsync(PanelType panelType, CancellationToken token)
        {
            if (activePanels.ContainsKey(panelType) == false)
                throw new Exception(panelType.ToString());

            await UniTask.WaitWhile(() => IsChanging, PlayerLoopTiming.Update, token);

            if (token.IsCancellationRequested) return;

            await ChangePanelProcess(panelType, token);
        }
        private async UniTask ChangePanelProcess(PanelType panelType, CancellationToken token)
        {
            IsChanging = true;

            if (ActivePanel != null)
                await activePanels[ActivePanel.Value].Hide(token);

            if(token.IsCancellationRequested) return;

            await activePanels[panelType].Show(token);

            if (token.IsCancellationRequested) return;

            ActivePanel = panelType;
            IsChanging = false;
        }
    }
}
