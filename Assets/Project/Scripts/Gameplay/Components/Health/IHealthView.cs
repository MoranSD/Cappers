using System;

namespace Gameplay.Components.Health
{
    public interface IHealthView
    {
        void DrawGetDamage();
        void DrawDie();
    }
}
