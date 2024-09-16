using Gameplay.Game;
using Gameplay.LevelLoad;
using System;
using System.Threading.Tasks;

namespace Gameplay.Travel
{
    public class TravelSystem
    {
        public int DestinationLocationId { get; private set; }
        public bool IsTraveling { get; private set; }
        public int TravelTimer { get; private set; }

        private readonly GameState gameState;
        private readonly ILevelLoadService levelLoadService;

        public TravelSystem(GameState gameState, ILevelLoadService levelLoadService)
        {
            this.gameState = gameState;
            this.levelLoadService = levelLoadService;
        }

        public async void BeginTravel(int locationId)
        {
            if (IsTraveling)
                throw new System.Exception();

            if (gameState.OpenedLocations.Contains(locationId) == false)
                throw new System.Exception(locationId.ToString());

            DestinationLocationId = locationId;
            IsTraveling = true;

            await TravelProcess(locationId);
        }

        private async Task TravelProcess(int locationId)
        {
            if (gameState.IsInSea == false)
            {
                await TimerProcess(3);
                await levelLoadService.LoadLocationAsync(GameConstants.SeaLocationId);
            }

            await TimerProcess(3);
            await levelLoadService.LoadLocationAsync(locationId);

            gameState.CurrentLocationId = locationId; 
            IsTraveling = false;
        }

        private async Task TimerProcess(int duration)
        {
            TravelTimer = duration;
            var timerWaiter = TimeSpan.FromSeconds(1);

            while (TravelTimer > 0)
            {
                await Task.Delay(timerWaiter);
                TravelTimer--;
            }
        }
    }
}
