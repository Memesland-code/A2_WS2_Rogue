using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class ShopMerchant : MonoBehaviour
    {
        [Header("Do not fill, automatically filled from MainCanvas values")]
        public GameObject ShopCanvas;
        public int ShopNumber;

        private void Start()
        {
            if (ShopNumber == 1)
            {
                ShopCanvas = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Shop1Screen;
            }
            else
            {
                ShopCanvas = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().Shop2Screen;
            }
            
            ShopCanvas.SetActive(false);
        }

        public void OpenShop()
        {
            ShopCanvas.SetActive(true);
        }
    }
}
