using Infrastructure.TickManagement;
using System;
using UnityEngine;

namespace Infrastructure.GameInput
{
    public class PCInput : IInput, ITickable
    {
        public Vector2 MoveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        public event Action OnPressInteractButton;

        public void Update(float deltaTime)
        {
            if(Input.GetKeyDown(KeyCode.E))
                OnPressInteractButton?.Invoke();
        }
    }
}
