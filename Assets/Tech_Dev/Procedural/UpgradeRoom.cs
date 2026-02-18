using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class UpgradeRoom : MonoBehaviour
    {
        [Header("Do not fill, automatically filled from MainCanvas values")]
        public GameObject UpgradeCanvas;

        private void Start()
        {
            UpgradeCanvas = GameManager.GetUpgradeScreen();

            if (UpgradeCanvas)
            {
                UpgradeCanvas.SetActive(false);
            }
            else
            {
                Debug.LogWarning("Warning: No Upgrade Canvas found in game manager!");
            }
        }

        public void OpenUpgradeChoice()
        {
            UpgradeCanvas.SetActive(true);
        }
    }
}
