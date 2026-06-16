using UnityEngine;
using System.Collections;

namespace Code
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _zombiePrefab;
        [SerializeField] private Transform[] _spawnPoints;
        [SerializeField] private float _spawnDelay = 1f;

        [SerializeField] private int _minEnemies = 3;
        [SerializeField] private int _maxEnemies = 20;
        private int _totalEnemies;

        private int _spawned;
        private int _alive;

        private Transform _player;
        private WinLoseController _winLoseController;

        private void Awake()
        {
            _minEnemies = Mathf.Max(_minEnemies, _maxEnemies - 10);
            _maxEnemies = Mathf.Max(_minEnemies + 1, _maxEnemies);
        }

        public void Init(Transform player, WinLoseController winLoseController)
        {
            _player = player;
            _winLoseController = winLoseController;

            _totalEnemies = Random.Range(_minEnemies, _maxEnemies);

            StartCoroutine(SpawnRoutine());
        }

        private IEnumerator SpawnRoutine()
        {
            while (_spawned < _totalEnemies)
            {
                Spawn();
                _spawned++;
                _alive++;

                yield return new WaitForSeconds(_spawnDelay);
            }
        }

        private void Spawn()
        {
            var point = _spawnPoints[Random.Range(0, _spawnPoints.Length)];

            var enemyGO = Instantiate(_zombiePrefab, point.position, Quaternion.identity);

            var ai = enemyGO.GetComponent<EnemyAI>();
            ai.Init(_player);

            var health = enemyGO.GetComponent<EnemyHealth>();
            health.OnDeath += OnEnemyDeath;
        }

        private void OnEnemyDeath(EnemyHealth enemy)
        {
            enemy.OnDeath -= OnEnemyDeath;

            _alive--;

            if (_spawned >= _totalEnemies && _alive <= 0)
            {
                StartCoroutine(WinRoutine());
            }
        }

        private IEnumerator WinRoutine()
        {
            yield return new WaitForSeconds(3.5f);
            _winLoseController.WinGame();
        }
    }
}