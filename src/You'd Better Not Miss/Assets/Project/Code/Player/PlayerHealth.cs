using UnityEngine;

namespace Code
{
    public class PlayerHealth : Health
    {
        [SerializeField] private WinLoseController _winLoseController;

        protected override void Die()
        {
            _winLoseController.LoseGame();
            base.Die();
        }
    }
}