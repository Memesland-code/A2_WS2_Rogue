using TECH.Procedural;
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
    
	public class Room
	{
		public RoomType Type;
		public int RoomIndex;
		public (Room room1, Room room2) ExitRooms;
		public GameObject Prefab;
		
	}
}