using Tech_Dev.Player;
using UnityEngine;

namespace Tech_Dev.Enemies
{
    public class SkullProjectile : MonoBehaviour
    {
        private Rigidbody _rb;
        [SerializeField] private float _damage;
        [SerializeField] private int _maxBounces;
        
        private Vector3 _lastVelocity;
        private float _curSpeed;
        private Vector3 _direction;
        private int _curBounces;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Destroy(gameObject, 10);
        }

        private void LateUpdate()
        {
            _lastVelocity = _rb.linearVelocity;
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collision.gameObject.GetComponent<PlayerController>().Damage(_damage);
                Destroy(gameObject);
            }
            
            if (_curBounces >= _maxBounces) Destroy(gameObject);
            
            _curSpeed = _lastVelocity.magnitude;
            _direction = Vector3.Reflect(_lastVelocity.normalized, collision.contacts[0].normal);

            _rb.linearVelocity = _direction * Mathf.Max(_curSpeed, 0);
            _curBounces++;
        }
    }
}
