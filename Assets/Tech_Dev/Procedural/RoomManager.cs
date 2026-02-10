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
					spawnersList.Add(roomElement.transform);
				}
			}
			
			return spawnersList;
		}

		public void InitRoom()
		{
			
		}
	}
}
