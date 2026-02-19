using System;
using Tech_Dev;
using Tech_Dev.Player;
using Tech_Dev.Procedural;
using TMPro;
using UnityEngine;

public class UI_Shop : MonoBehaviour
{
 
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
 
 [SerializeField] private GameObject ReliqueShop1;
 
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
 
 private int CurrentGold;
 
 
 void Start()
 {

  
  GreyChoice1.SetActive(false); // met sur faux sur les cadres qui recouvrirons les items une fois acheté
  GreyChoice2.SetActive(false);
  GreyChoice3.SetActive(false);
  GreyChoice4.SetActive(false);
  GreyChoice5.SetActive(false);
  GreyChoice6.SetActive(false);
  
  ReliqueShop1.SetActive(false);
  
  TextPrice1.SetText($"{Price1}");
  TextPrice2.SetText($"{Price2}");
  TextPrice3.SetText($"{Price3}");
  TextPrice4.SetText($"{Price4}");
  TextPrice5.SetText($"{Price5}");
  TextPrice6.SetText($"{Price6}");

 }

 private void Update()
 {
  CurrentGold = GameManager.GetPlayerScriptRef().GetGold();
 }


 public void Choice1() 
 {
  if (CurrentGold >= Price1)
  {
   GreyChoice1.SetActive(true);
   ImageChoice1.SetActive(false);
   GameManager.SpellProjectileSpeed();
   //ajouter item à l'inventaire

  }
  
 }
 
 public void Choice2()
 {
  if (CurrentGold >= Price2)
  { 
   GreyChoice2.SetActive(true);
   ImageChoice2.SetActive(false);
   GameManager.TrinketSpellStun();
   //ajouter item à l'inventaire
  }

 }
 
 public void Choice3()
 {
  if (CurrentGold >= Price3)
  {
   GreyChoice3.SetActive(true);
   ImageChoice3.SetActive(false); 
   GameManager.SetRelicSpellProjectileStun();
   ReliqueShop1.SetActive(true); //ajouter item à l'inventaire
  }

 }
 
 public void Choice4() 
 {
  if (CurrentGold >= Price4)
  {
   GreyChoice4.SetActive(true);
   ImageChoice4.SetActive(false);
   GameManager.SpellProjectileSpeed();
   //ajouter item à l'inventaire

  }

 }
 
 public void Choice5()
 {
  if (CurrentGold >= Price5)
  {
   GreyChoice5.SetActive(true);
   ImageChoice5.SetActive(false);
   GameManager.TrinketSpecialAttackCooldown();
   //ajouter item à l'inventaire
  }
 }
 
 public void Choice6()
 {
  if (CurrentGold >= Price6)
  {
   GreyChoice6.SetActive(true);
   ImageChoice6.SetActive(false);
   GameManager.SetRelicDoubleDash();
   //ajouter item à l'inventaire

  }
 }


 public void BloodButton()
 {
  // bouton pour donner de la vie aux marchand contre du gold
 }
 
}
