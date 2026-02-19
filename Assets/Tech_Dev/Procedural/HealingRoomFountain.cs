using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class HealingRoomFountain : MonoBehaviour
    {
        [Header("Do not fill, automatically filled from MainCanvas values")]
        public GameObject healingCanvas;

        private void Start()
        {
            healingCanvas = GameManager.GetHealingScreen();
            
            if (healingCanvas)
            {
                healingCanvas.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Warning: No healing Canvas found in game manager!");
            }
        }

        public void OpenHealingChoice() 
        {
            healingCanvas.SetActive(true);
            
        }
        
        
    }
}
