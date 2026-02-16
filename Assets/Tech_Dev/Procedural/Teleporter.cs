using UnityEngine;

namespace Tech_Dev.Procedural
{
	public class Teleporter : MonoBehaviour
	{
		[SerializeField] private Difficulty _teleporterDifficulty;
		[SerializeField] private Transform _nextRoomEntryPoint;
		private GameObject _nextRoomRef;
		public bool TeleportToHub;

		private void OnDrawGizmos()
		{
			if (_nextRoomEntryPoint != null)
			{
				Gizmos.DrawLine(transform.position, _nextRoomEntryPoint.transform.position);
			}
			else
			{
				Gizmos.color = Color.red;
				Gizmos.DrawSphere(transform.position, 0.75f);
			}
		}

		public Transform GetDestination()
		{
			return _nextRoomEntryPoint;
		}

		public void SetDestinationEntryPoint(GameObject nextRoomRef, Transform destinationTeleportPoint)
		{
			_nextRoomRef = nextRoomRef;
			_nextRoomEntryPoint = destinationTeleportPoint;
		}

		public Difficulty GetTeleporterDifficulty()
		{
			return _teleporterDifficulty;
		}
		
		public RoomManager GetNextRoomRef()
		{
			return _nextRoomRef.GetComponent<RoomManager>();
		}
	}
}