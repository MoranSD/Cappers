using Gameplay.QuestSystem;
using Gameplay.SeaFight.View;
using Gameplay.Travel;
using System.Linq;

namespace Gameplay.SeaFight
{
    public class SeaFightSystem
    {
        public bool IsInFight { get; private set; }

        private readonly TravelSystem travelSystem;
        private readonly QuestManager questManager;
        private readonly ISeaFightView view;

        public SeaFightSystem(TravelSystem travelSystem, QuestManager questManager, ISeaFightView view)
        {
            this.travelSystem = travelSystem;
            this.questManager = questManager;
            this.view = view;
        }

        public void Initialize()
        {
            travelSystem.OnLeaveLocation += OnTravelBegin;
        }

        public void Dispose()
        {
            travelSystem.OnLeaveLocation -= OnTravelBegin;
        }

        public async void BeginFight()
        {
            IsInFight = true;

            await view.ShowNewShip();

            //begin all the stuff
        }

        private void OnTravelBegin()
        {
            if (questManager.ActiveQuests.Any(x => x.QuestType == QuestSystem.Data.QuestType.delivery) == false)
                return;

            travelSystem.Pause();
            BeginFight();
        }

        private void OnFightEnd()
        {
            if (travelSystem.IsTraveling && travelSystem.IsPaused)
                travelSystem.Unpause();

            //hide enemy ship behind like player ship goes forward
            view.HideCurrentShip();
        }
    }
}
