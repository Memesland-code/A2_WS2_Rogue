namespace TECH.Procedural
{
	public enum RoomType
	{
		Spawn,
		Fight
	}
    
	public class Room
	{
		public RoomType Type;
		public int RoomIndex;
		public (Room room1, Room room2) ExitRooms;
	}
}