using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class TrialRoom : MonoBehaviour
    {
        [Header("Do not fill, automatically filled from MainCanvas values")]
        public GameObject TrialCanvas;

        private void Start()
        {
            TrialCanvas = GameManager.GetTrialScreen();
            
            if (TrialCanvas)
            {
                TrialCanvas.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Warning: No Trial Canvas found in game manager!");
            }
        }

        public void OpenTrialChoice()
        {
            TrialCanvas.SetActive(true);
        }
    }
}
