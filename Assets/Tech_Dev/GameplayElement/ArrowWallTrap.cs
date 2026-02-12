using UnityEngine;

namespace Tech_Dev.GameplayElement
{
    public class ArrowWallTrap : MonoBehaviour
    {
        [SerializeField] private GameObject _arrows;
        [SerializeField] private Transform _shootPoint;
        
        [SerializeField] private float _arrowShootForce;
        [SerializeField] private float _arrowsDamage;
        [SerializeField] private float _shootCooldown;
        private float _shootTimer;

        private void Start()
        {
            _shootTimer = _shootCooldown;
        }

        private void Update()
        {
            _shootTimer -= Time.deltaTime;

            if (_shootTimer <= 0)
            {
                GameObject arrows = Instantiate(_arrows, _shootPoint.position, transform.rotation);
                arrows.GetComponent<Rigidbody>().AddForce(transform.forward * (_arrowShootForce * 10));
                arrows.GetComponent<FlyingArrows>().Damage = _arrowsDamage;
                _shootTimer = _shootCooldown;
            }
        }
    }
}
