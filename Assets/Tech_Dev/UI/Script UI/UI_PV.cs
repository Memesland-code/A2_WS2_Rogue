using Tech_Dev;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Image = UnityEngine.UI.Image;
using Slider = UnityEngine.UI.Slider;

public class UI_PV : MonoBehaviour
{

    [SerializeField] private float MaxHP;
    
    [SerializeField] private Image PvBar;
    [SerializeField] private float CurrentHP;
    private float SaveCurrentHP;

    [SerializeField] private Image WoundBar;
    [SerializeField] private float CurrentWound;
    private float SaveCurrentWound;
    
    [SerializeField] private TMP_Text PvCount;
    
    
    [SerializeField] private KeyCode Test;
    
  void Start()
  {
      MaxHP = GameManager.GetPlayerScriptRef().GetMaxHealth();  //récupérer les valeurs dans les scripts resepctif
      CurrentHP=GameManager.GetPlayerScriptRef().GetHealth(); // idem avec CurrentHP
      CurrentWound = GameManager.GetPlayerScriptRef().GetCurrentWound();
      
      PvBar.fillAmount = CurrentHP / MaxHP;  // set le fillAmount de l'image  en des pv / pvMax 
      PvCount.SetText($"{CurrentHP}/{MaxHP}"); // écrire le nom de pv / pvMax
      SaveCurrentHP = CurrentHP; // save le nombre de pv pour en faire une comparaison plus tard voir pour le supprimer plus tard
      
      PvBar.fillAmount = CurrentWound / MaxHP  ;  // set le fillAmount de l'image  en des pv / pvMax 
      CurrentWound = CurrentHP;
      SaveCurrentWound = CurrentWound;

  }

  void Update()
  {
      MaxHP = GameManager.GetPlayerScriptRef().GetMaxHealth();
      CurrentHP=GameManager.GetPlayerScriptRef().GetHealth();
      
      CurrentWound = GameManager.GetPlayerScriptRef().GetCurrentWound();
      
      PvCount.SetText($"{CurrentHP}/{MaxHP}"); // écrire le nombre de pv / pvMax


 
      
      if (SaveCurrentHP != CurrentHP) // vérifier une modification de point de vie
      {
        PV_Change(); // lire ce qu'il y a d'écrit
      }

      if (SaveCurrentWound != CurrentWound)
      {
          Wound_Change();
      }
      
      
  }


  void PV_Change()
  {
      SaveCurrentHP = CurrentHP; // remettre save PV = current PV
      float PvPercent = CurrentHP / MaxHP; // set la valeur utiliser pour faire le pourcentage de point de vie
      PvBar.fillAmount = PvPercent; // modifier l'iamge en fonction du pourcentage de point de vie
      PvCount.SetText($"{CurrentHP}/{MaxHP}"); // afficher le nouveau nombre de point de vie
  }

  void Wound_Change()
  {
      SaveCurrentWound = CurrentWound; 
      float WoundPercent = CurrentWound / MaxHP;
      WoundBar.fillAmount = WoundPercent;
  }
}
