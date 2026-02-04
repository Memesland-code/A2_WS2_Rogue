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
        
        private List<(Room room1, Room room2)> _rooms;
        
        void Start()
        {
            InitSeed();
            
            _rooms = new List<(Room room1, Room room2)>();
            
            InitRoomsGeneration();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
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
                
                _rooms.Add((roomChoice1, roomChoice2));
            }

            foreach (var roomchoices in _rooms)
            {
                print(roomchoices.room1.Type + " - " + roomchoices.room2.Type);
            }
            
            

            /*
             ** Show generation in List
             ** Generate boss room
             * Generate rooms in map
             * add room identifier in engine
             * Add seed management
             */
        }
    }
}
