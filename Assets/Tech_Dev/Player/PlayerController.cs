using System;
using System.Collections;
using Tech_Dev.Enemies;
using Tech_Dev.Procedural;
using Tech_Dev.UI.Script_UI;
using Unity.Cinemachine;
using UnityEngine;
using Type = Tech_Dev.Procedural.Type;

namespace Tech_Dev.Player
{
    public class PlayerController : MonoBehaviour
    {
	    [Header("Player stats")]
	    [SerializeField] private float _maxHealth;
	    private float _health;
	    [SerializeField] private float _healthRecoverPercentage;
	    [SerializeField] private float _woundBarExpireTime;
	    [SerializeField] private float _interactCooldown;
	    
	    [Space(10), Header("Attack system")]
	    [SerializeField] private float _meleeAttackDamage;
	    [SerializeField] private float _heavyAttackDamage;
	    [SerializeField] private Transform _swordAttackCenter;
	    [SerializeField] private float _meleeCooldown;
	    [SerializeField] private float _heavyCooldown;
	    [SerializeField] private float _skillCooldown;
	    
	    [Space(10), Header("Ground movements")]
	    [SerializeField] private float _groundSpeed;
	    [SerializeField] private float _airSpeed;

	    [Space(10), Header("Jump")]
	    [SerializeField] private float _jumpForce;
	    [SerializeField] private float _gravityScale;
	    [SerializeField] private int _maxJumpsNumber;
	    [SerializeField] private GroundDetector _groundDetector;

	    [Space(5), Header("Dash")]
	    [SerializeField] private int _maxDashesNumber;
	    [SerializeField] private float _dashForce;
	    [SerializeField] private float _dashMultiplierX;
	    [SerializeField] private float _dashMultiplierY;
	    
	    [Space(5), Header("Abilities related")]
	    [SerializeField] private PlayerSpell _spellProjectile;
	    [SerializeField] private float _spellDamage;
	    [SerializeField] private float _spellStunTime;
	    [SerializeField] private float _projectileForce;
	    [SerializeField] private Transform _shootPoint;
	    [SerializeField] private float _dashCooldown;
	    [SerializeField] private float _dashTime;
	    
	    [Space(10), Header("Do not touch! - tech references")]
	    [SerializeField] private PhysicsMaterial _groundMaterial;
	    [SerializeField] private PhysicsMaterial _airMaterial;
	    [SerializeField] private float _teleportationFadeTime;

	    private RoomManager _currentRoom;
	    
	    private bool _isFacingRight = true;
	    
	    private Rigidbody _rb;
	    private InputManager _inputs;
	    private Animator _animator;
	    
	    private bool _isDead;
	    public bool IsVictory;
	    
	    private SwordDamager _swordDamager;

	    private const float FastFallingFactor = 1.05f;

	    private float _interactTimeDelta;
	    private float _dashCooldownTimeDelta;
	    private float _dashTimeDelta;
	    private float _fastDashAvoid;
	    private float _meleeTimeDelta;
	    private float _heavyTimeDelta;
	    private float _skillTimeDelta;

	    private int _currentJumpsCombo;
	    private bool _isDashing;
	    private int _currentDashesNumber;
	    public bool HasProjectileStunUpgrade;
	    public bool HasProjectileTpUpgrade;

	    private bool _woundBarActive;
	    private float _woundBarTimer;
	    private float _woundDamageAmount;

	    private GameObject _camera;
	    

	    private int _gold;
	    private int _souls;


	    public float RunSpecialAttackCooldown;
	    public float RunSpellProjectileDamage;
	    public float RunStunTime;
	    public float RunSpellProjectileSpeed;
	    public float RunMaxHealth;

	    //Run related
	    public int TotalGoldGain;
	    public int TotalSoulGain;
	    public float RunTimer;
	    
	    
	    // Cheat codes
	    private bool _godMode;

	    public bool NoClip;
	    
	    
	    
	    private void Start()
	    {
		    _rb = GetComponent<Rigidbody>();
		    _inputs = GetComponent<InputManager>();
		    _swordDamager = GetComponentInChildren<SwordDamager>();
		    _camera = GameManager.GetCamera();
		    _animator = GetComponent<Animator>();
		    print(_animator.gameObject.name);
		    
		    ResetPlayer();
	    }



