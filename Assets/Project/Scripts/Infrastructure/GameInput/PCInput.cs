using Infrastructure.TickManagement;
using System;
using UnityEngine;

namespace Infrastructure.GameInput
{
    public class PCInput : IInput, ITickable
    {
        public Vector2 MoveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        public event Action OnPressInteractButton;
        public event Action OnPressFightButton;
        public event Action OnPressMeleeAttackButton;
        public event Action OnPressLongAttackButton;

        public void Update(float deltaTime)
        {
            if(Input.GetKeyDown(KeyCode.E))
                OnPressInteractButton?.Invoke();

            if (Input.GetKeyDown(KeyCode.F))
                OnPressFightButton?.Invoke();

            if (Input.GetKeyDown(KeyCode.Mouse0))
                OnPressMeleeAttackButton?.Invoke();

            if (Input.GetKeyDown(KeyCode.Mouse1))
                OnPressLongAttackButton?.Invoke();
        }
    }
}
