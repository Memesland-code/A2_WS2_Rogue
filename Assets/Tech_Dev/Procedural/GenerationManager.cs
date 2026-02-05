using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tech_Dev.Procedural
{
    public class GenerationManager : MonoBehaviour
    {
        [SerializeField] private int _roomsNumber;
        
        [SerializeField] private bool _enableSeed;
        [SerializeField] private int _seed;

        [Space(5)]
        [SerializeField] private List<GameObject> _fightRoomPrefabs_Clean;
        [SerializeField] private List<GameObject> _fightRoomPrefabs_Abandoned;
        [Space(5)]
        [SerializeField] private List<GameObject> _notableRoomPrefabs;
        //TODO Add gambling room
        [Space(5)]
        [SerializeField] private List<GameObject> _bossRoomPrefabs;
        
        private List<List<Room>> _rooms;
        
        void Start()
        {
            InitSeed();
            
            _rooms = new List<List<Room>>();
            
            InitRoomsGeneration();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Console.Clear();
                
                InitSeed();
                print("Generating new dungeon with parameters:" + "\nSeed: " + _seed);
                
                _rooms.Clear();
                foreach (Transform tr in gameObject.transform)
                {
                    Destroy(tr.gameObject);
                }
                InitRoomsGeneration();
                
                print("Room Generation Complete!");
            }
        }


        // Set random seed or selected seed depending on user choice
        private void InitSeed()
        {
            if (!_enableSeed)
            {
                _seed = (int)DateTime.Now.Ticks;
            }
            Random.InitState(_seed);
        }
        
        

        public void InitRoomsGeneration()
        {
            //info First rooms init of types
            for (int i = 0; i <= _roomsNumber; i++)
            {
                Room roomChoice1 = new();
                Room roomChoice2 = new();

                if (i == 0)
                {
                    roomChoice1.Type = RoomType.Fight;
                    roomChoice2.Type = RoomType.Fight;
                }
                else if (i == _roomsNumber) // For last room = boss
                {
                    roomChoice1.Type = RoomType.Boss;
                    roomChoice2.Type = RoomType.Boss;
                }
                else if (i == _roomsNumber - 1)
                {
                    roomChoice1.Type = RoomType.Healing;
                    roomChoice2.Type = RoomType.Healing;
                }
                else // For others generate random rooms
                {
                    // Get random room type in enum
                    roomChoice1.Type = (RoomType)Random.Range(0, Enum.GetValues(typeof(RoomType)).Length-2);
                    roomChoice2.Type = (RoomType)Random.Range(0, Enum.GetValues(typeof(RoomType)).Length-2);
                }
                roomChoice1.RoomId = i;
                roomChoice2.RoomId = i;
                
                var rooms = new List<Room>
                {
                    roomChoice1,
                    roomChoice2
                };

                _rooms.Add(rooms);
            }
            
            //TODO [v2] - Iterate on rooms to correspond to rooms types and number requirements

            //info Setting room prefab for all rooms depending on their type
            foreach (var roomsChoice in _rooms)
            {
                foreach (var room in roomsChoice)
                {
                    // Assign prefab depending on room type
                    switch (room.Type)
                    {
                        case RoomType.Fight:
                            if (room.RoomDifficulty == RoomDifficulty.Clean)
                            {
                                room.RoomPrefab = _fightRoomPrefabs_Clean[Random.Range(0, _fightRoomPrefabs_Clean.Count - 1)];
                            }
                            else
                            {
                                room.RoomPrefab = _fightRoomPrefabs_Abandoned[Random.Range(0, _fightRoomPrefabs_Abandoned.Count - 1)];
                            }
                            break;
                            
                        case RoomType.Merchant:
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count - 1)];
                            break;
                        
                        case RoomType.Upgrade:
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count - 1)];
                            break;
                        
                        case RoomType.Gambling:
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count - 1)];
                            break;
                        
                        case RoomType.Healing:
                            if (room.RoomId != _roomsNumber - 1) Debug.LogError("Healing room instantiated on other slot than pre-boss!");
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count - 1)];
                            break;
                        
                        case RoomType.Boss:
                            room.RoomPrefab = _bossRoomPrefabs[Random.Range(0, _bossRoomPrefabs.Count - 1)];
                            break;

                        default:
                            Debug.LogError($"Could not apply room prefab for {room.Type} room type");
                            break;
                    }
                }
            }

            //info Check for all rooms and instantiate
            for (int i = 0; i < _rooms.Count; i++)
            {
                for (int j = 0; j < _rooms[i].Count; j++)
                {
                    Room room = _rooms[i][j];
                    room.RoomInstanceReference = Instantiate(room.RoomPrefab, new Vector3(i * 50, j * 50, 0), Quaternion.Euler(0, 0, 0), gameObject.transform);
                    room.RoomInstanceReference.GetComponentInChildren<TextMeshPro>().SetText(room.Type.ToString());
                }
            }
        }
    }
}
