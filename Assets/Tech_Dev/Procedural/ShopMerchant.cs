using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class ShopMerchant : MonoBehaviour
    {
        public GameObject ShopCanvas;

        public int ShopNumber;

        private void Start()
        {
            ShopNumber = GameManager.GetPlayerScriptRef().GetCurrentRoom().GetRoomInternalNb();
            
            if (ShopNumber == 1)
            {
                ShopCanvas = GameObject.FindWithTag("Shop1");
            }
            else
            {
                ShopCanvas = GameObject.FindWithTag("Shop2");
            }
        }



        public void OpenShop()
        {
            ShopCanvas.SetActive(true);
        }
    }
}
