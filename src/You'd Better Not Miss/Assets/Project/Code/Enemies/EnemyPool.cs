using System.Collections.Generic;
using UnityEngine;

namespace Code
{
    public class EnemyPool : MonoBehaviour
    {
        [SerializeField] private GameObject _zombiePrefab;
        [SerializeField] private int _size = 20;

        private Queue<GameObject> _pool = new Queue<GameObject>();

        private void Awake()
        {
            for (int i = 0; i < _size; i++)
            {
                var zombie = Instantiate(_zombiePrefab);
                zombie.SetActive(false);
                _pool.Enqueue(zombie);
            }
        }

        public GameObject Get(Vector3 position, Quaternion rotation)
        {
            GameObject obj = _pool.Count > 0
                ? _pool.Dequeue()
                : Instantiate(_zombiePrefab);

            obj.transform.SetPositionAndRotation(position, rotation);
            obj.SetActive(true);

            return obj;
        }

        public void Return(GameObject obj)
        {
            //obj.GetComponent<EnemyAI>()?.ResetEnemy();

            obj.SetActive(false);
            _pool.Enqueue(obj);
        }
    }
}