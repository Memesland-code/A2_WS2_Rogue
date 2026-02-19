using Tech_Dev;
using Tech_Dev.Player;
using UnityEngine;

public class UI_HealRoom : MonoBehaviour
{

    private float MaxHP;
    
    public void HealingChoice()
    {
        GameManager.CloseAllUIs(); // soigner de 50% la vie du joueur
        
        MaxHP = GameManager.GetPlayerScriptRef().GetMaxHealth();
        GameManager.GetPlayerScriptRef().AddHealth(MaxHP/2);

    }

    public void IncreaseHealthChoice()
    {
        GameManager.IncreasePlayerMaxHealth(25);
        GameManager.CloseAllUIs();
        // augmenter la vie max du joueur
    }
}
