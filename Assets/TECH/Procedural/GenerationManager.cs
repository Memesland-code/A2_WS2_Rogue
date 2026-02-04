using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TECH.Procedural
{
    public class GenerationManager : MonoBehaviour
    {
        [SerializeField] private int _roomsNumber;
        
        [SerializeField] private bool _enableSeed;
        [SerializeField] private int _seed;

        [Space(10)]
        [SerializeField] private List<GameObject> _spawnRoomPrefabs;
        [Space(5)]
        [SerializeField] private List<GameObject> _fightRoomPrefabs_Clean;
        [SerializeField] private List<GameObject> _fightRoomPrefabs_Abandoned;
        [Space(5)]
        [SerializeField] private List<GameObject> _notableRoomPrefabs; //TODO Check for 1 list / notable room type
        [Space(5)]
        [SerializeField] private List<GameObject> _bossRoomPrefabs;
        
        //private List<(Room room1, Room room2)> _rooms;
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
                InitRoomsGeneration();
            }
        }


        private void InitSeed()
        {
            // Put random seed or selected seed depending on user choice
            if (!_enableSeed)
            {
                _seed = (int)DateTime.Now.Ticks;
            }
            Random.InitState(_seed);
        }
        
        

        public void InitRoomsGeneration()
        {
            for (int i = 0; i <= _roomsNumber; i++)
            {
                Room roomChoice1 = new();
                Room roomChoice2 = new();
                
                // For the first room, forces spawn room to spawn
                if (i == 0)
                {
                    roomChoice1.Type = RoomType.Spawn;
                    roomChoice2.Type = RoomType.Spawn;
                }
                else if (i == _roomsNumber) // For last room = boss
                {
                    roomChoice1.Type = RoomType.Boss;
                    roomChoice2.Type = RoomType.Boss;
                }
                else // For others generate random rooms
                {
                    // Get random room type in enum
                    roomChoice1.Type = (RoomType)Random.Range(1, Enum.GetValues(typeof(RoomType)).Length-2);
                    roomChoice1.RoomIndex = i;
                    
                    roomChoice2.Type = (RoomType)Random.Range(1, Enum.GetValues(typeof(RoomType)).Length-2);
                    roomChoice2.RoomIndex = i;
                }
                
                var rooms = new List<Room>
                {
                    roomChoice1,
                    roomChoice2
                };

                _rooms.Add(rooms);
            }

            foreach (var roomchoices in _rooms)
            {
                print(roomchoices[0].Type + " - " + roomchoices[1].Type);
            }
            
            //TODO [v2] - Iterate on rooms to correspond to rooms types and number requirements (for v2)

            foreach (var roomsChoice in _rooms)
            {
                foreach (var room in roomsChoice)
                {
                    // Assign prefab depending on room type
                    switch (room.Type)
                    {
                        case RoomType.Spawn:
                            room.RoomPrefab = _spawnRoomPrefabs[Random.Range(0, _spawnRoomPrefabs.Count-1)];
                            break;

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
                        
                        case RoomType.Healing:
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count - 1)];
                            break;
                        
                        case RoomType.Bonus:
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count - 1)];
                            break;
                        
                        case RoomType.Boss:
                            room.RoomPrefab = _bossRoomPrefabs[Random.Range(0, _bossRoomPrefabs.Count - 1)];
                            break;
                        
                        default:
                            Debug.LogError($"Could not apply roo prefab for {room.Type} room type");
                            break;
                    }
                }
            }

            for (int i = 0; i < _rooms.Count; i++)
            {
                for (int j = 0; j < _rooms[i].Count; j++)
                {
                    Room room = _rooms[i][j];
                    room.RoomInstanceReference = Instantiate(room.RoomPrefab, new Vector3(i * 50, j * 50, 0), Quaternion.Euler(0, 0, 0));
                }
            }

            /*
             ** Show generation in List
             ** Generate boss room
             ** Generate rooms in map
             ** add room identifier in engine
             * Resolve error: console rooms type != instantiated rooms in world
             * Add seed management
             */
        }
    }
}
