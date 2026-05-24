using UnityEngine;
using Random = UnityEngine.Random;

namespace Code
{
    public class TrashMakingService : MonoBehaviour
    {
        [Header("Trash")]
        [SerializeField] private GameObject[] _prefabs;

        [SerializeField] private Transform _spawnPoint;

        [SerializeField] private int _countForSpawn = 15;

        [Header("Spawn Range")]
        [SerializeField] private float _spawnRangeX = 5f;
        [SerializeField] private float _spawnRangeZ = 5f;

        [SerializeField] private float _raycastHeight = 4f;

        [Header("Physics")]
        [SerializeField] private LayerMask _floorMask;
        [SerializeField] private bool _addRigidbodies = true;


        private void Start()
        {
            GenerateTrash();
        }

        private void GenerateTrash()
        {
            if (_prefabs == null || _prefabs.Length == 0)
            {
                Debug.LogWarning("Префабы наполнения комнаты не назначены");
                return;
            }

            for (int i = 0; i < _countForSpawn; i++)
            {
                SpawnTrashObject();
            }
        }

        private void SpawnTrashObject()
        {
            Vector3 randomOffset = new Vector3(
                Random.Range(-_spawnRangeX, _spawnRangeX),
                0f,
                Random.Range(-_spawnRangeZ, _spawnRangeZ)
                );

            Vector3 rayStart =
                _spawnPoint.position +
                randomOffset +
                Vector3.up * _raycastHeight;

            if (Physics.Raycast(
                rayStart,
                Vector3.down,
                out RaycastHit hit,
                _raycastHeight * 2f,
                _floorMask))
            {
                GameObject randomPrefab = _prefabs[Random.Range(0, _prefabs.Length)];

                Quaternion randomRotation =
                    Quaternion.Euler(
                        0f,
                        Random.Range(0f, 360f),
                        0f);

                GameObject spawnedObject = Instantiate(
                    randomPrefab,
                    hit.point,
                    randomRotation,
                    transform);

                Collider collider = spawnedObject.GetComponent<Collider>();

                if (collider == null)
                {
                    BoxCollider boxcollider = spawnedObject.AddComponent<BoxCollider>();

                    Renderer renderer = spawnedObject.GetComponentInChildren<Renderer>();

                    if (renderer != null)
                    {
                        boxcollider.center = spawnedObject.transform.InverseTransformPoint(renderer.bounds.center);

                        boxcollider.size = renderer.bounds.size;
                    }
                }

                if (_addRigidbodies)
                {
                    Rigidbody rigidbody = spawnedObject.GetComponent<Rigidbody>();

                    if (rigidbody == null)
                    {
                        rigidbody = spawnedObject.AddComponent<Rigidbody>();
                    }

                    rigidbody.isKinematic = false;

                    rigidbody.useGravity = true;

                    rigidbody.mass = Random.Range(0.2f, 1f);

                    rigidbody.collisionDetectionMode = CollisionDetectionMode.Discrete;

                    rigidbody.interpolation = RigidbodyInterpolation.Interpolate;
                }
            }
        }
    }
}