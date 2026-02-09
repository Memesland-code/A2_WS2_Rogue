using System;
using System.Collections;
using Tech_Dev.Procedural;
using UnityEngine;

namespace Tech_Dev.Player
{
    public class PlayerController : MonoBehaviour
    {
	    [SerializeField] private float _groundSpeed;
	    [SerializeField] private float _airSpeed;

	    [Space(10)]
	    [SerializeField] private float _jumpForce;
	    [SerializeField] private float _gravityScale;
	    [SerializeField] private GroundDetector _groundDetector;

	    [Space(5)]
	    [SerializeField] private float _dashForce;
	    [SerializeField] private float _dashMultiplierX;
	    [SerializeField] private float _dashMultiplierY;

	    [Obsolete("ddd")]
	    [SerializeField] private float _fastFallingFactor;

	    [Space(5)]
	    [SerializeField] private float _jumpCooldown;
	    [SerializeField] private float _interactCooldown;
	    [SerializeField] private float _dashCooldown;
	    [SerializeField] private float _dashTime;
	    
	    private bool _isFacingRight = true;
	    
	    private Rigidbody _rb;
	    private InputManager _inputs;

	    private float _jumpTimeDelta;
	    private float _interactTimeDelta;
	    private float _dashCooldownTimeDelta;
	    private float _dashTimeDelta;
	    
	    private bool _isDashing;

	    private void Start()
	    {
		    _rb = GetComponent<Rigidbody>();
		    _inputs = GetComponent<InputManager>();
		    _jumpTimeDelta = 0;
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
		    
		    // Check for ground
		    if (_groundDetector.Touched)
		    {
			    _jumpTimeDelta -= Time.deltaTime;
		    }
		    else
		    {
			    _jumpTimeDelta = _jumpCooldown;
		    }

		    float jumpForce = Mathf.Sqrt(_jumpForce * -3 * (Physics.gravity.y * _gravityScale));
		    // Detect if contact with ground ==> make jump
		    if (_inputs.Jump && _groundDetector.Touched && _jumpTimeDelta <= 0.0f)
		    {
			    _rb.linearVelocity = Vector3.zero;
			    _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
			    _jumpTimeDelta = _jumpCooldown;
		    }
		    
		    // Process values
		    float realGravity = _rb.linearVelocity.y >= 0 ? _rb.linearVelocity.y : _rb.linearVelocity.y * _fastFallingFactor;
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
					    gameObject.transform.position = teleporter.GetDestination().gameObject.transform.position;
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
	    }


	    
	    private void FixedUpdate()
	    {
		    _rb.AddForce(Physics.gravity * ((_gravityScale - 1) * _rb.mass));
	    }
	    
	    //TODO FIX too fast up ; FIX blocked moment when touching back ground after dash
    }
}
