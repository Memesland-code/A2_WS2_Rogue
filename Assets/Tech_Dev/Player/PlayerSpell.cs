using System;
using System.Net;
using Tech_Dev.Enemies;
using UnityEngine;

namespace Tech_Dev.Player
{
    public class PlayerSpell : MonoBehaviour
    {
        private Rigidbody _rb;
        [SerializeField] private float _damage;
        public bool HasStunUpgrade;
        public bool HasTeleportUpgrade;
        
        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            Destroy(gameObject, 3);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Enemy"))
            {
                if (other.gameObject.TryGetComponent(out EnemyRat rat))
                {
                    rat.TakeDamage(_damage);
                }

                if (other.gameObject.TryGetComponent(out EnemySkull skull))
                {
                    skull.TakeDamage(_damage);
                }

                if (other.gameObject.TryGetComponent(out BossSkull bossSkull))
                {
                    bossSkull.TakeDamage(_damage);
                }
                
                if (HasStunUpgrade)
                {
                    Debug.LogWarning("Stun not implemented!");
                    //TODO stun enemies with AOE
                }
                
                if (HasTeleportUpgrade)
                {
                    GameManager.GetPlayerRef().transform.position = other.gameObject.transform.position + new Vector3(0, 5, 0);
                }
                
                Destroy(gameObject);
            }

            if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("Sword") || other.gameObject.CompareTag("PlayerRelated"))
            {
                
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}
