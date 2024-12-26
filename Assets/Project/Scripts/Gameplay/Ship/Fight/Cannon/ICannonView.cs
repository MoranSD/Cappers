using Gameplay.UnitSystem.Controller;
using System;
using UnityEngine;

namespace Gameplay.Ship.Fight.Cannon
{
    public interface ICannonView
    {
        event Action OnInteracted;
        event Action<IUnitController> OnUnitInteracted;

        Transform AimPivot { get; }
        Transform UnitInteractPivot { get; }

        void OnUnitAim();
        void SetAvailable(bool available);
        void BeginAim();
        void EndAim();
        void DrawCannonFly(Action callBack);
    }
}
