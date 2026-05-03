using UnityEngine;

namespace Code
{
	public class GameBootstrap: MonoBehaviour
	{
		[SerializeField] private EnemySpawner _spawner;
		[SerializeField] private Transform _player;
		[SerializeField] private WinLoseController _winLoseController;

		private void Start()
		{
			_spawner.Init(_player, _winLoseController);
		}
			}
}