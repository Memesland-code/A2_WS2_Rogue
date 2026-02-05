using UnityEngine;

namespace Tech_Dev.Procedural
{
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
		Clean,
		Abandoned
	}
	
	/* todo 
	 * Object List<List<(GameObject clean, GameObject abandoned)>>
	 * Select in List a random element.
	 * Selected element in List is a Tuple
	 * Tuple.clean is the clean prefab of the room
	 * Tuple.Abandoned is the abandoned prefab of the room
	 */
    
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