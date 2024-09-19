using System;

namespace Gameplay.Player.Data
{
    [Serializable]
    public class PlayerConfig
    {
        public PlayerMovementConfig MovementConfig;
        public PlayerLookConfig LookConfig;
        public PlayerFightConfig FightConfig;
    }
}
