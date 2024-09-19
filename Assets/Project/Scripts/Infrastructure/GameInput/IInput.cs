using System;
using UnityEngine;

namespace Infrastructure.GameInput
{
    public interface IInput
    {
        event Action OnPressInteractButton;

        event Action OnPressMeleeAttackButton;
        event Action OnPressLongAttackButton;

        Vector2 MoveInput { get; }
    }
}
