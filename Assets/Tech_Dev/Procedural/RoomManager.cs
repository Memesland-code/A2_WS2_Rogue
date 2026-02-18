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
		public int RoomInternalNb;

		public List<GameObject> RoomEnemies;

		private GameObject _ratPrefab;
		private GameObject _skullPrefab;
		private GameObject _bossPrefab;
		
		private GameManager _gameManager;

		public GameObject RoomTeleporter;

		private void Start()
		{
			RoomEnemies = new List<GameObject>();
			
			_gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
			
			_ratPrefab = _gameManager.GetEnemyRatPrefab();
			_skullPrefab = _gameManager.GetEnemySkullPrefab();
			_bossPrefab = _gameManager.GetBossPrefab();

			foreach (Transform go in transform)
			{
				if (go.CompareTag("TeleportersContainer") || go.CompareTag("TeleporterBoss"))
				{
					RoomTeleporter = go.gameObject;
					break;
				}
			}
			
			if (Type == Type.Shop) InitShopRoom();
		}



		public void InitRoomOnEnter()
		{
			switch (Type)
			{
				case Type.Fight:
					InitFightRoom();
					break;
				
				case Type.Boss:
					InitBossRoom();
					break;
				
				case Type.Trial:
					if (RoomTeleporter != null)
					{
						RoomTeleporter.SetActive(false);
					}
					else
					{
						Debug.LogError("No room teleporter found in room ID: " + RoomId);
					}
					break;
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
			if (RoomTeleporter != null)
			{
				RoomTeleporter.SetActive(false);
			}
			else
			{
				Debug.LogError("No room teleporter found in room ID: " + RoomId);
			}
			
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

				RoomEnemies.Add(enemy.gameObject);
			}
		}



		private void InitBossRoom()
		{
			if (RoomTeleporter != null)
			{
				RoomTeleporter.GetComponent<Teleporter>().TeleportToHub = true;
				RoomTeleporter.SetActive(false);
			}
			else
			{
				Debug.LogError("No room teleporter found in room ID: " + RoomId);
			}

			var spawners = GetSpawners();
			
			Transform enemy = Instantiate(_bossPrefab.transform, spawners[0].transform.position, Quaternion.Euler(0, 0, 0));
			
			enemy.gameObject.GetComponent<BossSkull>().RoomManagerReference = this;
			
			RoomEnemies.Add(enemy.gameObject);
		}



		public void StartTrial()
		{
			InitFightRoom();
		}



		private void InitShopRoom()
		{
			GetComponentInChildren<ShopMerchant>().InitShopRef(RoomInternalNb);
		}



		public void RegisterEnemyDeath(GameObject enemyRef)
		{
			GameManager.AddTotalPlayerKills();
			RoomEnemies.Remove(enemyRef);

			if (RoomEnemies.Count == 0)
			{
				UnlockTeleporters();
			}
		}



		public void UnlockTeleporters()
		{
			RoomTeleporter.SetActive(true);
		}



		public Collider2D GetRoomBounds()
		{
			BoxCollider2D bounds = null;
			foreach (Transform el in transform)
			{
				if (el.CompareTag("CameraBounds"))
				{
					bounds = el.GetComponent<BoxCollider2D>();
				}
			}

			if (bounds == null)
			{
				Debug.LogError("No camera bounds found in room ID: " + RoomId + " of type: " + Type);
				return null;
			}
			else
			{
				return bounds;
			}
		}



		public Vector3 GetRoomEntryCoord()
		{
			foreach (Transform el in transform)
			{
				if (el.CompareTag("RoomEntry")) return el.transform.position;
			}

			Debug.LogError("No room entry found in room ID: " + RoomId + " of type: " + Type);
			return Vector3.zero;
		}



		public int GetRoomInternalNb()
		{
			return RoomInternalNb;
		}
		
		
		
		public void KillAllEnemies()
		{
			while (RoomEnemies.Count > 0)
			{
				Destroy(RoomEnemies[0]);
				RegisterEnemyDeath(RoomEnemies[0]);
			}
		}
	}
}
