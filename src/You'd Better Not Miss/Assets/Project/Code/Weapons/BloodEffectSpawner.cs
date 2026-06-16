using UnityEngine;
using System.Collections;

namespace Code
{
	public class BloodEffectSpawner: MonoBehaviour
	{
		[SerializeField] private GameObject _bloodEffectPrefab;
		
		public void SpawnBlood (Vector3 position, Vector3 normal)
		{
			if (_bloodEffectPrefab == null)
			{
				return;
			}

			Instantiate(
				_bloodEffectPrefab,
				position + normal * 0.02f,
				Quaternion.LookRotation(normal)
				);
        }
	}
}