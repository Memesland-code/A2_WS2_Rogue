using System;
using UnityEngine;

namespace Tech_Dev.Procedural
{
	public class Teleporter : MonoBehaviour
	{
		[SerializeField] private Difficulty _teleporterDifficulty;
		[SerializeField] private Transform _nextRoomEntryPoint;
		[SerializeField] private GameObject _doorGlow;
		private GameObject _spawnedDoorGlow;
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

		private void OnEnable()
		{
			foreach (Transform child in gameObject.transform)
			{
				child.gameObject.SetActive(true);
			}
		}

		private void OnDisable()
		{
			foreach (Transform child in gameObject.transform)
			{
				child.gameObject.SetActive(false);
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
			
			if (nextRoomRef.GetComponent<RoomManager>().Type == Type.Spawn) return;
			transform.GetChild(1).gameObject.GetComponent<MeshRenderer>().sharedMaterial = GameManager.RoomsIcon[_nextRoomRef.GetComponent<RoomManager>().Type];
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