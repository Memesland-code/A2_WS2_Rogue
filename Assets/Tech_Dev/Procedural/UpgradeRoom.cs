using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class UpgradeRoom : MonoBehaviour
    {
        [Header("Do not fill, automatically filled from MainCanvas values")]
        public GameObject UpgradeCanvas;

        private void Start()
        {
            UpgradeCanvas = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().UpgradeScreen;
            
            UpgradeCanvas.SetActive(false);
        }

        public void OpenUpgradeChoice()
        {
            UpgradeCanvas.SetActive(true);
        }
    }
}
