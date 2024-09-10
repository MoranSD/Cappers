using UnityEngine;

namespace Infrastructure.Root
{
    public class GameRunner : MonoBehaviour
    {
        [SerializeField] private GameBootStrap bootStrap;

        private void Awake()
        {
            if (FindObjectOfType<GameBootStrap>()) return;

            Instantiate(bootStrap);
        }
    }
}
