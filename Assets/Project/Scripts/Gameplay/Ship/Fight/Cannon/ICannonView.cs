using Gameplay.UnitSystem.Controller;
using System;
using UnityEngine;

namespace Gameplay.Ship.Fight.Cannon
{
    public interface ICannonView
    {
        event Action OnPlayerInteract;
        event Action<IUnitController> OnUnitInteract;

        Transform AimPivot { get; }
        Transform UnitInteractPivot { get; }

        void OnUnitAim();
        void SetAvailable(bool available);
        void BeginAim();
        void EndAim();
        void DrawCannonFly(Action callBack);
    }
}
