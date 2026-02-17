using System;
using System.Net;
using Tech_Dev.Enemies;
using UnityEngine;

namespace Tech_Dev.Player
{
    public class PlayerSpell : MonoBehaviour
    {
        private Rigidbody _rb;
        public float Damage;
        public float StunTime;
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
                    rat.TakeDamage(Damage);
                }

                if (other.gameObject.TryGetComponent(out EnemySkull skull))
                {
                    skull.TakeDamage(Damage);
                }

                if (other.gameObject.TryGetComponent(out BossSkull bossSkull))
                {
                    bossSkull.TakeDamage(Damage);
                }
                
                if (HasStunUpgrade)
                {
                    Collider[] stunnedEnemies = Physics.OverlapSphere(transform.position, 3f);

                    foreach (Collider col in stunnedEnemies)
                    {
                        if (col.TryGetComponent(out EnemyRat rat1))
                        {
                            rat1.Stun(StunTime);
                        }

                        if (col.TryGetComponent(out EnemySkull skull1))
                        {
                            skull1.Stun(StunTime);
                        }

                        if (col.TryGetComponent(out BossSkull bossSkull1))
                        {
                            bossSkull1.Stun(StunTime);
                        }
                    }
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