	    private void ResetPlayer()
	    {
		    _health = _maxHealth;

		    _currentRoom = GameObject.FindWithTag("Respawn").GetComponent<RoomManager>();

		    if (GameManager.IsGameLaunch)
		    {
			    //SoundManager.PlaySound(SoundType.CharacterRiseFromTomb);
			    transform.position = GameObject.FindWithTag("GameStart").transform.position;
		    }
		    else
		    {
				transform.position = _currentRoom.GetRoomEntryCoord();
		    }

		    _camera.GetComponent<CinemachineConfiner2D>().BoundingShape2D = _currentRoom.GetRoomBounds();

		    _isDead = false;

		    RunSpecialAttackCooldown = _heavyCooldown;
		    RunSpellProjectileDamage = _spellDamage;
		    RunStunTime = _spellStunTime;
		    RunSpellProjectileSpeed = _projectileForce;
		    RunMaxHealth = _maxHealth;

		    TotalGoldGain = 0;
		    TotalSoulGain = 0;
		    RunTimer = 0;
		    GameManager.SetTotalPlayerKills(0);
	    }

	    
	    
	    private void Update()
	    {
		    if (_currentRoom.Type != Type.Spawn) RunTimer += Time.deltaTime;
		    
		    if (transform.rotation.y == 0) transform.rotation = Quaternion.Euler(0, 90, 0);
		    
			// Check player facing direction and change on movement direction 
		    if (_inputs.Move.x < 0 && _isFacingRight)
		    {
			    _isFacingRight = false;
			    transform.rotation = Quaternion.Euler(0, -90, 0);
		    }
		    else if (_inputs.Move.x > 0 && !_isFacingRight)
		    {
			    _isFacingRight = true;
			    transform.rotation = Quaternion.Euler(0, 90, 0);
		    }
		    
		    // Reset jump combo on ground touch
		    if (_groundDetector.Touched) _currentJumpsCombo = 0;
		    
		    float jumpForce = Mathf.Sqrt(_jumpForce * -3 * (Physics.gravity.y * _gravityScale));
		    // Check input jump and if on ground and else if max jump count is not exceeded
		    if (_inputs.Jump && (_groundDetector.Touched || _currentJumpsCombo < _maxJumpsNumber))
		    {
			    _animator.SetTrigger("Jump");
			    SoundManager.PlaySound(SoundType.CharacterJump);
			    _rb.linearVelocity = Vector3.zero;
			    _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			    _currentJumpsCombo++;
		    }

		    if (!_groundDetector.Touched)
		    {
			    if (_rb.linearVelocity.magnitude > 0.0f)
			    {
				    GetComponent<CapsuleCollider>().sharedMaterial = _airMaterial;
			    }
		    }
		    else
		    {
			    GetComponent<CapsuleCollider>().sharedMaterial = _groundMaterial;
		    }
		    
		    
		    // Process values
		    float realGravity = _rb.linearVelocity.y >= 0 ? _rb.linearVelocity.y : _rb.linearVelocity.y * FastFallingFactor;
		    realGravity = Mathf.Max(realGravity, -15);
		    
		    float realSpeed = _groundDetector.Touched ? _groundSpeed : _airSpeed;
		    
		    Vector3 horizontalMove = new(_inputs.Move.x * realSpeed, 0, 0);


		    // Physics and noclip
		    if (NoClip)
		    {
			    if (_inputs.Move.x != 0) _animator.SetBool("IsMoving", true);
			    transform.position += new Vector3(_inputs.Move.x, _inputs.Move.y, 0);
		    }
		    else if (_isDashing)
		    {
			    Vector3 dashVelocity = new(_inputs.Move.x * _dashForce * _dashMultiplierX, _inputs.Move.y * _dashForce * _dashMultiplierY, 0);
			    _rb.linearVelocity = new Vector3(0, 0, 0) + horizontalMove + dashVelocity;
			    _animator.SetBool("IsMoving", false);
		    }
		    else
		    {
				_rb.linearVelocity = new Vector3(0, realGravity, 0) + horizontalMove;
				if (_inputs.Move.x != 0) _animator.SetBool("IsMoving", true);
		    }
		    
		    if (_inputs.Move.x == 0) _animator.SetBool("IsMoving", false);

		    
		    // Interaction
		    if (_inputs.Interact && _interactTimeDelta <= 0.0f)
		    {
			    var colliders = Physics.OverlapBox(transform.position, new Vector3(0.5f, 0.5f, 0.5f), Quaternion.Euler(0, 0, 0));
			    foreach (Collider coll in colliders)
			    {
				    if (coll.TryGetComponent(out Teleporter teleporter))
				    {
					    StartCoroutine(EnterNewRoom(teleporter));
				    }

				    if (coll.TryGetComponent(out ShopMerchant merchant))
				    {
					    merchant.OpenShop();
				    }

				    if (coll.TryGetComponent(out UpgradeRoom upgradeRoom))
				    {
					    upgradeRoom.OpenUpgradeChoice();
				    }

				    if (coll.TryGetComponent(out TrialRoom trialRoom))
				    {
					    trialRoom.OpenTrialChoice();
				    }

				    if (coll.TryGetComponent(out HealingRoomFountain healingFountain)) 
				    {
					    healingFountain.OpenHealingChoice();
				    }
			    }
			    _interactTimeDelta = _interactCooldown;
		    }
		    _interactTimeDelta -=  Time.deltaTime;
		    
		    
		    
		    // Dash system
		    if (_dashCooldownTimeDelta <= 0.0f)
		    {
			    _currentDashesNumber = 0;
		    }
		    
		    if (_inputs.Dash && (_dashCooldownTimeDelta <= 0.0f || (_currentDashesNumber < _maxDashesNumber && _fastDashAvoid <= 0.0f)))
		    {
			    SoundManager.PlaySound(SoundType.CharacterDash);
			    _rb.linearVelocity = Vector3.zero;
			    _dashCooldownTimeDelta = _dashCooldown;
			    _isDashing = true;
			    _dashTimeDelta = _dashTime;
			    _currentDashesNumber++;
			    _fastDashAvoid = 0.3f;
		    }
		    _dashCooldownTimeDelta -= Time.deltaTime;
		    _fastDashAvoid -= Time.deltaTime;

		    if (_isDashing)
		    {
			    _dashTimeDelta -= Time.deltaTime;
		    }

		    if (_dashTimeDelta <= 0.0f)
		    {
			    _isDashing = false;
		    }



		    // Attack system
		    //TODO Check for cooldown: animation time
		    if (_inputs.MeleeAttack && _meleeTimeDelta <= 0.0f)
		    {
			    _animator.SetTrigger("Attack");
			    SoundManager.PlaySound(SoundType.CharacterSwordHit);
			    bool damagedEnemy = false;
			    var enemiesInRange = _swordDamager.GetEnemiesInCollider();

			    foreach (GameObject enemy in enemiesInRange)
			    {
				    if (!enemy) return;
				    
				    damagedEnemy = ManageEnemyDamage(enemy, _meleeAttackDamage);
			    }
			    
			    if (damagedEnemy && _woundBarActive)
			    {
				    _health += Math.Clamp(_healthRecoverPercentage * _woundDamageAmount / 100, -1, _maxHealth);
				    _woundBarActive = false;
			    }

			    _meleeTimeDelta = _meleeCooldown;
		    }
		    _meleeTimeDelta -= Time.deltaTime;

		    if (_inputs.HeavyAttack && _heavyTimeDelta <= 0.0f)
		    {
			    SoundManager.PlaySound(SoundType.CharacterHeavyAttack);
			    bool damagedEnemy = false;
			    var enemiesInRange = _swordDamager.GetEnemiesInCollider();

			    foreach (GameObject enemy in enemiesInRange)
			    {
				    if (!enemy) return;

				    damagedEnemy = ManageEnemyDamage(enemy, _heavyAttackDamage);
			    }
			    
			    if (damagedEnemy && _woundBarActive)
			    {
				    _health += _healthRecoverPercentage * _woundDamageAmount / 100;
				    _woundBarActive = false;
			    }
			    
			    _heavyTimeDelta = RunSpecialAttackCooldown;
		    }
		    _heavyTimeDelta -= Time.deltaTime;

		    
		    
		    if (_inputs.Skill && _skillTimeDelta <= 0.0f)
		    {
			    _animator.SetTrigger("Spell");

			    StartCoroutine(LaunchSpell());
			    
			    _skillTimeDelta = _skillCooldown;
			    if (HasProjectileStunUpgrade == true)
			    {
				    SoundManager.PlaySound(SoundType.CharacterSpellElectric);
			    }

			    if (HasProjectileTpUpgrade == true)
			    {
				    SoundManager.PlaySound(SoundType.CharacterSpellElectric);
			    }
		    }
		    
		    _skillTimeDelta -= Time.deltaTime;
		    
		    
		    
		    // Wound system
		    if (_woundBarActive)
		    {
			    _woundBarTimer -= Time.deltaTime;
		    }

		    if (_woundBarTimer <= 0.0f)
		    {
			    _woundBarActive = false;
			    _woundBarTimer = 0.1f;
		    }

		    if (Input.GetKeyDown(KeyCode.U))
		    {
			    _animator.SetTrigger("Test");
		    }
	    }


