using UnityEngine;

namespace Gameplay.Player.Data
{
    [CreateAssetMenu(menuName = "Player/Config")]
    public class PlayerConfigSO : ScriptableObject
    {
        public PlayerConfig MainConfig;
    }
}
