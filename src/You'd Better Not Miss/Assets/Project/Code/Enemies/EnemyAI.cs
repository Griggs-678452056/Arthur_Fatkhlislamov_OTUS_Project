using UnityEngine;
using UnityEngine.AI;

namespace Code
{
    public class EnemyAI : MonoBehaviour
    {
        [SerializeField] private float _attackDistance = 2f;
        [SerializeField] private float _attackCooldown = 1f;
        [SerializeField] private int _damage = 10;

        [SerializeField] private EnemyAnimator _animator;

        private NavMeshAgent _agent;
        private Transform _target;
        private IDamageable _playerHealth;

        private float _lastAttackTime;

        public void Init(Transform target)
        {
            if (target == null)
            {
                Debug.LogError("TARGET IS NULL");
            }

            _target = target;

            _playerHealth = target.GetComponent<IDamageable>();

            if (_playerHealth == null )
            {
                Debug.LogError("IDamageable NOT FOUND ON PLAYER");
            }
            else
            {
                Debug.Log("Player damageable OK");
            }
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

            _agent.SetDestination(_target.position);

            float speed = _agent.velocity.magnitude;
            _animator.SetSpeed(speed);

            float distance = Vector3.Distance(transform.position, _target.position);

            if (distance <= _attackDistance)
            {
                _animator.PlayAttack();
                TryAttack();
            }
        }

        private void TryAttack()
        {
            if(Time.time < _lastAttackTime + _attackCooldown)
            {
                return;
            }

            _lastAttackTime = Time.time;

            _playerHealth?.TakeDamage(_damage);
        }
    }
}