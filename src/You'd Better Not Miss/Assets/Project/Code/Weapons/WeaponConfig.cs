using UnityEngine;

namespace Code
{
    [CreateAssetMenu(menuName = "Game/Weapon Config")]
    public class WeaponConfig : ScriptableObject
    {
        [SerializeField, Min(0f)] float _damage = 10f;
        [SerializeField, Min(1)] int _magazineSize = 10;
        [SerializeField, Min(0.01f)] float _fireRate = 0.2f;
        [SerializeField, Min(0f)] float _reloadTime = 1.5f;
        [SerializeField, Min(0f)] float _range = 100f;

        public float Damage => _damage;
        public int MagazineSize => _magazineSize;
        public float FireRate => _fireRate;
        public float ReloadTime => _reloadTime;
        public float Range => _range;
    }
}