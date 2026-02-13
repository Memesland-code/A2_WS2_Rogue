using System;
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
                Player.transform.position = FightRoom1.transform.position;
            }

            if (FightRoomCount == 2)
            {
                Player.transform.position = FightRoom2.transform.position;
            }

            if (FightRoomCount == 3)
            {
                Player.transform.position = FightRoom3.transform.position;
                FightRoomCount = 0;
            }
            FightRoomCount = FightRoomCount + 1;
        }

        if (Input.GetKeyDown(kcTrialRoomTP))
        {
            Player.transform.position = TrialRoom.transform.position;
        }

        if (Input.GetKeyDown(kcShopTP))
        {
            Player.transform.position = Shop.transform.position;
        }

        if (Input.GetKeyDown(kcUpgradeTP))
        {
            Player.transform.position = Upgrade.transform.position;
        }

        if (Input.GetKeyDown(kcHealTP))
        {
            Player.transform.position = Heal.transform.position;
        }

        if (Input.GetKeyDown(kcBossTP))
        {
            Player.transform.position = Boss.transform.position;
        }


    }
}

