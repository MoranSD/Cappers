using System;
using UnityEngine;

namespace Gameplay.Ship.Fight.Cannon
{
    public interface ICannonView
    {
        event Action OnPlayerInteract;
        event Action OnUnitInteract;

        Transform AimPivot { get; }

        void SetAvailable(bool available);
        void BeginAim();
        void EndAim();
        void DrawCannonFly(Action callBack);
    }
}
