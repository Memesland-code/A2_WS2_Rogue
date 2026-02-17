using UnityEngine;

namespace Tech_Dev.Procedural
{
    public class TrialRoom : MonoBehaviour
    {
        [Header("Do not fill, automatically filled from MainCanvas values")]
        public GameObject TrialCanvas;

        private void Start()
        {
            TrialCanvas = GameObject.FindWithTag("GameManager").GetComponent<GameManager>().TrialScreen;
            
            TrialCanvas.SetActive(false);
        }

        public void OpenTrialChoice()
        {
            TrialCanvas.SetActive(true);
            Debug.LogWarning("Ne pas oublier de lancer les trials à la fermeture ==> Voir TrialRoom.cs");
        }
        
        //!TODO A METTRE POUR ACTIVER LES ENNEMIS QUAND LE SCRREN SE FERME
        //GameManager.GetPlayerScriptRef().GetCurrentRoom().StartTrial();
    }
}
