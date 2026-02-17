using Tech_Dev.Player;
using TMPro;
using UnityEngine;

public class Ui_Currencies : MonoBehaviour
{
    private int CurrentGold;
    private int CurrentSouls;
    
    [SerializeField] private TMP_Text GoldText;
    [SerializeField] private TMP_Text SoulsText;
    
    public PlayerController playerController;


    void Update ()
    {
         //CurrentGold = playerController.Gold;
         //CurrentSouls = playerController.Souls;

         GoldText.SetText($"{CurrentGold}"); 
         SoulsText.SetText($"{CurrentSouls}");

         if (CurrentGold < 0) // sécurité en cas de bug
         {
             GoldText.SetText($"{0}"); 
         }
         if (CurrentSouls < 0)
         {
             SoulsText.SetText($"{0}");
         }
             

    }
    
}
