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
    [SerializeField] private float CurrentHP;
    private float SaveCurrentHP;
    
    [SerializeField] private Image PvBar;
    
    [SerializeField] private TMP_Text PvCount;
    
  void Start()
  {
      MaxHP = GameManager.GetPlayerScriptRef().GetMaxHealth();  //récupérer les valeurs dans les scripts resepctif
      CurrentHP=GameManager.GetPlayerScriptRef().GetHealth(); // idem avec CurrentHP
      
      PvBar.fillAmount = CurrentHP / MaxHP;  // set le fillAmount de l'image  en des pv / pvMax 
      PvCount.SetText($"{CurrentHP}/{MaxHP}"); // écrire le nom de pv / pvMax
      SaveCurrentHP = CurrentHP; // save le nombre de pv pour en faire une comparaison plus tard voir pour le supprimer plus tard
     
  }

  void Update()
  {
      MaxHP = GameManager.GetPlayerScriptRef().GetMaxHealth();
      CurrentHP=GameManager.GetPlayerScriptRef().GetHealth();
      
      if (SaveCurrentHP != CurrentHP) // vérifier une modification de point de vie
      {
        PV_Change(); // lire ce qu'il y a d'écrit
      }
  }


  void PV_Change()
  {
      SaveCurrentHP = CurrentHP; // remettre save PV = current PV
      float PvPercent = CurrentHP / MaxHP; // set la valeur utiliser pour faire le pourcentage de point de vie
      PvBar.fillAmount = PvPercent; // modifier l'iamge en fonction du pourcentage de point de vie
      PvCount.SetText($"{CurrentHP}/{MaxHP}"); // afficher le nouveau nombre de point de vie
  }
}
