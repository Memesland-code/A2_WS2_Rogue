using UnityEngine;

namespace Tech_Dev.UI.Script_UI
{
    public class MainMenuUI : MonoBehaviour
    {
        public void StartGame()
        {
            gameObject.SetActive(false);

            GameManager.StartGame();
        }

        public void Quit()
        {
	        Application.Quit();
        }
    }
}
