﻿using Gameplay.Components.Health;
using Gameplay.Player.Fight;
using Gameplay.Player.Look;
using Gameplay.Player.Movement;
using UnityEngine;

namespace Gameplay.Player.View
{
    public interface IPlayerView
    {
        PlayerController Controller { get; }
        Transform UnitFollowPivot { get; }
        IPlayerMovement Movement { get; }
        IPlayerLookView Look { get; }
        IPlayerFightView Fight { get; }
        IHealthView Health { get; }
    }
}
