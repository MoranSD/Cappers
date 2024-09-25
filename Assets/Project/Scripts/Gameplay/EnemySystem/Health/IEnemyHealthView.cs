using System;

namespace Gameplay.EnemySystem.Health
{
    public interface IEnemyHealthView
    {
        event Action<float> OnGetDamage;
        void DrawGetDamage();
        void DrawDie();
    }
}
