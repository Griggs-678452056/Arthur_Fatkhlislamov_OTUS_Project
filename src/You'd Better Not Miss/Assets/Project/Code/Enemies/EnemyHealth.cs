using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace Code
{
    public class EnemyHealth : Health
    {
        public Action<EnemyHealth> OnDeath;

        [SerializeField] private EnemyAnimator _animator;

        protected override void Die()
        {
            NavMeshAgent agent = GetComponent<NavMeshAgent>();

            if (agent != null)
            {
                agent.isStopped = true;
                agent.ResetPath();
            }

            EnemyAI enemyAI = GetComponent<EnemyAI>();

            if (enemyAI != null)
            {
                enemyAI.SetDead();
                enemyAI.enabled = false;
            }

            _animator.PlayDeath();
            OnDeath?.Invoke(this);

            StartCoroutine(DestroyObject());
        }

        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(3.5f);
            Destroy(gameObject);
        }
    }
}