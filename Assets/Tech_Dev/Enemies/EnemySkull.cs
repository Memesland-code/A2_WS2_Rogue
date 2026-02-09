using System;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Tech_Dev.Enemies
{
    public class EnemySkull : MonoBehaviour
    {
        private float _health;
        
        public NavMeshAgent Agent;
        public Transform Player;
        public LayerMask WhatIsGround, WhatIsPlayer;
        
        // Patrolling
        public Vector3 WalkPoint;
        private bool _walkPointSet;
        private float _walkPointRange;
        
        // Attacking
        public float TimeBetweenAttacks;
        private bool _alreadyAttacked;
        public GameObject Projectile;
        
        // States
        public float SightRange, AttackRange;
        public bool PlayerInSightRange, PlayerInAttackRange;

        
        
        private void Awake()
        {
            Player = GameObject.FindGameObjectWithTag("Player").transform;
            Agent = GetComponent<NavMeshAgent>();
        }



        private void Update()
        {
            // Check for sight and attack range
            PlayerInSightRange = Physics.CheckSphere(transform.position, SightRange, WhatIsPlayer);
            PlayerInAttackRange = Physics.CheckSphere(transform.position, AttackRange, WhatIsPlayer);
            
            if (!PlayerInSightRange && !PlayerInAttackRange) Patroling();
            if (PlayerInSightRange && !PlayerInAttackRange) ChasePlayer();
            if (PlayerInAttackRange && PlayerInSightRange) AttackPlayer();
        }



        private void Patroling()
        {
            if (!_walkPointSet) SearchWalkPoint();

            if (_walkPointSet)
            {
                Agent.SetDestination(WalkPoint);
            }
            
            Vector3 distanceToWalkPoint = transform.position - WalkPoint;
            
            // Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                _walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            float randomX = Random.Range(-_walkPointRange, _walkPointRange);

            WalkPoint = new Vector3(transform.position.x + randomX, transform.position.y, 0);

            if (Physics.Raycast(WalkPoint, -transform.up, 2f, WhatIsGround))
            {
                _walkPointSet = true;
            }
        }


        private void ChasePlayer()
        {
            Agent.SetDestination(Player.position);
        }



        private void AttackPlayer()
        {
            // Make sure enemy doesn't move
            Agent.SetDestination(transform.position);
            
            transform.LookAt(Player);

            if (!_alreadyAttacked)
            {
                Rigidbody rb = Instantiate(Projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
                rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
                rb.AddForce(transform.up * 8f, ForceMode.Impulse);
                
                _alreadyAttacked = true;
                Invoke(nameof(ResetAttack), TimeBetweenAttacks);
            }
        }

        private void ResetAttack()
        {
            _alreadyAttacked = false;
        }

        
        
        public void TakeDamage(float damage)
        {
            _health -= damage;

            if (_health <= 0)
            {
                Invoke(nameof(EnemyDeath), 0.5f);
            }
        }

        private void EnemyDeath()
        {
            Destroy(gameObject);
        }


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, AttackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, SightRange);
        }
    }
}
