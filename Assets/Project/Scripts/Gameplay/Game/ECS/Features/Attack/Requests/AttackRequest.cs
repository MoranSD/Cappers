using Leopotam.Ecs;
using System.Collections.Generic;

namespace Gameplay.Game.ECS.Features
{
    public class AttackRequest
    {
        public EcsEntity Sender;
        public bool IsAbleToAttack = true;

        public Dictionary<string, object> ExtensionData;

        public const string TARGET_EXTENSION_DATA_KEY = "Target";
        public const string TARGET_LAYER_EXTENSION_DATA_KEY = "TargetLayer";
    }
}
