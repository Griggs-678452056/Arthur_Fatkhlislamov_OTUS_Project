using Cysharp.Threading.Tasks;
using System.Threading;
using UnityEngine;

namespace Code
{
    public class GunController : MonoBehaviour
    {
        [SerializeField] private WeaponConfig _weaponConfig;

        [SerializeField] private MonoBehaviour _weaponBehaviour;

        private IWeapon _weapon;
        private PlayerInputHandler _input;
        private WeaponRuntime _runtime = new WeaponRuntime();

        private float _nextFireTime;
        private bool _isReloading;

        private CancellationTokenSource _reloadCts;

        private void Awake()
        {
            _input = GetComponent<PlayerInputHandler>();
            _runtime.Init(_weaponConfig);

            _weapon = _weaponBehaviour as IWeapon;

            if (_weapon == null)
            {
                Debug.LogError("Weapon не реализует IWeapon");
            }
        }

        private void Update()
        {            
            HandleFire();
            HandleReload();
        }

        private void HandleFire()
        {
            if (_isReloading)
            {
                return;
            }

            if (!_input.FirePressed)
            {
                return;
            }

            if (Time.time < _nextFireTime)
            {
                return;
            }

            if (!_runtime.CanShoot())
            {
                Debug.Log("Кончились патроны!");
                return;
            }

            _nextFireTime = Time.time + _weaponConfig.FireRate;

            _runtime.ConsumeAmmo();

            _weapon.Shoot(_runtime.Damage);

            Debug.Log($"Бум! Патронов: {_runtime.CurrentAmmo}");
        }


        private void HandleReload()
        {
            if (_isReloading)
            {
                return;
            }

            if (!_input.ReloadPressed)
            {
                return;
            }

            ReloadAsync().Forget();
        }

        private async UniTask ReloadAsync()
        {
            if (_isReloading)
            {
                return;
            }

            _isReloading = true;

            _reloadCts?.Cancel();
            _reloadCts = new CancellationTokenSource();

            CancellationToken token = _reloadCts.Token;

            Debug.Log("Перезаряжаю...");

            try
            {
                await UniTask.Delay(
                    (int)(_weaponConfig.ReloadTime * 1000),
                    DelayType.DeltaTime,
                    cancellationToken: token
                    );

                _runtime.Reload();

                Debug.Log("Перезарядка завершена");
            }
            catch (System.OperationCanceledException)
            {
                Debug.Log("Отмена перезарядки");
            }
            finally
            {
                _isReloading = false;
            }
        }

        public void UpgradeDamage(float amount)
        {
            _runtime.UpgradeDamage(amount);
        }

        public void UpgradeMagazine(int amount)
        {
            _runtime.UpgradeMagazine(amount);
        }

        private void OnDestroy()
        {
            _reloadCts?.Cancel();
            _reloadCts?.Dispose();
        }
    }
}
