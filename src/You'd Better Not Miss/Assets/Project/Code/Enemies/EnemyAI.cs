using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Code
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float _attackDistance = 1f;
        [SerializeField] private float _attackCooldown = 2.5f;
        [SerializeField] private float _damage = 5f;
        [SerializeField] private float _detectionDistance = 5f;

        [SerializeField] private EnemyAnimator _animator;

        [SerializeField] private AudioSource _audioSource;
        [SerializeField] private AudioClip[] _idleClips;
        [SerializeField] private AudioClip[] _attackClips;

        private float _idleSoundIntervalMin = 3f;
        private float _idleSoundIntervalMax = 8f;
        private float _nextIdleSoundTime;

        private NavMeshAgent _agent;
        private Transform _target;
        private IDamageable _playerHealth;

        private float _lastAttackTime;

        private bool _isDead;
        public void Init(Transform target)
        {
            if (target == null)
            {
                Debug.LogError("TARGET IS NULL");
                return;
            }

            _target = target;

            _playerHealth = target.GetComponent<IDamageable>();
        }

        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _animator = GetComponent<EnemyAnimator>();
            PlayerHealth.OnPlayerDied += Disable;
        }

        private void Disable()
        {
            enabled = false;
        }

        private void Update()
        {
            if (_target == null)
            {
                return;
            }

            if (_isDead)
            {
                return;
            }

            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance <= _attackDistance)
            {
                _agent.isStopped = true;

                Vector3 lookDirection = _target.position - transform.position;
                lookDirection.y = 0f;

                if (lookDirection.sqrMagnitude > 0.01f)
                {
                    Quaternion targetRotation = Quaternion.LookRotation(lookDirection);
                    transform.rotation = Quaternion.Slerp(
                        transform.rotation,
                        targetRotation,
                        8f * Time.deltaTime);
                }

                _animator.SetSpeed(0);

                TryAttack();
            }
            else if (distance <= _detectionDistance)
            {
                _agent.isStopped = false;

                _agent.SetDestination(_target.position);

                float speed = _agent.velocity.magnitude;

                _animator.SetSpeed(speed);
            }
            else
            {
                _agent.isStopped = true;

                _animator.SetSpeed(0);
            }

            PlayIdleSound();
        }



        private void TryAttack()
        {
            if (Time.time < _lastAttackTime + _attackCooldown)
            {
                return;
            }

            _lastAttackTime = Time.time;

            PlayAttackSound();

            _animator.PlayAttack();
        }

        public void DealDamage()
        {
            if (_target == null)
            {
                return;
            }

            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance > _attackDistance + 0.7f)
            {
                return;
            }

            Vector3 directionToPlayer = (_target.position - transform.position).normalized;

            float angle = Vector3.Angle(
                transform.forward,
                directionToPlayer);

            if (angle > 70)
            {
                return;
            }

            _playerHealth?.TakeDamage(_damage);
        }

        public void SetDead()
        {
            _isDead = true;
        }

        private void PlayIdleSound()
        {
            if (_target == null)
            {
                return;
            }

            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance > _detectionDistance)
            {
                return;
            }

            if (_idleClips == null || _idleClips.Length == 0)
            {
                return;
            }

            if (Time.time < _nextIdleSoundTime)
            {
                return;
            }

            AudioClip clip = _idleClips[Random.Range(0, _idleClips.Length)];

            _audioSource.PlayOneShot(clip);

            _nextIdleSoundTime = Time.time + Random.Range(_idleSoundIntervalMin, _idleSoundIntervalMax);
        }

        private void PlayAttackSound()
        {
            if (_attackClips == null || _attackClips.Length == 0)
            {
                return;
            }

            AudioClip clip = _attackClips[Random.Range(0, _attackClips.Length)];

            _audioSource.PlayOneShot(clip);
        }

        private void OnDestroy()
        {
            PlayerHealth.OnPlayerDied -= Disable;
        }
    }
}