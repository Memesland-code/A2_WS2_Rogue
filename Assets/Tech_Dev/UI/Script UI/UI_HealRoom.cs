using Tech_Dev;
using UnityEngine;

public class UI_HealRoom : MonoBehaviour
{
    public void HealingChoice()
    {
        GameManager.CloseAllUIs();
        // soigner de 50% la vie du joueur
		
    }

    public void IncreaseHealthChoice()
    {
        GameManager.CloseAllUIs();
        // augmenter la vie max du joueur
    }
}
