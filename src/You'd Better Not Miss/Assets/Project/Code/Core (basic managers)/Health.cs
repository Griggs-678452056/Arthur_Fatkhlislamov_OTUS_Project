using UnityEngine;

namespace Code
{
    public class Health : MonoBehaviour, IDamageable
    {
        [SerializeField] private float _maxHealth = 100f;

        private float _currentHealth;

        private bool IsDead
        {
            get
            {
                return _currentHealth <= 0;
            }
        }

        private void Awake()
        {
            _currentHealth = _maxHealth;
        }

        public void TakeDamage(float damage)
        {
            if (IsDead)
            {
                return;
            }

            _currentHealth -= damage;

            Debug.Log($"{gameObject.name} получил {damage} очков урона. HP: {_currentHealth}");

            if (_currentHealth <= 0)
            {
                Die();
            }
        }

        protected virtual void Die()
        {
            Debug.Log($"{gameObject} убит");
            Destroy(gameObject);
        }

        public void Heal(float amount)
        {
            if (IsDead)
            {
                return;
            }

            _currentHealth = Mathf.Min(_currentHealth + amount, _maxHealth);
        }
    }
}