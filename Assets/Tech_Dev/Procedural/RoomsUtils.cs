using System.Collections.Generic;
using UnityEngine;

namespace Tech_Dev.Procedural
{
	[System.Serializable]
	public class RoomDifficultyPrefabs
	{
		public List<GameObject> PrefabsChoice;
	}
	
	public enum RoomType
	{
		Fight,
		Merchant,
		Upgrade,
		Gambling,
		Healing,
		Boss
	}

	public enum RoomDifficulty
	{
		Pristine,
		Ruine
	}
    
	public class Room
	{
		public int RoomId;
		
		public RoomType Type;
		public RoomDifficulty RoomDifficulty;
		
		public GameObject RoomPrefab;
		public GameObject WorldInstance;
		public (Room room1, Room room2) ExitRooms;

		public Transform GetRoomEntry()
		{
			foreach (Transform roomElement in WorldInstance.transform)
			{
				if (roomElement.CompareTag("RoomEntry"))
				{
					return roomElement.transform;
				}
			}
			
			Debug.LogError("No room entry found in room: " + RoomId);
			return null;
		}



		public Teleporter GetRoomExitTeleporter()
		{
			return WorldInstance.transform.GetComponentInChildren<Teleporter>();
		}
	}
}