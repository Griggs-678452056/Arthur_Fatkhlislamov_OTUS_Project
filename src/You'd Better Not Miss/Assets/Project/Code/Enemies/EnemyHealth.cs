using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public class EnemyHealth : Health
    {
        public Action<EnemyHealth> OnDeath;
        [SerializeField] private EnemyAnimator _animator;

        protected override void Die()
        {
            _animator.PlayDeath();
            OnDeath?.Invoke(this);

            StartCoroutine(DestroyObject());
        }

        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(10f);
            Destroy(gameObject);
        }
    }
}