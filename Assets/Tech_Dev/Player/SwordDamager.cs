using System;
using System.Collections.Generic;
using Tech_Dev.Enemies;
using UnityEngine;

namespace Tech_Dev.Player
{
    public class SwordDamager : MonoBehaviour
    {
        [SerializeField] private float _detectionRadius;
        private Vector3 _detectionZone;
        
        private List<GameObject> _enemiesInCollider = new();

        private void Update()
        {
            _detectionZone = new Vector3(_detectionRadius, _detectionRadius, 5);
            gameObject.GetComponent<BoxCollider>().size = _detectionZone;
        }

        private void OnTriggerEnter(Collider other)
        {
            CleanList();
            if (other.TryGetComponent(out EnemyRat _) || other.TryGetComponent(out EnemySkull _))
            {
                if (_enemiesInCollider.Contains(other.gameObject)) return;
                _enemiesInCollider.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            CleanList();
            if (other.TryGetComponent(out EnemyRat _) || other.TryGetComponent(out EnemySkull _))
            {
                _enemiesInCollider.Remove(other.gameObject);
            }
        }



        private void CleanList()
        {
            for (int i = _enemiesInCollider.Count - 1; i >= 0; i--)
            {
                if (_enemiesInCollider[i] == null) _enemiesInCollider.Remove(_enemiesInCollider[i]);
            }
        }


        public List<GameObject> GetEnemiesInCollider()
        {
            return _enemiesInCollider;
        }
    }
}
