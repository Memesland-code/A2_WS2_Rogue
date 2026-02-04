using UnityEngine;

namespace TECH.Procedural
{
	public enum RoomType
	{
		Spawn,
		Fight,
		Merchant,
		Healing,
		Bonus,
		Boss
	}

	public enum RoomDifficulty
	{
		Clean,
		Abandoned
	}
    
	public class Room
	{
		public int RoomIndex;
		
		public RoomType Type;
		//TODO public RoomDifficulty RoomDifficulty; //TODO Select between clean and abandoned
		
		public GameObject Prefab;
		//TODO public (Room room1, Room room2) ExitRooms; //TODO Check with teleporters later
	}
}