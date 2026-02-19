using System;
using Tech_Dev;
using UnityEngine;

public class Ui_Trials : MonoBehaviour
{
	[SerializeField] private GameObject Relique1TrialRoom;

	private void Start()
	{
		Relique1TrialRoom.SetActive(false);
	}

	public void Trial1rstChoice()
	{
		GameManager.CloseAllUIs();
		GameManager.SetRelicSpellprojectileTeleport();
		Relique1TrialRoom.SetActive(true);
		// ajouter la relique 1 envoyer au GameManager l'information
		
	}

	public void Trial2stChoice()
	{
		GameManager.CloseAllUIs();
		GameManager.SetRelicDoubleDash();
		// ajouter la relique 2 envoyer au GameManager l'information
	}
	
	
}
