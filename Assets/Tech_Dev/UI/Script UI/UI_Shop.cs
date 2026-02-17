using Tech_Dev;
using Tech_Dev.Player;
using Tech_Dev.Procedural;
using TMPro;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
 [SerializeField] private KeyCode TestOpen;
 [SerializeField] private KeyCode TestClose;
 
 [SerializeField] private GameObject Shop1;
 [SerializeField] private GameObject Shop2;

 [SerializeField] private int ActualShop;
 
 [SerializeField] private GameObject Background; // fond dérrière le shop et autres éléments intéractibles
 
 [SerializeField] private GameObject AllGreyChoice;
 [SerializeField] private GameObject GreyChoice1; // cadres qui recouvrirons les items une fois acheté
 [SerializeField] private GameObject GreyChoice2;
 [SerializeField] private GameObject GreyChoice3;
 [SerializeField] private GameObject GreyChoice4;
 [SerializeField] private GameObject GreyChoice5;
 [SerializeField] private GameObject GreyChoice6;
 
 [SerializeField] private GameObject ImageChoice1; // choix dans le magasin 
 [SerializeField] private GameObject ImageChoice2;
 [SerializeField] private GameObject ImageChoice3;
 [SerializeField] private GameObject ImageChoice4;
 [SerializeField] private GameObject ImageChoice5;
 [SerializeField] private GameObject ImageChoice6;
 
 [SerializeField] TMP_Text TextPrice1;
 [SerializeField] TMP_Text TextPrice2;
 [SerializeField] TMP_Text TextPrice3;
 [SerializeField] TMP_Text TextPrice4;
 [SerializeField] TMP_Text TextPrice5;
 [SerializeField] TMP_Text TextPrice6;
 
 [SerializeField] int Price1;
 [SerializeField] int Price2;
 [SerializeField] int Price3;
 [SerializeField] int Price4;
 [SerializeField] int Price5;
 [SerializeField] int Price6;
 
 
 public PlayerController playerController;
 private int CurrentGold;

 
 
 private bool OpenMenu = false;


 void Start()
 {
  Shop1.SetActive(false);
  Shop2.SetActive(false);
  Background.SetActive(false); 
  
  GreyChoice1.SetActive(false); // met sur faux sur les cadres qui recouvrirons les items une fois acheté
  GreyChoice2.SetActive(false);
  GreyChoice3.SetActive(false);
  GreyChoice4.SetActive(false);
  GreyChoice5.SetActive(false);
  GreyChoice6.SetActive(false);
  
  TextPrice1.SetText($"{Price1}");
  TextPrice2.SetText($"{Price2}");
  TextPrice3.SetText($"{Price3}");
  TextPrice4.SetText($"{Price4}");
  TextPrice5.SetText($"{Price5}");
  TextPrice6.SetText($"{Price6}");

 }

 void Update()
 {
  //CurrentGold = playerController.Gold;

  
   ActualShop = GameManager.GetPlayerScriptRef().GetCurrentRoom().GetRoomInternalNb();

   
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
  Background.SetActive(true);

  if (ActualShop == 1)
  {
    Shop1.SetActive(true);
  }
  
  if (ActualShop == 2)
  {
   Shop2.SetActive(true);
  }
 }

 public void CloseShop()
 {
  if (ActualShop == 1)
  {
   Shop1.SetActive(false);
  }
  if (ActualShop == 2)
  {
   Shop2.SetActive(false);
  }
  Background.SetActive(false);
  
  
  Time.timeScale = 1; //   le jeu et les animation de la pause
  

 }
           // ne pas oublier de soustraire le prix à la suite d'un achat
 public void Choice1() 
 {
  if (CurrentGold >= Price1)
  {
   GreyChoice1.SetActive(true);
   ImageChoice1.SetActive(false);
   //ajouter item à l'inventaire
   
  }
  
 }
 
 public void Choice2()
 {
  if (CurrentGold >= Price2)
  {
   GreyChoice2.SetActive(true);
   ImageChoice2.SetActive(false);
   //ajouter item à l'inventaire
  }

 }
 
 public void Choice3()
 {
  if (CurrentGold >= Price3)
  {
   GreyChoice3.SetActive(true);
   ImageChoice3.SetActive(false);
   //ajouter item à l'inventaire

  }

 }
 
 public void Choice4() 
 {
  if (CurrentGold >= Price4)
  {
   GreyChoice4.SetActive(true);
   ImageChoice4.SetActive(false);
   //ajouter item à l'inventaire

  }

 }
 
 public void Choice5()
 {
  if (CurrentGold >= Price5)
  {
   GreyChoice5.SetActive(true);
   ImageChoice5.SetActive(false);
   //ajouter item à l'inventaire
  }
 }
 
 public void Choice6()
 {
  if (CurrentGold >= Price6)
  {
   GreyChoice6.SetActive(true);
   ImageChoice6.SetActive(false);
   //ajouter item à l'inventaire

  }
 }


 public void BloodButton()
 {
  // bouton pour donner de la vie aux marchand contre du gold
 }
 
}
