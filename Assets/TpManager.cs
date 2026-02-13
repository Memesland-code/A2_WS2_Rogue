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

    [SerializeField] GameObject FightRoom;
    [SerializeField] GameObject TrialRoom;
    [SerializeField] GameObject Shop;
    [SerializeField] GameObject Upgrade;
    [SerializeField] GameObject Heal;
    [SerializeField] GameObject Boss;

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
            originalPlayerPosition = Player.transform.position;
            Player.transform.position = FightRoom.transform.position;
        }

        if (Input.GetKeyDown(kcTrialRoomTP))
        {
            originalPlayerPosition = Player.transform.position;
            Player.transform.position = TrialRoom.transform.position;
        }

        if (Input.GetKeyDown(kcShopTP))
        {
            originalPlayerPosition = Player.transform.position;
            Player.transform.position = Shop.transform.position;
        }

        if (Input.GetKeyDown(kcUpgradeTP))
        {
            originalPlayerPosition = Player.transform.position;
            Player.transform.position = Upgrade.transform.position;
        }

        if (Input.GetKeyDown(kcHealTP))
        {
            originalPlayerPosition = Player.transform.position;
            Player.transform.position = Heal.transform.position;
        }

        if (Input.GetKeyDown(kcBossTP))
        {
            originalPlayerPosition = Player.transform.position;
            Player.transform.position = Boss.transform.position;
        }


    }
}

