using UnityEngine;

namespace Tech_Dev.Player
{
	public class GroundDetector : MonoBehaviour
	{
		[SerializeField] private float _distance;

		public bool Touched;

		private void Update()
		{
			Touched = Physics.Raycast(transform.position, Vector3.down, _distance);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Touched ? Color.green : Color.yellow;
			Gizmos.DrawRay(transform.position, Vector3.down * _distance);
		}
	}
}
