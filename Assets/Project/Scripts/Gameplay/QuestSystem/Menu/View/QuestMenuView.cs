using Gameplay.Panels;
using Gameplay.QuestSystem.Menu.Factory;
using Gameplay.QuestSystem.Quests;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;
using Utils.Interaction;

namespace Gameplay.QuestSystem.Menu.View
{
    public class QuestMenuView : MonoBehaviour, IQuestMenuView, IPanel
    {
        public event Action<QuestData> OnSelectQuest;
        public event Action OnTryToClose;
        public event Action OnPlayerInteract;

        public PanelType Type => PanelType.questMenu;

        [SerializeField] private GameObject panel;
        [SerializeField] private Transform questWidgetPivot;
        [SerializeField] private Button closeButton;
        [SerializeField] private CameraFollowTriggerInteractor triggerInteractor;

        private List<QuestWidget> activeWidgets;
        private QuestWidgetFactory widgetFactory;

        public void Initialize(QuestWidgetFactory widgetFactory)
        {
            activeWidgets = new();
            this.widgetFactory = widgetFactory;
            closeButton.onClick.AddListener(() => OnTryToClose?.Invoke());
            triggerInteractor.OnInteracted += OnInteract;
        }

        public void Dispose()
        {
            closeButton.onClick.RemoveAllListeners();

            foreach (var widget in activeWidgets)
                widget.InteractButton.onClick.RemoveAllListeners();

            triggerInteractor.OnInteracted -= OnInteract;
        }

        public void RedrawQuests(params QuestData[] datas)
        {
            foreach(var widget in activeWidgets)
            {
                widget.InteractButton.onClick.RemoveAllListeners();
                Destroy(widget.gameObject);
            }

            activeWidgets.Clear();

            for (int i = 0; i < datas.Length; i++)
            {
                var widget = widgetFactory.CreateWidget(datas[i], questWidgetPivot);

                var questData = datas[i];
                widget.InteractButton.onClick.AddListener(() => OnSelectQuest?.Invoke(questData));

                activeWidgets.Add(widget);
            }
        }

        public async Task Show(CancellationToken token)
        {
            panel.SetActive(true);
            await Task.Delay(0);
        }

        public async Task Hide(CancellationToken token)
        {
            panel.SetActive(false);
            await Task.Delay(0);
        }

        private void OnInteract() => OnPlayerInteract?.Invoke();
    }
}
