using System.Collections;
using UnityEngine;

namespace Code
{
    public class WeaponRuntime
    {
        public float Damage {  get; private set; }
        public int MagazineSize { get; private set; }

        public int CurrentAmmo { get; private set; }

        public void Init(WeaponConfig config)
        {
            Damage = config.Damage;
            MagazineSize = config.MagazineSize;
            CurrentAmmo = MagazineSize;
        }

        public void UpgradeDamage(float amount)
        {
            Damage += amount;
        }

        public void UpgradeMagazine(int amount)
        {
            MagazineSize += amount;
            CurrentAmmo += amount;
        }

        public bool CanShoot()
        {
            return CurrentAmmo > 0;
        }

        public void ConsumeAmmo()
        {
            CurrentAmmo--;
        }

        public void Reload()
        {
            CurrentAmmo = MagazineSize;
        }
    }
}