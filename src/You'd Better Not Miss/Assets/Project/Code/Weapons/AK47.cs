using UnityEngine;

namespace Code
{
    public class AK47 : MonoBehaviour, IWeapon
    {
        [Header("References")]
        [SerializeField] private Transform _barrel;
        [SerializeField] private Camera _camera;
        [SerializeField] private GameObject _bulletTracerPrefab;

        [Header("Stats")]
        [SerializeField] private float _range = 100f;

        private float _lastShootTime;

        public void Shoot(float damage)
        {
            Vector3 direction = GetSpreadDirection();

            Ray ray = new Ray(_camera.transform.position, direction);

            if (Physics.Raycast(ray, out RaycastHit hit, _range))
            {
                if (hit.collider.TryGetComponent<IDamageable>(out var dmg))
                {
                    dmg.TakeDamage(damage);
                }

                SpawnTracer(hit.point);
            }
            else
            {
                SpawnTracer(ray.origin + ray.direction * _range);
            }
        }

        private Vector3 GetSpreadDirection()
        {
            Vector3 direction = _camera.transform.forward;

            direction += new Vector3(
                Random.Range(-0.02f, 0.02f),
                Random.Range(-0.02f, 0.02f),
                0f
                );

            return direction.normalized;
        }

        private void SpawnTracer(Vector3 hitPoint)
        {
            if (_bulletTracerPrefab == null || _barrel == null)
            {
                return;
            }

            var tracer = Instantiate(
                _bulletTracerPrefab,
                _barrel.position,
                Quaternion.identity
                );
            tracer.GetComponent<BulletTracer>()?.Init(hitPoint);
        }
    }
}