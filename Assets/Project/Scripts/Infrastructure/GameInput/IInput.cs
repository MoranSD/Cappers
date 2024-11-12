using System;
using UnityEngine;

namespace Infrastructure.GameInput
{
    public interface IInput
    {
        bool IsInteractButtonPressed { get; }

        event Action OnPressInteractButton;

        event Action OnPressMeleeAttackButton;
        event Action OnPressRangeAttackButton;

        Vector2 MoveInput { get; }
    }
}
