using System;
using UnityEngine;

namespace Infrastructure.GameInput
{
    public interface IInput
    {
        event Action OnPressInteractButton;

        Vector2 MoveInput { get; }
    }
}