	    public IEnumerator LaunchSpell()
	    {
		    yield return new WaitForSeconds(1.05f);
		    SoundManager.PlaySound(SoundType.CharacterSpell);
		    PlayerSpell spell = Instantiate(_spellProjectile, _shootPoint.position, Quaternion.identity);
		    spell.GetComponent<Rigidbody>().AddForce(_shootPoint.forward * (RunSpellProjectileSpeed * 10));
		    spell.HasStunUpgrade = HasProjectileStunUpgrade;
		    spell.HasTeleportUpgrade = HasProjectileTpUpgrade;
		    spell.Damage = RunSpellProjectileDamage;
		    spell.StunTime = RunStunTime;
	    }

	    

	    private void FixedUpdate()
	    {
		    _rb.AddForce(Physics.gravity * ((_gravityScale - 1) * _rb.mass));
	    }



	    public void ForceEnterNewRoom(Teleporter teleporter)
	    {
		    StartCoroutine(EnterNewRoom(teleporter));
	    }



	    private IEnumerator EnterNewRoom(Teleporter teleporter)
	    {
		    SoundManager.PlaySound(SoundType.DoorOpen);
		    GameManager.GetFadeRef().PlayFadeIn();
		    yield return new WaitForSeconds(1f);

		    if (_currentRoom.Type == Type.Boss)
		    {
			    AddSouls(5);
			    GameManager.GetGenerationManagerRef().ResetDungeon(false);
			    DungeonEndScreen(true);
			    ResetPlayer();
		    }
		    else if (_currentRoom.Type == Type.Fight)
		    {
			    AddGold(100);
		    }

		    if (_currentRoom.Difficulty == Difficulty.Ruins)
		    {
			    AddSouls(2);
		    }
		    
		    _currentRoom = teleporter.GetNextRoomRef();
		    gameObject.transform.position = teleporter.GetDestination().gameObject.transform.position;
		    SetNewCameraBounds(_currentRoom.GetRoomBounds());
		    
		    yield return new WaitForSeconds(_teleportationFadeTime);
		    _currentRoom.InitRoomOnEnter();
		    
		    GameManager.GetFadeRef().PlayFadeOut();
	    }



