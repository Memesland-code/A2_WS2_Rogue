using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace TECH.Procedural
{
    public class GenerationManager : MonoBehaviour
    {
        [SerializeField] private int roomsNumber;
        [SerializeField] private int _seed;
        
        private List<(Room room1, Room room2)> _rooms;
        
        void Start()
        {
            if (_seed == 0) _seed = (int)DateTime.Now.Ticks;
            
            InitRoomsGeneration();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.N))
            {
                Random.InitState(_seed);
                _seed++;
                print("Seed: " + _seed + "\nRandom generation: " + Random.value);
                
                _rooms.Clear();
                InitRoomsGeneration();
            }
        }

        public void InitRoomsGeneration()
        {
            for (int i = 0; i < roomsNumber-1; i++)
            {
                Room roomChoice1 = new();
                Room roomChoice2 = new();

                // For the first room, forces spawn room to spawn
                if (i == 0)
                {
                    roomChoice1.Type = RoomType.Spawn;
                    _rooms.Add((roomChoice1, roomChoice1));
                }
                else // For others generate random rooms
                {
                    roomChoice1.Type = (RoomType)Random.Range(1, Enum.GetValues(typeof(RoomType)).Length-1);
                    roomChoice1.RoomIndex = i;
                    
                    roomChoice2.Type = (RoomType)Random.Range(1, Enum.GetValues(typeof(RoomType)).Length-1);
                    roomChoice2.RoomIndex = i;
                    
                    _rooms.Add((roomChoice1, roomChoice2));
                }
            }

            foreach (var roomchoices in _rooms)
            {
                print(roomchoices.room1 + " - " + roomchoices.room2);
            }

            /*
             * TODO Show generation in List
             * Generate boss room
             * Generate rooms in map
             * add room identifier in engine
             * Add seed management
             */
        }
    }
}
