using UnityEngine;

namespace Code
{
	public class PlayerAnimator: MonoBehaviour
	{
        [SerializeField] private Animator _animator;

        private static readonly int Speed = Animator.StringToHash("Speed");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int Reload = Animator.StringToHash("Reload");
        private static readonly int Die = Animator.StringToHash("Die");

        public void SetSpeed(float value)
        {
            _animator.SetFloat(Speed, value);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(Attack);
        }

        public void PlayReload()
        {
            _animator.SetTrigger(Reload);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(Die);
        }
    }
}