using System;
using Tech_Dev;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
	[SerializeField] private KeyCode pauseKey = KeyCode.Escape;
	[SerializeField] private GameObject pauseMenu;


	private void Start()
	{
		pauseMenu.SetActive(false);
	}

	private void Update()
{
	if (Input.GetKeyDown(pauseKey))
	{
		PauseGame();
	}
}


public void PauseGame()
{
	Time.timeScale = 0;
	pauseMenu.SetActive(true);
}

public void ResumeButton()
	{
		pauseMenu.SetActive(false);
		Time.timeScale = 1;
	}

	public void RestartButton()
	{
		Time.timeScale = 1;
		StartCoroutine(GameManager.GetPlayerScriptRef().PlayerDeath());
		pauseMenu.SetActive(false);
		// retour au hub
	}

	public void QuitButton()
	{
		Application.Quit();
	}
}
