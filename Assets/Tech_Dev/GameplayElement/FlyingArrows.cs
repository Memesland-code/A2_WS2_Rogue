using Tech_Dev.Player;
using UnityEngine;

namespace Tech_Dev.GameplayElement
{
    public class FlyingArrows : MonoBehaviour
    {
        public float Damage;
        
        private float _curSpeed;
        private Vector3 _direction;

        private void Awake()
        {
            Destroy(gameObject, 5);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.gameObject.GetComponent<PlayerController>().Damage(Damage);
            }
        }
    }
}
