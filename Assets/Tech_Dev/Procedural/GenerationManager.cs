using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tech_Dev.Procedural
{
    public class GenerationManager : MonoBehaviour
    {
        [Tooltip("Does no count the 1st fight room and the last boss room")]
        [SerializeField] private int _roomsNumber;
        
        [SerializeField] private bool _enableSeed;
        [SerializeField] private int _seed;

        [Space(10)]
        [SerializeField] private List<RoomDifficultyPrefabs> _fightRoomPrefabs;
        [Space(5)]
        [SerializeField] private List<GameObject> _notableRoomPrefabs;
        //TODO Add gambling room
        [Space(5)]
        [SerializeField] private List<GameObject> _bossRoomPrefabs;

        private GameObject _hubRoom;
        private GameObject _hubSpawnPoint;
        
        private List<List<Room>> _rooms;
        
        void Start()
        {
            _hubRoom = GameObject.FindWithTag("Respawn");
            
            foreach (Transform hubElement in _hubRoom.transform)
            {
                if (hubElement.CompareTag("RoomEntry"))
                {
                    _hubSpawnPoint = hubElement.gameObject;
                }
            }
            
            InitSeed();
            
            _rooms = new List<List<Room>>();

            _roomsNumber += 1;
            
            InitRoomsGeneration();
        }

        //info Optional for testing only: resets the rooms generation
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
                    roomChoice1.Type = Type.Fight;
                    roomChoice2.Type = Type.Fight;
                }
                else if (i == _roomsNumber) // For last room = boss
                {
                    roomChoice1.Type = Type.Boss;
                    roomChoice1.Difficulty = Difficulty.Pristine;
                    
                    roomChoice2.Type = Type.Boss;
                    roomChoice2.Difficulty = Difficulty.Ruin;
                }
                else if (i == _roomsNumber - 1)
                {
                    roomChoice1.Type = Type.Healing;
                    roomChoice1.Difficulty = Difficulty.None;
                    
                    roomChoice2.Type = Type.Healing;
                    roomChoice2.Difficulty = Difficulty.None;
                }
                else // For others generate random rooms
                {
                    roomChoice1.Type = (Type)Random.Range(1, Enum.GetValues(typeof(Type)).Length-2);

                    if (roomChoice1.Type == Type.Fight)
                    {
                        roomChoice1.Difficulty = Difficulty.Pristine;
                        
                        roomChoice2.Type = Type.Fight;
                        roomChoice2.Difficulty = Difficulty.Ruin;
                    }
                    else
                    {
                        roomChoice2.Type = (Type)Random.Range(2, Enum.GetValues(typeof(Type)).Length-2);

                        roomChoice1.Difficulty = roomChoice1.Type == Type.Gambling ? Difficulty.Pristine : Difficulty.None;
                        roomChoice2.Difficulty = roomChoice2.Type == Type.Gambling ? Difficulty.Ruin : Difficulty.None;
                    }
                }
                roomChoice1.RoomId = i;
                roomChoice2.RoomId = i + 100;
                
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
                        case Type.Fight:
                            //info 0 = Pristine room ; 1 = Ruin room
                            if (room.Difficulty == Difficulty.Pristine)
                            {
                                room.RoomPrefab = _fightRoomPrefabs[Random.Range(0, _fightRoomPrefabs.Count)].PrefabsChoice[0];
                            }
                            else
                            {
                                room.RoomPrefab = _fightRoomPrefabs[Random.Range(0, _fightRoomPrefabs.Count)].PrefabsChoice[1];
                            }
                            break;
                            
                        case Type.Merchant:
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count)];
                            break;
                        
                        case Type.Upgrade:
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count)];
                            break;
                        
                        case Type.Gambling:
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count)];
                            break;
                        
                        case Type.Healing:
                            if (room.RoomId == (_roomsNumber - 1) || room.RoomId == (_roomsNumber - 1 + 100)) {}
                            else
                            {
                                Debug.LogError("Healing room instantiated on other slot than pre-boss!\nRoom ID: " + room.RoomId);
                            }
                            room.RoomPrefab = _notableRoomPrefabs[Random.Range(0, _notableRoomPrefabs.Count)];
                            break;
                        
                        case Type.Boss:
                            room.RoomPrefab = _bossRoomPrefabs[Random.Range(0, _bossRoomPrefabs.Count)];
                            break;

                        default:
                            Debug.LogError($"Could not apply room prefab for {room.Type} room type");
                            break;
                    }
                }
            }

            
            
            //info Instantiate all rooms in world
            for (int i = 0; i < _rooms.Count; i++)
            {
                for (int j = 0; j < _rooms[i].Count; j++)
                {
                    Room room = _rooms[i][j];
                    room.WorldInstance = Instantiate(room.RoomPrefab, new Vector3(i * 150, j * 150, 0), Quaternion.Euler(0, 0, 0), gameObject.transform);
                    
                    room.SetupRoomScript();
                }
            }
            
            

            //info Teleporters binding - Loop through all rooms
            for (int i = 0; i < _rooms.Count - 1; i++)
            {
                for (int j = 0; j < _rooms[i].Count; j++)
                {
                    Room room = _rooms[i][j];
                    Room nextPristineRoom = _rooms[i + 1][0];
                    Room nextRuinRoom = _rooms[i + 1][1];
                    
                    //info Check if 1st room
                    if (i == 0)
                    {
                        // Hub to 1st room
                        Transform teleportersContainerRef = null;
                        foreach (Transform roomElement in GameObject.FindWithTag("Respawn").transform)
                        {
                            if (roomElement.CompareTag("TeleportersContainer"))
                            {
                                teleportersContainerRef = roomElement;
                            }
                        }

                        if (teleportersContainerRef == null)
                        {
                            Debug.LogError("TeleportersContainerRef is null in spawn room. Stopping process!");
                            return;
                        }
                        
                        teleportersContainerRef.GetChild(0).GetComponent<Teleporter>().SetDestinationEntryPoint(_rooms[i][0].WorldInstance, _rooms[i][0].GetRoomEntry());
                        teleportersContainerRef.GetChild(1).GetComponent<Teleporter>().SetDestinationEntryPoint(_rooms[i][1].WorldInstance, _rooms[i][1].GetRoomEntry());
                        
                        // 1st room to 2nd
                        room.GetPristineTeleporter().SetDestinationEntryPoint(nextPristineRoom.WorldInstance, nextPristineRoom.GetRoomEntry());
                        room.GetRuinTeleporter().SetDestinationEntryPoint(nextRuinRoom.WorldInstance, nextRuinRoom.GetRoomEntry());
                    }
                    else
                    {
                        foreach (Transform roomElement in room.WorldInstance.transform)
                        {
                            if (roomElement.CompareTag("TeleportersContainer"))
                            {
                                foreach (Transform teleporterGameObject in roomElement.transform)
                                {
                                    if (teleporterGameObject.gameObject.TryGetComponent(out Teleporter teleporter))
                                    {
                                        if (teleporter.GetTeleporterDifficulty() == Difficulty.Pristine)
                                        {
                                            room.GetPristineTeleporter().SetDestinationEntryPoint(nextPristineRoom.WorldInstance, nextPristineRoom.GetRoomEntry());
                                            continue;
                                        }
                                        room.GetRuinTeleporter().SetDestinationEntryPoint(nextRuinRoom.WorldInstance, nextRuinRoom.GetRoomEntry());
                                        break;
                                    }
                                }
                            }
                            
                        }
                    }
                }
            }
            
            //info if room is boss room
            _rooms.Last()[0].GetBossTeleporter().SetDestinationEntryPoint(_hubRoom, _hubSpawnPoint.transform);
            _rooms.Last()[1].GetBossTeleporter().SetDestinationEntryPoint(_hubRoom, _hubSpawnPoint.transform);
        }
    }
}
