using System;
using Tech_Dev;
using Tech_Dev.Player;
using Tech_Dev.Procedural;
using Unity.VisualScripting;
using UnityEngine;

public class TpManager : MonoBehaviour
{

    [SerializeField] private KeyCode kcFightRoomTP; //1
    [SerializeField] private KeyCode kcTrialRoomTP; //2
    [SerializeField] private KeyCode kcShopTP; //3
    [SerializeField] private KeyCode kcUpgradeTP; //4
    [SerializeField] private KeyCode kcHealTP; //5
    [SerializeField] private KeyCode kcBossTP; //6
    [SerializeField] private KeyCode kcReturnTP; //9

    [SerializeField] GameObject FightRoom1;
    [SerializeField] GameObject FightRoom2;
    [SerializeField] GameObject FightRoom3;
    [SerializeField] GameObject TrialRoom;
    [SerializeField] GameObject Shop;
    [SerializeField] GameObject Upgrade;
    [SerializeField] GameObject Heal;
    [SerializeField] GameObject Boss;
    
    private int FightRoomCount = 1;
    
    private bool Security;

    private GameObject _currentRoomEntry;
    private GameObject Player;

    private Vector3 originalPlayerPosition;
    private bool firstTime = true;


    private void Update()
    {
        if (Player == null)
        {
            Player = GameObject.FindGameObjectWithTag("Player");
        }

        if (Input.GetKeyDown(kcFightRoomTP) || Input.GetKeyDown(kcTrialRoomTP) || Input.GetKeyDown(kcShopTP) || 
            Input.GetKeyDown(kcUpgradeTP) || Input.GetKeyDown(kcHealTP) || Input.GetKeyDown(kcBossTP) && firstTime)
        {
            Security = true;
            firstTime = false;
            originalPlayerPosition = Player.transform.position;
        }

        if (Input.GetKeyDown(kcReturnTP))
        {
            Player.transform.position = originalPlayerPosition;
        }

        if (Input.GetKeyDown(kcFightRoomTP))
        {
            if (FightRoomCount == 1)
            {
                _currentRoomEntry = FightRoom1;
                Player.transform.position = FightRoom1.transform.position;
            }

            if (FightRoomCount == 2)
            {
                _currentRoomEntry = FightRoom2;
                Player.transform.position = FightRoom2.transform.position;
            }

            if (FightRoomCount == 3)
            {
                _currentRoomEntry = FightRoom3;
                Player.transform.position = FightRoom3.transform.position;
                FightRoomCount = 0;
            }
            FightRoomCount = FightRoomCount + 1;
        }

        if (Input.GetKeyDown(kcTrialRoomTP))
        {
            _currentRoomEntry = TrialRoom;
            Player.transform.position = TrialRoom.transform.position;
        }

        if (Input.GetKeyDown(kcShopTP))
        {
            _currentRoomEntry = Shop;
            Player.transform.position = Shop.transform.position;
        }

        if (Input.GetKeyDown(kcUpgradeTP))
        {
            _currentRoomEntry = Upgrade;
            Player.transform.position = Upgrade.transform.position;
        }

        if (Input.GetKeyDown(kcHealTP))
        {
            _currentRoomEntry = Heal;
            Player.transform.position = Heal.transform.position;
        }

        if (Input.GetKeyDown(kcBossTP))
        {
            _currentRoomEntry = Boss;
            Player.transform.position = Boss.transform.position;
        }

        if (Security)
        {
        if (_currentRoomEntry == null) return;
        print(_currentRoomEntry.transform.parent.name);
        Player.GetComponent<PlayerController>().SetNewCameraBounds(_currentRoomEntry.transform.parent.GameObject().GetComponent<RoomManager>().GetRoomBounds());
        Security = false;
            
        }
        
        


    }
}

