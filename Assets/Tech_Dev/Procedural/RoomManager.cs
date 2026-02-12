using System;
using System.Collections.Generic;
using Tech_Dev.Enemies;
using UnityEngine;

namespace Tech_Dev.Procedural
{
	public class RoomManager : MonoBehaviour
	{
		public int RoomId;
		
		public Type Type;
		public Difficulty Difficulty;

		public List<object> RoomEnemies = new();

		private GameObject _ratPrefab;
		private GameObject _skullPrefab;
		
		private GameManager _gameManager;

		private GameObject _roomEntry;
		private GameObject _roomTeleporter;

		private void Start()
		{
			foreach (Transform el in transform)
			{
				if (el.CompareTag("RoomEntry")) _roomEntry = el.gameObject;
			}
			_gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
			
			_ratPrefab = _gameManager.GetEnemyRatPrefab();
			_skullPrefab = _gameManager.GetEnemySkullPrefab();

			foreach (Transform go in transform)
			{
				if (go.CompareTag("TeleportersContainer") || go.CompareTag("TeleporterBoss"))
				{
					_roomTeleporter = go.gameObject;
					break;
				}
			}

			if (_roomTeleporter != null)
			{
				_roomTeleporter.SetActive(false);
			}
			else
			{
				Debug.LogError("No room teleporter found in room ID: " + RoomId);
			}
		}



		public void InitRoom()
		{
			if (Type == Type.Fight)
			{
				InitFightRoom();
			}
		}

		
		
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

		
		
		private void InitFightRoom()
		{
			var spawners = GetSpawners();

			// Spawn enemies and register in list
			foreach (Transform spawner in spawners)
			{
				Transform enemy;
				if (spawner.CompareTag("RatSpawner"))
				{
					enemy = Instantiate(_ratPrefab.transform, spawner.position, Quaternion.Euler(0, 0, 0));
					enemy.gameObject.GetComponent<EnemyRat>().RoomManagerReference = this;
				}
				else if (spawner.CompareTag("SkullSpawner"))
				{
					enemy = Instantiate(_skullPrefab.transform, spawner.position, Quaternion.Euler(0, 0, 0));
					enemy.gameObject.GetComponent<EnemySkull>().RoomManagerReference = this;
				}
				else
				{
					// If error spawn rat by default
					Debug.LogError("Wrong spawner settings found!\nRoom ID: " + RoomId + "\nRoom type: " + Type + "\nSpawner tag: " + spawner.tag + "\nGameObject name: " + spawner.name);
					enemy = Instantiate(_ratPrefab.transform, spawner.position, Quaternion.Euler(0, 0, 0));
					enemy.gameObject.GetComponent<EnemyRat>().RoomManagerReference = this;
				}

				RoomEnemies.Add(enemy);
			}
		}



		public void RegisterEnemyDeath(object enemyRef)
		{
			RoomEnemies.Remove(enemyRef);

			if (RoomEnemies.Count == 0)
			{
				print("Room cleared!");
				UnlockTeleporters();
			}
		}



		public Transform GetRoomEntry()
		{
			return _roomEntry.transform;
		}



		public void UnlockTeleporters()
		{
			_roomTeleporter.SetActive(true);
		}
	}
}
