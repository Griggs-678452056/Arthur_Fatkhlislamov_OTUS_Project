using System;
using System.Collections;
using UnityEngine;

namespace Code
{
    public class PlayerHealth : Health
    {
        [SerializeField] private WinLoseController _winLoseController;
        [SerializeField] private UIController _uiController;

        [SerializeField] private PlayerAnimator _playerAnimator;
        public Action<PlayerHealth> OnDeath;

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
            _playerAnimator.PlayDeath();
            OnDeath?.Invoke(this);

            StartCoroutine(DestroyObject());
        }

        private IEnumerator DestroyObject()
        {
            yield return new WaitForSeconds(4f);
            Destroy(gameObject);
            _winLoseController.LoseGame();
        }
    }
}