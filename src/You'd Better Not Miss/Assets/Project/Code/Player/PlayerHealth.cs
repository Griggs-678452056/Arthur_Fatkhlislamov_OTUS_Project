using UnityEngine;

namespace Code
{
    public class PlayerHealth : Health
    {
        [SerializeField] private WinLoseController _winLoseController;
        [SerializeField] private UIController _uiController;

        private void Start()
        {
            _uiController.SetHealth(CurrentHealth);
        }

        public override void TakeDamage(float damage)
        {
            base.TakeDamage(damage);

            _uiController.SetHealth(CurrentHealth);
        }

        protected override void Die()
        {
            _winLoseController.LoseGame();
            base.Die();
        }
    }
}