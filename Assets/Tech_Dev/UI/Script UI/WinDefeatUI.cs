using System;
using TMPro;
using UnityEngine;

namespace Tech_Dev.UI.Script_UI
{
    public class WinDefeatUI : MonoBehaviour
    {
        public string RunState;
        [SerializeField] private TextMeshProUGUI _runState;
        [SerializeField] private TextMeshProUGUI _totalKills;
        [SerializeField] private TextMeshProUGUI _totalGoldGain;
        [SerializeField] private TextMeshProUGUI _totalSoulsGain;
        [SerializeField] private TextMeshProUGUI _runTime;
        
        private void OnEnable()
        {
            if (GameManager.GetPlayerScriptRef() == null)
            {
                return;
            }
            
            _runState.text = RunState;
            _totalKills.text = GameManager.GetTotalPlayerKills().ToString();
            _totalGoldGain.text = GameManager.GetPlayerScriptRef().TotalGoldGain.ToString();
            _totalSoulsGain.text = GameManager.GetPlayerScriptRef().TotalSoulGain.ToString();
            _runTime.text = Math.Floor(GameManager.GetPlayerScriptRef().RunTimer).ToString();
        }

        public void BackToHub()
        {
            GameManager.GetFadeRef().PlayFadeOut();
            gameObject.SetActive(false);
        }
    }
}
