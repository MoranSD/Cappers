using Gameplay.Game;
using Gameplay.LevelLoad;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Gameplay.Travel
{
    public class TravelSystem
    {
        public event Action OnLeaveLocation;
        public event Action OnArriveToLocation;

        public int LastDestinationLocationId { get; private set; }
        public bool IsTraveling { get; private set; }
        public int TravelTimer { get; private set; }

        private const int TravelHalfDurationInSeconds = 1;

        private readonly GameState gameState;
        private readonly ILevelLoadService levelLoadService;

        private CancellationTokenSource cancellationTokenSource;

        public TravelSystem(GameState gameState, ILevelLoadService levelLoadService)
        {
            this.gameState = gameState;
            this.levelLoadService = levelLoadService;
        }

        public void Dispose()
        {
            if (cancellationTokenSource != null)
            {
                cancellationTokenSource.Cancel();
                cancellationTokenSource.Dispose();
                cancellationTokenSource = null;
            }
        }

        public async void BeginTravel(int locationId)
        {
            if (IsTraveling)
                throw new System.Exception();

            if (gameState.OpenedLocations.Contains(locationId) == false)
                throw new System.Exception(locationId.ToString());

            LastDestinationLocationId = locationId;
            IsTraveling = true;

            await TravelProcess(locationId);
        }

        private async Task TravelProcess(int locationId)
        {
            cancellationTokenSource = new();

            if (gameState.IsInSea == false)
            {
                await TimerProcess(TravelHalfDurationInSeconds);
                await levelLoadService.LoadLocationAsync(GameConstants.SeaLocationId, cancellationTokenSource.Token);

                if (cancellationTokenSource.Token.IsCancellationRequested) return;

                OnLeaveLocation?.Invoke();
            }

            await TimerProcess(TravelHalfDurationInSeconds);

            if (cancellationTokenSource.Token.IsCancellationRequested) return;

            await levelLoadService.LoadLocationAsync(locationId, cancellationTokenSource.Token);

            if (cancellationTokenSource.Token.IsCancellationRequested) return;

            gameState.CurrentLocationId = locationId; 
            IsTraveling = false;
            OnArriveToLocation?.Invoke();
        }

        private async Task TimerProcess(int duration)
        {
            TravelTimer = duration;
            var timerWaiter = TimeSpan.FromSeconds(1);

            while (TravelTimer > 0)
            {
                try
                {
                    await Task.Delay(timerWaiter, cancellationTokenSource.Token);
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
