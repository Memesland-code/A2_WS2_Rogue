using UnityEngine;

namespace Tech_Dev.Player
{
	public class GroundDetector : MonoBehaviour
	{
		[SerializeField] private float _distance;

		public bool Touched;

		private void Update()
		{
			Touched = Physics.Raycast(transform.position + new Vector3(0, 1, 0), Vector3.down, _distance + 1);
		}

		private void OnDrawGizmos()
		{
			Gizmos.color = Touched ? Color.green : Color.yellow;
			Gizmos.DrawRay(transform.position, Vector3.down * _distance);
		}
	}
}
