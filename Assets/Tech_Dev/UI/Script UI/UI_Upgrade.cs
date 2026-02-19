using Tech_Dev;
using UnityEngine;

public class UI_Upgrade : MonoBehaviour
{

	
	public void Upgrade1rstChoice()
	{
		GameManager.CloseAllUIs();
		GameManager.TrinketSpellProjectileDamage();
		// ajouter la trinket 1 envoyer au GameManager l'information

	}

	public void Upgrade2ndChoice()
	{
		GameManager.CloseAllUIs();
		GameManager.TrinketSpecialAttackCooldown();
		// ajouter la trinket 2 envoyer au GameManager l'information
	}
}
