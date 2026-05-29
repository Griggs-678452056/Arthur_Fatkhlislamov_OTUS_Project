using UnityEngine;
using UnityEngine.AI;

namespace Code
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float _attackDistance = 1f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private float _damage = 5f;
        [SerializeField] private float _detectionDistance = 5f;

        [SerializeField] private EnemyAnimator _animator;

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
        }

        private void TryAttack()
        {
            if (Time.time < _lastAttackTime + _attackCooldown)
            {
                return;
            }

            _lastAttackTime = Time.time;

            _animator.PlayAttack();
        }

        public void DealDamage()
        {
            if (_target == null)
            {
                return;
            }

            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance <= _attackDistance + 0.3f)
            {
                _playerHealth?.TakeDamage(_damage);
            }
        }

        public void SetDead()
        {
            _isDead = true;
        }
    }
}