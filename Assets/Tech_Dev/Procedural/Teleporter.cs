using UnityEngine;

namespace Tech_Dev.Procedural
{
	public class Teleporter : MonoBehaviour
	{
		[SerializeField] private RoomDifficulty _teleporterDifficulty;
		private Transform _roomEntryPoint;

		private void OnDrawGizmos()
		{
			if (_roomEntryPoint != null)
			{
				Gizmos.DrawLine(transform.position, _roomEntryPoint.transform.position);
			}
			else
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(transform.position, 0.75f);
			}
		}

		public Transform GetDestination()
		{
			return _roomEntryPoint;
		}

		public void SetDestinationEntryPoint(Transform destinationTeleporter)
		{
			_roomEntryPoint = destinationTeleporter;
		}

		public RoomDifficulty GetTeleporterDifficulty()
		{
			return _teleporterDifficulty;
		}
	}
}