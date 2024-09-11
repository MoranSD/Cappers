using Infrastructure.Curtain;
using Infrastructure.Routine;
using UnityEngine;

namespace Infrastructure.Root
{
    public class GameBootStrap : MonoBehaviour, ICoroutineRunner
    {
        public Game Game { get; private set; }

        [SerializeField] private LoadingCurtain loadingCurtain;

        private void Awake()
        {
            DontDestroyOnLoad(this);

            var curtain = Instantiate(loadingCurtain, transform);
            Game = new Game(curtain, this);
            Game.Start();
        }

        private void Update()
        {
            Game.Update(Time.deltaTime);
        }

        private void OnDisable()
        {
            Game.Stop();
        }
    }
}