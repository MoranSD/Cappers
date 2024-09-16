using UnityEngine;

namespace Infrastructure.Root
{
    public class GameBootStrap : MonoBehaviour
    {
        public Game Game { get; private set; }

        private void Awake()
        {
            DontDestroyOnLoad(this);

            Game = new Game(transform);
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