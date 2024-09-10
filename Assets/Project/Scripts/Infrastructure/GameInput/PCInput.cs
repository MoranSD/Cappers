using UnityEngine;

namespace Infrastructure.GameInput
{
    public class PCInput : IInput
    {
        public Vector2 MoveInput => new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }
}
