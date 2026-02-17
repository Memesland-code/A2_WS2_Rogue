using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class ShopMerchant : MonoBehaviour
    {
        [Header("Do not fill, automatically filled")]
        public GameObject ShopCanvas;
        public int ShopNumber;
        public RoomManager RoomManagerRef;

        private void Start()
        {
            ShopNumber = RoomManagerRef.GetRoomInternalNb();
            
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