	    public void SetNewCameraBounds(Collider2D newCollider)
	    {
		    _camera.GetComponent<CinemachineConfiner2D>().BoundingShape2D = newCollider;
	    }



	    private bool ManageEnemyDamage(GameObject enemy, float damage)
	    {
		    if (enemy.TryGetComponent(out EnemyRat rat))
		    {
			    SoundManager.PlaySound(SoundType.RatDeath);
			    rat.TakeDamage(damage);
			    return true;
		    }

		    if (enemy.TryGetComponent(out EnemySkull skull))
		    {
			    SoundManager.PlaySound(SoundType.SkullDeath);
			    skull.TakeDamage(damage);
			    return true;
		    }

		    if (enemy.TryGetComponent(out BossSkull bossSkull))
		    {
			    SoundManager.PlaySound(SoundType.SkullDeath);
			    bossSkull.TakeDamage(damage);
			    return true;
		    }

		    return false;
	    }



	    public void Damage(float damage)
	    {
		    SoundManager.PlaySound(SoundType.CharacterHurt);
		    if (_godMode) return;
		    
		    _health -= damage;

		    _woundBarActive = true;
		    _woundBarTimer = _woundBarExpireTime;
		    _woundDamageAmount = damage;
		    
		    if (_health <= 0)
		    {
			    StartCoroutine(PlayerDeath());
		    }
	    }



