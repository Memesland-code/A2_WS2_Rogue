using System;
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
            if (ShopNumber == 1)
            {
                ShopCanvas = GameObject.FindWithTag("Shop1");
            }
            else
            {
                ShopCanvas = GameObject.FindWithTag("Shop2");
            }
        }

        private void Update()
        {
            if (ShopNumber == 0)
            {
                ShopNumber = RoomManagerRef.GetRoomInternalNb();
            }
        }


        public void OpenShop()
        {
            ShopCanvas.SetActive(true);
        }
    }
}
