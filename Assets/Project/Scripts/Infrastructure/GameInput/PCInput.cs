using Infrastructure.TickManagement;
using System;
using UnityEngine;

namespace Infrastructure.GameInput
{
    public class PCInput : IInput, ITickable
    {
        public Vector2 MoveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        public bool IsInteractButtonPressed => Input.GetKeyDown(interactButton);
        public bool IsExitButtonPressed => Input.GetKeyDown(exitButton);
        public bool MeleeAttackButtonPressed => Input.GetKeyDown(meleeAttackButton);
        public bool RangeAttackButtonPressed => Input.GetKeyDown(rangeAttackButton);

        public event Action OnPressInteractButton;
        public event Action OnPressMeleeAttackButton;
        public event Action OnPressRangeAttackButton;
        public event Action OnPressExitButton;

        private const KeyCode interactButton = KeyCode.E;
        private const KeyCode exitButton = KeyCode.Escape;
        private const KeyCode meleeAttackButton = KeyCode.Mouse0;
        private const KeyCode rangeAttackButton = KeyCode.Mouse1;

        public void Update(float deltaTime)
        {
            if (Input.GetKeyDown(interactButton))
                OnPressInteractButton?.Invoke();

            if (Input.GetKeyDown(exitButton))
                OnPressExitButton?.Invoke();

            if (Input.GetKeyDown(meleeAttackButton))
                OnPressMeleeAttackButton?.Invoke();

            if (Input.GetKeyDown(rangeAttackButton))
                OnPressRangeAttackButton?.Invoke();
        }
    }
}
