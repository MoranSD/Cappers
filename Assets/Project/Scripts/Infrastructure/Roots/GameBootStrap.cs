using Infrastructure.SceneLoad;
using Infrastructure.TickManagement;
using Services.GameInput;
using Services.Map;
using UnityEngine;
using World;
using World.Variants;

namespace Infrastructure
{
    public class GameBootStrap : MonoBehaviour, ICoroutineRunner
    {
        private TickManager tickManager;

        private void Awake()
        {
            Application.targetFrameRate = 60;
            DontDestroyOnLoad(gameObject);

            ServiceLocator.Register<IInput>(new PCInput());

            var sceneLoader = new SceneLoader(this);
            ServiceLocator.Register<ISceneLoader>(sceneLoader);

            ServiceLocator.Register<ICoroutineRunner>(this);

            tickManager = new TickManager();
            ServiceLocator.Register(tickManager);

            var port1Location = new PortLocation("Port 1", Vector2.zero);
            var port2Location = new PortLocation("Port 2", Vector2.one);
            var gameWorld = new GameWorld(new Location[]
            {
                port1Location,
                port2Location
            });
            ServiceLocator.Register(gameWorld);

            var worldMapService = new WorldMapService();
            worldMapService.AddLocation(port1Location);
            ServiceLocator.Register(worldMapService);

            sceneLoader.Load(SceneType.ShipAtSea);
        }

        private void Update()
        {
            tickManager.Update(Time.deltaTime);
        }

        private void OnDestroy()
        {
            ServiceLocator.Remove<IInput>();
            ServiceLocator.Remove<ISceneLoader>();
            ServiceLocator.Remove<ICoroutineRunner>();
            ServiceLocator.Remove<TickManager>();
            ServiceLocator.Remove<GameWorld>();
            ServiceLocator.Remove<WorldMapService>();
        }
    }
}