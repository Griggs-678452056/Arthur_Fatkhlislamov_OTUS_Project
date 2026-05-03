using UnityEngine;

namespace Code
{
    public class BulletTracer : MonoBehaviour
    {
        private Vector3 _target;
        private float _speed = 300f;
        private TrailRenderer _trail;

        private void Awake()
        {
            _trail = GetComponent<TrailRenderer>();
        }

        public void Init(Vector3 target)
        {
            _target = target;

            if (_trail != null)
            {
                _trail.Clear();
            }

            Destroy(gameObject, 2f);
        }

        private void Update()
        {
            transform.position = Vector3.MoveTowards(
                                transform.position,
                                _target,
                                _speed * Time.deltaTime
                                );

            if (Vector3.Distance( transform.position, _target ) < 0.1f)
            {
                Destroy(gameObject);
            }
        }
    }
}