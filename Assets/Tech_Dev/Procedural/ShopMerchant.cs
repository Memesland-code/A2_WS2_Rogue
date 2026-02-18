using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class ShopMerchant : MonoBehaviour
    {
        [Header("Do not fill, automatically filled from MainCanvas values")]
        public GameObject ShopCanvas;
        public int ShopNumber;

        public void InitShopRef(int shopNumber)
        {
	        ShopNumber = shopNumber;
	        
	        if (ShopNumber == 1)
	        {
		        ShopCanvas = GameManager.GetShop1Screen();
	        }
	        else
	        {
		        ShopCanvas = GameManager.GetShop2Screen();
	        }
            
	        ShopCanvas.SetActive(false);
        }

        public void OpenShop()
        {
            ShopCanvas.SetActive(true);
        }
    }
}
