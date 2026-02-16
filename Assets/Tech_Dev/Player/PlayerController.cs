using System;
using System.Collections;
using Tech_Dev.Enemies;
using Tech_Dev.Procedural;
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
	    
	    [Space(10), Header("Attack system")]
	    [SerializeField] private float _meleeAttackDamage;
	    [SerializeField] private float _heavyAttackDamage;
	    [SerializeField] private Transform _swordAttackCenter;
	    
	    [Space(10), Header("Ground movements")]
	    [SerializeField] private float _groundSpeed;
	    [SerializeField] private float _airSpeed;

	    [Space(10), Header("Jump")]
	    [SerializeField] private float _jumpForce;
	    [SerializeField] private float _gravityScale;
	    [SerializeField] private int _maxJumpsNumber;
	    [SerializeField] private GroundDetector _groundDetector;

	    [Space(5), Header("Dash")]
	    [SerializeField] private float _dashForce;
	    [SerializeField] private float _dashMultiplierX;
	    [SerializeField] private float _dashMultiplierY;
	    
	    [Space(5), Header("Abilities related")]
	    [SerializeField] private float _interactCooldown;
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
	    
	    private SwordDamager _swordDamager;

	    private const float FastFallingFactor = 1.05f;

	    private float _interactTimeDelta;
	    private float _dashCooldownTimeDelta;
	    private float _dashTimeDelta;

	    private int _currentJumpsCombo;
	    private bool _isDashing;

	    private bool _woundBarActive;
	    private float _woundBarTimer;
	    private float _woundDamageAmount;

	    private GameObject _camera;
	    
	    
	    // Cheat codes
	    private bool _godMode;
	    
	    
	    
	    private void Start()
	    {
		    _rb = GetComponent<Rigidbody>();
		    _inputs = GetComponent<InputManager>();
		    _swordDamager = GetComponentInChildren<SwordDamager>();
		    _camera = GameManager.GetCamera();
		    
		    ResetPlayer();
	    }



	    private void ResetPlayer()
	    {
		    _health = _maxHealth;

		    _currentRoom = GameObject.FindWithTag("Respawn").GetComponent<RoomManager>();

		    transform.position = _currentRoom.GetRoomEntryCoord();

		    _camera.GetComponent<CinemachineConfiner2D>().BoundingShape2D = _currentRoom.GetRoomBounds();
	    }

	    
	    
	    private void Update()
	    {
			// Check player facing direction and change on movement direction 
		    if (_inputs.Move.x < 0 && _isFacingRight)
		    {
			    _isFacingRight = false;
			    transform.rotation = Quaternion.Euler(0, 180, 0);
		    }
		    else if (_inputs.Move.x > 0 && !_isFacingRight)
		    {
			    _isFacingRight = true;
			    transform.rotation = Quaternion.Euler(0, 0, 0);
		    }
		    
		    // Reset jump combo on ground touch
		    if (_groundDetector.Touched) _currentJumpsCombo = 0;
		    
		    float jumpForce = Mathf.Sqrt(_jumpForce * -3 * (Physics.gravity.y * _gravityScale));
		    // Check input jump and if on ground and else if max jump count is not exceeded
		    if (_inputs.Jump && (_groundDetector.Touched || _currentJumpsCombo < _maxJumpsNumber))
		    {
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

		    // Apply to physics
		    if (_isDashing)
		    {
			    Vector3 dashVelocity = new(_inputs.Move.x * _dashForce * _dashMultiplierX, _inputs.Move.y * _dashForce * _dashMultiplierY, 0);
			    _rb.linearVelocity = new Vector3(0, 0, 0) + horizontalMove + dashVelocity;
		    }
		    else
		    {
				_rb.linearVelocity = new Vector3(0, realGravity, 0) + horizontalMove;
		    }

		    
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
			    }
			    _interactTimeDelta = _interactCooldown;
		    }
		    _interactTimeDelta -=  Time.deltaTime;
		    
		    
		    
		    // Dash system
		    if (_inputs.Dash && _dashCooldownTimeDelta <= 0.0f)
		    {
			    _rb.linearVelocity = Vector3.zero;
			    _dashCooldownTimeDelta = _dashCooldown;
			    _isDashing = true;
			    _dashTimeDelta = _dashTime;
		    }
		    _dashCooldownTimeDelta -= Time.deltaTime;

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
		    if (_inputs.MeleeAttack)
		    {
			    
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
		    }

		    if (_inputs.HeavyAttack)
		    {
			    bool damagedEnemy = false;
			    var enemiesInRange = _swordDamager.GetEnemiesInCollider();

			    foreach (GameObject enemy in enemiesInRange)
			    {
				    if (!enemy) return;

				    damagedEnemy = ManageEnemyDamage(enemy, _meleeAttackDamage);
			    }
			    
			    if (damagedEnemy && _woundBarActive)
			    {
				    _health += _healthRecoverPercentage * _woundDamageAmount / 100;
				    _woundBarActive = false;
			    }
		    }
		    
		    
		    
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
	    }

	    

	    private void FixedUpdate()
	    {
		    _rb.AddForce(Physics.gravity * ((_gravityScale - 1) * _rb.mass));
	    }



	    private IEnumerator EnterNewRoom(Teleporter teleporter)
	    {
		    GameManager.GetFadeRef().PlayFadeIn();
		    yield return new WaitForSeconds(1f);

		    print("Previous room: " + _currentRoom.RoomId);
		    if (_currentRoom.Type == Type.Boss) GameManager.GetGenerationManagerRef().InitRoomsGeneration();
		    
		    _currentRoom = teleporter.GetNextRoomRef();
		    gameObject.transform.position = teleporter.GetDestination().gameObject.transform.position;
		    SetNewCameraBounds(_currentRoom.GetRoomBounds());
		    
		    yield return new WaitForSeconds(_teleportationFadeTime);
		    _currentRoom.InitRoom();
		    print("New room: " + _currentRoom.RoomId);
		    
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
			    rat.TakeDamage(damage);
			    return true;
		    }

		    if (enemy.TryGetComponent(out EnemySkull skull))
		    {
			    skull.TakeDamage(damage);
			    return true;
		    }

		    if (enemy.TryGetComponent(out BossSkull bossSkull))
		    {
			    bossSkull.TakeDamage(damage);
			    return true;
		    }

		    return false;
	    }



	    public void Damage(float damage)
	    {
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



	    private IEnumerator PlayerDeath()
	    {
		    GameManager.GetFadeRef().PlayFadeIn();

		    //TODO send info to GameManager? (may avoid bugs)
		    //TODO reset in-run related elements?
		    foreach (Transform hubElement in GameObject.FindWithTag("Respawn").transform)
		    {
			    if (hubElement.gameObject.CompareTag("RoomEntry"))
			    {
				    transform.position = hubElement.position;
			    }
		    }
		    
		    ResetPlayer();
		    
		    yield return new WaitForSeconds(3f);
		    
		    GameManager.GetFadeRef().PlayFadeOut();
	    }



	    public float GetHealth()
	    {
		    return _health;
	    }



	    public float GetMaxHealth()
	    {
		    return _maxHealth;
	    }
	    
	    
	    
	    public void GodMode()
	    {
		    _godMode = !_godMode;
	    }

	    public RoomManager GetCurrentRoom()
	    {
		    return _currentRoom;
	    }
    }
}
