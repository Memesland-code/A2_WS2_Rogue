using UnityEngine;

namespace Tech_Dev.UI.Script_UI
{
    public class MainMenuUI : MonoBehaviour
    {
	    [SerializeField] private GameObject _settingsScreen;
	    
        public void StartGame()
        {
            gameObject.SetActive(false);

            GameManager.StartGame();
        }

        public void OpenSettings()
        {
	        _settingsScreen.SetActive(true);
        }

        public void QuitGame()
        {
	        Application.Quit();
        }
    }
}
