using System.Collections.Generic;
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
			RoomManager scriptRef = WorldInstance.AddComponent<RoomManager>();

			scriptRef.RoomId = RoomId;
			scriptRef.Type = Type;
			scriptRef.Difficulty = Difficulty;
			
			if (!scriptRef.enabled) scriptRef.enabled = true;
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
			foreach (Transform child in WorldInstance.transform)
			{
				if (child.CompareTag("TeleportersContainer"))
				{
					foreach (Transform teleporter in child.transform)
					{
						if (teleporter.CompareTag("TeleporterPristine"))
						{
							return teleporter.GetComponent<Teleporter>();
						}
					}
				}
			}

			return null;
		}



		public Teleporter GetRuinTeleporter()
		{
			foreach (Transform child in WorldInstance.transform)
			{
				if (child.CompareTag("TeleportersContainer"))
				{
					foreach (Transform teleporter in child.transform)
					{
						if (teleporter.CompareTag("TeleporterRuin"))
						{
							return teleporter.GetComponent<Teleporter>();
						}
					}
				}
			}

			return null;
		}



		public Teleporter GetBossTeleporter()
		{
			foreach (Transform child in WorldInstance.transform)
			{
				if (child.CompareTag("TeleporterBoss"))
				{
					return child.GetComponent<Teleporter>();
				}
			}

			return null;
		}
	}
}