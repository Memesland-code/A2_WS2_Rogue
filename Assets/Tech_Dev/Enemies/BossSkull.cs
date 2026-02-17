using System.Collections.Generic;
using Tech_Dev.Procedural;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Tech_Dev.Enemies
{
    public class BossSkull : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float _maxHealth;
        private float _health;

        [Header("Nav Mesh")]
        [SerializeField] private NavMeshAgent _agent;
        private Transform _player;
        [SerializeField] private LayerMask _whatIsGround, _whatIsPlayer;
        
        // Patrolling
        [Header("Patrolling")]
        [SerializeField] private Vector3 _walkPoint;
        private bool _walkPointSet;
        [SerializeField] private float _walkPointRange;
        
        // Attacking
        [Header("Attacking")]
        [SerializeField] private float _timeBetweenAttacks;
        private bool _alreadyAttacked;
        [SerializeField] private SkullProjectile _projectile;
        [SerializeField] private Transform _shootPoint;
        [SerializeField] private float _bulletForce;
        
        // States
        [Header("States")]
        [SerializeField] private float _sightRange, _attackRange;
        [SerializeField] private bool _playerInSightRange, _playerInAttackRange;

        [Header("Do not fill!")]
        public RoomManager RoomManagerReference;
        private bool _isDead;
        
        private bool _isStun;
        private float _stunTimer;

        
        
        private void Awake()
        {
            _health = _maxHealth;
            
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            if (_player == null)
            {
                Debug.LogError("Player not found by " + gameObject.name);
            }
            _agent = GetComponent<NavMeshAgent>();
        }



        private void Update()
        {
            if (_isStun)
            {
                _stunTimer -= Time.deltaTime;

                if (_stunTimer <= 0f) _isStun = false;
                
                return;
            }
            
            // Check for sight and attack range
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
            _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);
            
            if (!_playerInSightRange && !_playerInAttackRange) Patroling();
            if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
            if (_playerInAttackRange && _playerInSightRange) AttackPlayer();
        }



        private void Patroling()
        {
            if (!_walkPointSet) SearchWalkPoint();

            if (_walkPointSet)
            {
                _agent.SetDestination(_walkPoint);
            }
            
            Vector3 distanceToWalkPoint = transform.position - _walkPoint;
            
            // Walkpoint reached
            if (distanceToWalkPoint.magnitude < 1f)
                _walkPointSet = false;
        }

        private void SearchWalkPoint()
        {
            float randomX = Random.Range(-_walkPointRange, _walkPointRange);

            _walkPoint = new Vector3(transform.position.x + randomX, transform.position.y + 1, 0);

            if (Physics.Raycast(_walkPoint, -transform.up, 2f, _whatIsGround))
            {
                _walkPointSet = true;
            }
        }


        private void ChasePlayer()
        {
            _agent.SetDestination(_player.position);
        }



        private void AttackPlayer()
        {
            // Make sure enemy doesn't move
            _agent.SetDestination(transform.position);
            
            _shootPoint.LookAt(_player);
            transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));

            if (!_alreadyAttacked)
            {
                foreach (Transform shootPoint in _shootPoint)
                {
                    SkullProjectile projectile = Instantiate(_projectile, shootPoint.position, Quaternion.identity);
                    projectile.GetComponent<Rigidbody>().AddForce(shootPoint.forward * _bulletForce * 10);
                }
                
                _alreadyAttacked = true;
                Invoke(nameof(ResetAttack), _timeBetweenAttacks);
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
            if (_isDead) return;

            _isDead = true;
            GetComponent<BoxCollider>().enabled = false;
            
            if (RoomManagerReference != null)
            {
                RoomManagerReference.RegisterEnemyDeath(gameObject);
            }
            else
            {
                Debug.LogWarning("Enemy is not belonging to a room");
            }
            
            Destroy(gameObject);
        }
        
        
        
        public void Stun(float stunTime)
        {
            _isStun = true;
            _stunTimer = stunTime;
        }
        
        


        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _attackRange);
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position, _sightRange);
        }
    }
}