	    public IEnumerator PlayerDeath()
	    {
		    if (!_isDead) _isDead = true;
		    
		    _animator.SetTrigger("Die");
		    SoundManager.PlaySound(SoundType.CharacterDeath);
		    yield return new WaitForSeconds(4f);
		    
		    GameManager.GetFadeRef().PlayFadeIn();

		    //TODO send info to GameManager? (may avoid bugs)
		    //TODO reset in-run related elements?
		    
		    _currentRoom.KillAllEnemies();
		    
		    GameManager.GetGenerationManagerRef().ResetDungeon(false);
		    
		    foreach (Transform hubElement in GameObject.FindWithTag("Respawn").transform)
		    {
			    if (hubElement.gameObject.CompareTag("RoomEntry"))
			    {
				    transform.position = hubElement.position;
			    }
		    }
		    
		    DungeonEndScreen(false);
		    
		    ResetPlayer();
	    }



	    public void AddGold(int amount)
	    {
		    _gold += amount;
		    TotalGoldGain += amount;
	    }

	    public bool RemoveGold(int amount)
	    {
		    if (_gold - amount >= 0)
		    {
			    SoundManager.PlaySound(SoundType.MerchantBuy);
			    _gold -= amount;
			    return true;
		    }

		    return false;
	    }

	    public void AddSouls(int amount)
	    {
		    _souls += amount;
		    TotalSoulGain += amount;
	    }

	    public bool RemoveSoul(int amount)
	    {
		    if (_souls - amount >= 0)
		    {
			    _souls -= amount;
			    return true;
		    }
		    return false;
	    }

	    public bool RemoveBlood(float amount)
	    {
		    if (_health - amount > 0)
		    {
			    SoundManager.PlaySound(SoundType.MerchantBlood);
			    _health -= amount;
			    return true;
		    }
		    return false;
	    }



	    public float GetHealth()
	    {
		    return _health;
	    }



	    public float GetMaxHealth()
	    {
		    return _maxHealth;
	    }
	    
	    public float GetCurrentWound() // Modification de Baptiste pour récupérer les wound pour la barre de vie
	    {
		    return _woundDamageAmount;
	    }

	    public void AddDashMaxNumber()
	    {
		    _maxDashesNumber++;
	    }

	    public int GetGold()
	    {
		    return _gold;
	    }

	    public int GetSouls()
	    {
		    return _souls;
	    }

	    public void SetMaxHealth(float newMaxHealth)
	    {
		    _maxHealth = newMaxHealth;
	    }

	    public void DungeonEndScreen(bool isWin)
	    {
		    WinDefeatUI endingScreen = GameManager.GetWinDefeatScreen().GetComponent<WinDefeatUI>();
		    
		    if (isWin)
		    {
			    endingScreen.RunState = "Victory!";
		    }
		    else
		    {
			    endingScreen.RunState = "Defeat...";
		    }
		    GameManager.GetWinDefeatScreen().SetActive(true);
	    }
	    
	    
	    public void GodMode()
	    {
		    _godMode = !_godMode;
	    }

	    public RoomManager GetCurrentRoom()
	    {
		    return _currentRoom;
	    }

	    public void GoBackToRoomEntry()
	    {
		    transform.position = _currentRoom.GetRoomEntryCoord();
	    }
    }
}
