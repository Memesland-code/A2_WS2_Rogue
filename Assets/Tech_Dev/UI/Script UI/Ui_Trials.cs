using System;
using Tech_Dev;
using UnityEngine;

public class Ui_Trials : MonoBehaviour
{
	[SerializeField] private GameObject Relique1TrialRoom;
	[SerializeField] private GameObject Relique2TrialRoom;

	private void Start()
	{
		Relique1TrialRoom.SetActive(false);
		Relique2TrialRoom.SetActive(false);

	}

	public void Trial1rstChoice()
	{
		GameManager.CloseAllUIs();
		GameManager.SetRelicSpellprojectileTeleport();
		Relique1TrialRoom.SetActive(true);
		// ajouter la relique 1 envoyer au GameManager l'information
		GameManager.GetPlayerScriptRef().GetCurrentRoom().StartTrial();
		
	}

	public void Trial2stChoice()
	{
		GameManager.CloseAllUIs();
		GameManager.SetRelicDoubleDash();
		Relique2TrialRoom.SetActive(true);
		// ajouter la relique 2 envoyer au GameManager l'information
		GameManager.GetPlayerScriptRef().GetCurrentRoom().StartTrial();
	}
	
	
}
