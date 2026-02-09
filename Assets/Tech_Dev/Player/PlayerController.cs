using System;
using Tech_Dev.Procedural;
using UnityEditor.AssetImporters;
using UnityEngine;

namespace Tech_Dev.Player
{
    public class PlayerController : MonoBehaviour
    {
	    [SerializeField] private float _groundSpeed;
	    [SerializeField] private float _airSpeed;

	    [SerializeField] private float _jumpHeight;
	    [SerializeField] private float _jumpCooldown;
	    [SerializeField] private GroundDetector _groundDetector;

	    [SerializeField] private float _fastFallingFactor;

	    [SerializeField] private float _interactCooldown;
	    
	    private bool _isFacingRight = true;
	    
	    private Rigidbody _rb;
	    private InputManager _inputs;
	    private Vector3 _initialGravity;

	    private float _jumpTimeDelta;

	    private float _interactTimeDelta;

	    private void Start()
	    {
		    _rb = GetComponent<Rigidbody>();
		    _inputs = GetComponent<InputManager>();
		    _jumpTimeDelta = 0;
		    _initialGravity = Physics.gravity;
	    }

	    private void Update()
	    {
		    Vector3 verticalMove = Vector3.zero;
		    
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

		    // Detect if contact with ground ==> make jump
		    if (_inputs.Jump && _groundDetector.Touched && _jumpTimeDelta <= 0.0f)
		    {
			    verticalMove = new Vector3(0, Mathf.Sqrt(_jumpHeight * -2.0f * _initialGravity.y), 0);
			    //! _rb.linearVelocity = new Vector3(0, _jumpHeight, 0);
			    _jumpTimeDelta = _jumpCooldown;
		    }

		    /* For constant jump (Mario like)
		    if (!_inputs.Jump && _rb.linearVelocity.y > 0)
		    {
			    _rb.linearVelocity = new Vector3(0, 0, 0);
		    }
		    */
		    
		    // Process values
		    float realGravity = _rb.linearVelocity.y >= 0 ? _rb.linearVelocity.y : _rb.linearVelocity.y * _fastFallingFactor;
		    realGravity = Mathf.Max(realGravity, -15);
		    
		    float realSpeed = _groundDetector.Touched ? _groundSpeed : _airSpeed;

		    Vector3 horizontalMove = new(_inputs.Move.x * realSpeed, 0, 0);

		    // Apply to physics
		    _rb.linearVelocity = new Vector3(0, realGravity, 0) + horizontalMove + verticalMove;



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
	    }
    }
}
