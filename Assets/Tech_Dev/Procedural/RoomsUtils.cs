using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace Tech_Dev.Procedural
{
	[System.Serializable]
	public class RoomDifficultyPrefabs
	{
		public List<GameObject> PrefabsChoice;
	}
	
	public enum Type
	{
		Fight,
		Merchant,
		Upgrade,
		Gambling,
		Healing,
		Boss
	}

	public enum Difficulty
	{
		Pristine,
		Ruin,
		None
	}
    
	public class Room
	{
		public int RoomId;
		
		public Type Type;
		public Difficulty Difficulty;
		
		public GameObject RoomPrefab;
		public GameObject WorldInstance;



		public void SetupRoomScript()
		{
			var scriptRef = WorldInstance.AddComponent<RoomDebug>();

			scriptRef.RoomId = RoomId;
			scriptRef.Type = Type;
			scriptRef._difficulty = Difficulty;
		}
		
		

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



		public Teleporter GetPristineTeleporter()
		{
			return WorldInstance.transform.GetChild(0).GetChild(0).transform.GetComponentInChildren<Teleporter>();
		}



		public Teleporter GetRuinTeleporter()
		{
			return WorldInstance.transform.GetChild(0).GetChild(1).transform.GetComponentInChildren<Teleporter>();
		}
	}
}