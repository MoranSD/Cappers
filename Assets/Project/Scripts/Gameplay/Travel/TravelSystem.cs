using Cysharp.Threading.Tasks;
using Gameplay.Game;
using Gameplay.LevelLoad;
using System;

namespace Gameplay.Travel
{
    public class TravelSystem
    {
        public event Action OnLeaveLocation;
        public event Action OnArriveToLocation;

        public int LastDestinationLocationId { get; private set; }
        public bool IsTraveling { get; private set; }
        public bool IsPaused { get; private set; }
        public int TravelTimer { get; private set; }

        private const int TravelHalfDurationInSeconds = 3;

        private readonly GameState gameState;
        private readonly ILevelLoadService levelLoadService;

        public TravelSystem(GameState gameState, ILevelLoadService levelLoadService)
        {
            this.gameState = gameState;
            this.levelLoadService = levelLoadService;
        }

        public void Dispose()
        {
        }

        public void BeginTravel(int locationId)
        {
            if (IsTraveling)
                throw new System.Exception();

            if (gameState.OpenedLocations.Contains(locationId) == false)
                throw new System.Exception(locationId.ToString());

            LastDestinationLocationId = locationId;
            IsTraveling = true;

            TravelProcess(locationId);
        }

        public void Pause()
        {
            if (IsTraveling == false) throw new System.Exception();
            if (IsPaused) throw new System.Exception();

            IsPaused = true;
        }

        public void Unpause()
        {
            if (IsTraveling == false) throw new System.Exception();
            if (IsPaused == false) throw new System.Exception();

            IsPaused = false;
        }

        private async void TravelProcess(int locationId)
        {
            if (gameState.IsInSea == false)
            {
                await TimerProcess(TravelHalfDurationInSeconds);

                await levelLoadService.LoadLocationAsync(GameConstants.SeaLocationId);

                OnLeaveLocation?.Invoke();
            }

            await TimerProcess(TravelHalfDurationInSeconds);

            await levelLoadService.LoadLocationAsync(locationId);

            gameState.CurrentLocationId = locationId; 
            IsTraveling = false;
            OnArriveToLocation?.Invoke();
        }

        private async UniTask TimerProcess(int duration)
        {
            TravelTimer = duration;
            var timerWaiter = TimeSpan.FromSeconds(1);

            while (TravelTimer > 0)
            {
                try
                {
                    await UniTask.Delay(timerWaiter);
                    await UniTask.WaitWhile(() => IsPaused);

                    TravelTimer--;
                }
                catch
                {
                    return;
                }
            }
        }
    }
}
