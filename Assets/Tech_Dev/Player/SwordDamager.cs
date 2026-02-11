using System.Collections.Generic;
using Tech_Dev.Enemies;
using UnityEngine;

namespace Tech_Dev.Player
{
    public class SwordDamager : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius;
        
        private List<GameObject> _enemiesInCollider = new();

        private void Update()
        {
            gameObject.GetComponent<SphereCollider>().radius = _detectionRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out EnemyRat _) || other.TryGetComponent(out EnemySkull _))
            {
                _enemiesInCollider.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyRat _) || other.TryGetComponent(out EnemySkull _))
            {
                _enemiesInCollider.Remove(other.gameObject);
            }
        }


        public List<GameObject> GetEnemiesInCollider()
        {
            return _enemiesInCollider;
        }
    }
}
