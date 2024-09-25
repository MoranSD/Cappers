using UnityEngine;

namespace Gameplay.EnemySystem.Fight
{
    public class EnemyFightView : MonoBehaviour, IEnemyFightView
    {
        [SerializeField] private Animator animator;

        public void DrawAttack()
        {
            animator.SetTrigger(EnemyConstants.AttackAnimationName);
        }
    }
}
