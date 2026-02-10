using System.Collections.Generic;
using UnityEngine;

namespace Tech_Dev.Procedural
{
	public class RoomManager : MonoBehaviour
	{
		public int RoomId;
		
		public Type Type;
		public Difficulty _difficulty;
		
		public List<Transform> GetSpawners()
		{
			var spawnersList = new List<Transform>();
			
			foreach (Transform roomElement in transform)
			{
				if (roomElement.CompareTag("EnemiesSpawner"))
				{
					foreach (Transform spawner in roomElement.transform)
					{
						spawnersList.Add(spawner);
					}
				}
			}
			
			if (spawnersList.Count == 0) Debug.LogError("Error: No enemies spawner found in room\nRoom ID: " + RoomId + "\nRoom type: " + Type);
			return spawnersList;
		}

		public void InitRoom()
		{
			var spawners = GetSpawners();
			
			//TODO Make enemies spawn
		}
	}
}
