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
        
        public static GameObject Shop1Screen;
        public static GameObject Shop2Screen;
        public static GameObject UpgradeScreen;
        public static GameObject TrialScreen;
        public static GameObject WinDefeatScreen;

        private static GameObject _playerRef;
        private static PlayerController _playerScriptRef;
        public int RunTotalPlayerKills;

        private static GenerationManager _generationManager;

        private bool _noClip;

        public static bool IsGameLaunch = true;
        
        
        
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
            _playerRef.GetComponent<Rigidbody>().isKinematic = true;
            
            GameObject.FindWithTag("MainCamera").GetComponent<CinemachineCamera>().Target.TrackingTarget = _playerRef.transform;

            _generationManager = GameObject.FindWithTag("GenerationManager").GetComponent<GenerationManager>();

            Shop1Screen = GameObject.FindWithTag("Shop1");
            Shop1Screen.SetActive(false);
            
            Shop2Screen = GameObject.FindWithTag("Shop2");
            Shop2Screen.SetActive(false);
            
            UpgradeScreen = GameObject.FindWithTag("UpgradeScreen");
            UpgradeScreen.SetActive(false);
            
            TrialScreen = GameObject.FindWithTag("TrialScreen");
            TrialScreen.SetActive(false);
            
            WinDefeatScreen = GameObject.FindWithTag("WinDefeatScreen");
            WinDefeatScreen.SetActive(false);
        }

        public static GameObject GetShop1Screen()
        {
            return Shop1Screen;
        }

        public static GameObject GetShop2Screen()
        {
            return Shop2Screen;
        }

        public static GameObject GetUpgradeScreen()
        {
            return UpgradeScreen;
        }

        public static GameObject GetTrialScreen()
        {
            return TrialScreen;
        }
        
        public static GameObject GetWinDefeatScreen()
        {
            return WinDefeatScreen;
        }



        public static void StartGame()
        {
            GameObject.FindWithTag("SarcophagusTop").GetComponent<Animator>().SetTrigger("Open");
            
            _playerRef.GetComponent<Rigidbody>().isKinematic = false;
            
            IsGameLaunch = false;
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



        public static GameObject GetPlayerRef()
        {
            return _playerRef;
        }



        public static PlayerController GetPlayerScriptRef()
        {
            return _playerScriptRef;
        }
        
        
        
        //INFO upgrades, trinkets, and relics apply



        public void SetRelicSpellprojectileTeleport()
        {
            _playerScriptRef.HasProjectileTpUpgrade = true;
        }

        public void SetRelicSpellProjectileStun()
        {
            _playerScriptRef.HasProjectileStunUpgrade = true;
        }

        public void SetRelicDoubleDash()
        {
            _playerScriptRef.AddDashMaxNumber();
        }



        public void TrinketSpecialAttackCooldown()
        {
            _playerScriptRef.RunSpecialAttackCooldown *= 0.8f;
        }

        public void TrinketSpellProjectileDamage()
        {
            _playerScriptRef.RunSpecialAttackCooldown *= 1.25f;
        }

        public void TrinketSpellStun()
        {
            _playerScriptRef.RunStunTime *= 1.5f;
        }

        public void SpellProjectileSpeed()
        {
            _playerScriptRef.RunSpellProjectileSpeed *= 1.3f;
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

            if (Input.GetKeyDown(KeyCode.Keypad2))
            {
                if (!_noClip)
                {
                    _playerRef.GetComponent<CapsuleCollider>().enabled = false;
                    _playerRef.GetComponent<Rigidbody>().isKinematic = true;
                    _playerScriptRef.NoClip = true;
                    
                    GetCamera().GetComponent<CinemachineConfiner2D>().BoundingShape2D = null;
                    
                    _noClip = true;
                }
                else
                {
                    _playerRef.GetComponent<CapsuleCollider>().enabled = true;
                    _playerRef.GetComponent<Rigidbody>().isKinematic = false;
                    _playerScriptRef.NoClip = false;
                    
                    _noClip = false;
                }
            }

            if (Input.GetKeyDown(KeyCode.Keypad9))
            {
                StartCoroutine(_playerScriptRef.PlayerDeath());
            }

            if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                _playerScriptRef.GoBackToRoomEntry();
            }

            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                GameObject roomTeleporter = _playerScriptRef.GetCurrentRoom().RoomTeleporter;

                if (roomTeleporter.transform.childCount == 0)
                {
                    _playerScriptRef.ForceEnterNewRoom(roomTeleporter.GetComponent<Teleporter>());
                }
                else
                {
                    _playerScriptRef.ForceEnterNewRoom(roomTeleporter.transform.GetChild(0).GetComponent<Teleporter>());
                }
            }

            if (Input.GetKeyDown(KeyCode.KeypadPlus))
            {
                GetPlayerScriptRef().AddGold(1000);
            }
        }
    }
}
