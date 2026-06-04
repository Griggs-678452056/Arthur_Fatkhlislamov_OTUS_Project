using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Code
{
    public class EnemyHealth : Health
    {
        public Action<EnemyHealth> OnDeath;

        [SerializeField] private EnemyAnimator _animator;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _hitClips;

        private float _nextHitSoundTime;

        public override void TakeDamage(float damage)
        {
            if (IsDead)
            {
                return;
            }

            PlayHitSound();

            base.TakeDamage(damage);
        }

        private void PlayHitSound()
        {
            if (Time.time < _nextHitSoundTime)
            {
                return;
            }

            if (_hitClips == null || _hitClips.Length == 0)
            {
                return;
            }

            AudioClip clip = _hitClips[Random.Range(0, _hitClips.Length)];

            _audioSource.PlayOneShot(clip);

            _nextHitSoundTime = Time.time + 0.3f;
        }

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