using UnityEngine;

public class UI_Shop : MonoBehaviour
{
 [SerializeField] private KeyCode TestOpen;
 [SerializeField] private KeyCode TestClose;
 
 [SerializeField] private GameObject ShopInterface;
 
 private bool OpenMenu = false;


 void Start()
 {
  ShopInterface.SetActive(false);
 }

 void Update()
 {
  if (Input.GetKeyDown(TestOpen) && OpenMenu == false)
  {
   
   OpenMenu = true; // à retirer ou alors à faire en clean
    OpenShop();
  }

  if (Input.GetKeyDown(TestClose) && OpenMenu == true)
  {
   OpenMenu = false; // à retirer ou alors à faire en clean 
   CloseShop(); //fermer menu  
   
   
  }
 }

 public void OpenShop()
 {
  ShopInterface.SetActive(true);
 }

 public void CloseShop()
 {
  ShopInterface.SetActive(false);
 }
}
