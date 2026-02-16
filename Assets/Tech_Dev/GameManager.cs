using Tech_Dev.Player;
using Tech_Dev.Procedural;
using Tech_Dev.UI;
using Unity.Cinemachine;
using UnityEngine;

namespace Tech_Dev
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private Canvas _fadeEffectPrefab;
        private static FadeEffect _fadeEffect;

        [SerializeField] private GameObject _enemyRatPrefab;
        [SerializeField] private GameObject _enemySkullPrefab;
        [SerializeField] private GameObject _bossPrefab;

        private static GameObject _playerRef;
        private static PlayerController _playerScriptRef;

        private static GenerationManager _generationManager;
        
        
        
        private void Awake()
        {
            _fadeEffect = GameObject.FindWithTag("CanvasFade").transform.GetChild(0).GetComponent<FadeEffect>();

            if (_fadeEffect == null)
            {
                Canvas fadeEffect = Instantiate(_fadeEffectPrefab);

                _fadeEffect = fadeEffect.transform.GetChild(0).GetComponent<FadeEffect>();
            }
            
            _playerRef = GameObject.FindWithTag("Player");
            _playerScriptRef = _playerRef.GetComponent<PlayerController>();
        }



        private void Start()
        {
            GameObject.FindWithTag("MainCamera").GetComponent<CinemachineCamera>().Target.TrackingTarget = _playerRef.transform;
        }

        
        
        public static FadeEffect GetFadeRef()
        {
            return _fadeEffect;
        }



        public GameObject GetEnemyRatPrefab()
        {
            return _enemyRatPrefab;
        }



        public GameObject GetEnemySkullPrefab()
        {
            return _enemySkullPrefab;
        }



        public GameObject GetBossPrefab()
        {
            return _bossPrefab;
        }



        public static GameObject GetCamera()
        {
            return GameObject.FindWithTag("MainCamera");
        }



        public static GenerationManager GetGenerationManagerRef()
        {
            return _generationManager;
        }
        
        
        
        //INFO CHEAT CODES


        private void Update()
        {
            // God mode
            if (Input.GetKeyDown(KeyCode.Keypad0))
            {
                _playerScriptRef.GodMode();
            }

            if (Input.GetKeyDown(KeyCode.KeypadMinus))
            {
                _fadeEffect.PlayFadeOut();
            }

            if (Input.GetKeyDown(KeyCode.Keypad1))
            {
                _playerScriptRef.GetCurrentRoom().KillAllEnemies();
            }
        }
    }
}
