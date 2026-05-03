using System;
using UnityEngine;

namespace Code
{
	public class EnemyHealth: Health
	{
		public Action<EnemyHealth> OnDeath;
		[SerializeField] private EnemyAnimator _animator;

		protected override void Die()
		{
			_animator.PlayDeath();
			OnDeath?.Invoke(this);

			Destroy(gameObject);
		}
	}
}