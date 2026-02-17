using Tech_Dev.Player;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
 [SerializeField] private KeyCode TestOpen;
 [SerializeField] private KeyCode TestClose;
 
 [SerializeField] private GameObject ShopInterface;
 
 [SerializeField] private GameObject Shop1;
 [SerializeField] private GameObject Shop2;

 private GameObject ActualShop;
 
 
 private bool OpenMenu = false;


 void Start()
 {
  ShopInterface.SetActive(false);
 }

 void Update()
 {
  // faire la réferance de la salle pour que actual shop corresponde au shop 1 ou 2
   // getRoomInternalNb
   //ActualShop = PlayerController.getRoomInternalNb();
  
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
  Time.timeScale = 0; // met le jeux et les animations en pause ATTNETION à bien retirer la pause ensuite


  if (ActualShop == Shop1)
  {
    ShopInterface.SetActive(true);
  }
  else if (ActualShop == Shop2)
  {
   ShopInterface.SetActive(true);
  }
 }

 public void CloseShop()
 {
  ShopInterface.SetActive(false);
  Time.timeScale = 1; //   le jeu et les animation de la pause
 }

 public void Choice1() 
 {
 }
 
 public void Choice2()
 {
 }
 
 public void Choice3()
 {
 }
 
 public void Choice4() 
 {
 }
 
 public void Choice5()
 {
 }
 
 public void Choice6()
 {
 }
 
}
