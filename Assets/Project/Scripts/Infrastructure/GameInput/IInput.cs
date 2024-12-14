using System;
using UnityEngine;

namespace Infrastructure.GameInput
{
    public interface IInput
    {
        bool IsInteractButtonPressed { get; }
        bool IsExitButtonPressed { get; }
        bool IsMeleeAttackButtonPressed { get; }
        bool IsRangeAttackButtonPressed { get; }

        event Action OnPressInteractButton;

        event Action OnPressExitButton;

        event Action OnPressMeleeAttackButton;
        event Action OnPressRangeAttackButton;

        Vector2 MoveInput { get; }
    }
}
