using UnityEngine;

namespace Gameplay.EnemySystem.Animation
{
    public class EnemyAnimation : MonoBehaviour, IEnemyAnimation
    {
        [SerializeField] private Animator animator;

        public void SetAnimation(string animationName)
        {
            animator.SetTrigger(animationName);
        }
    }
}
