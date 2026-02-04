using UnityEngine;

namespace TECH.Procedural
{
	public enum RoomType
	{
		Spawn,
		Fight,
		Merchant,
		Upgrade,
		Gambling,
		Healing,
		Boss
	}

	public enum RoomDifficulty
	{
		Clean,
		Abandoned
	}
    
	public class Room
	{
		public int RoomId;
		
		public RoomType Type;
		public RoomDifficulty RoomDifficulty; //TODO Select between clean and abandoned
		
		public GameObject RoomPrefab;
		public GameObject RoomInstanceReference;
		//TODO public (Room room1, Room room2) ExitRooms; //TODO Check with teleporters later
	}
}