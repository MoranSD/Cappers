using System;

namespace Gameplay.Components.Health
{
    public interface IHealthView
    {
        event Action<float> OnGetDamage;
        void DrawGetDamage();
        void DrawDie();
    }
}
