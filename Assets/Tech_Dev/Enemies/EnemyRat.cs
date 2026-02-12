using Tech_Dev.Player;
using Tech_Dev.Procedural;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace Tech_Dev.Enemies
{
    public class EnemyRat : MonoBehaviour
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
        [SerializeField] private float _damage;
        [SerializeField] private Transform _attackPoint;
        [SerializeField] private float _damageRange;
        [SerializeField] private float _attackCooldown;
        
        // States
        [Header("States")]
        [SerializeField] private float _sightRange, _attackRange;
        [SerializeField] private bool _playerInSightRange, _playerInAttackRange;

        public RoomManager RoomManagerReference;
        private bool _isDead;

        private float _baseSpeed;
        private float _baseAcceleration;
        
        private float _boostSpeed;
        private float _boostAcceleration;

        private float _attackTimer;

        
        
        private void Awake()
        {
            _health = _maxHealth;
            
            _baseSpeed = _agent.speed;
            _baseAcceleration = _agent.acceleration;

            _boostSpeed = _baseSpeed * 2;
            _boostAcceleration = _baseAcceleration * 2;

            _attackTimer = _attackCooldown;
            
            _player = GameObject.FindGameObjectWithTag("Player").transform;
            if (_player == null)
            {
                Debug.LogError("Player not found by " + gameObject.name);
            }
            _agent = GetComponent<NavMeshAgent>();
        }



        private void Update()
        {
            _attackTimer -= Time.deltaTime;
            
            // Check for sight and attack range
            _playerInSightRange = Physics.CheckSphere(transform.position, _sightRange, _whatIsPlayer);
            _playerInAttackRange = Physics.CheckSphere(transform.position, _attackRange, _whatIsPlayer);
            
            if (!_playerInSightRange && !_playerInAttackRange) Patroling();
            if (_playerInSightRange && !_playerInAttackRange) ChasePlayer();
            if (_playerInAttackRange && _playerInSightRange && _attackTimer <= 0) AttackPlayer();
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
            transform.LookAt(new Vector3(_player.position.x, transform.position.y, _player.position.z));

            _agent.speed = _boostSpeed;
            _agent.acceleration = _boostAcceleration;
            _agent.SetDestination(_player.position);
            
            if (Physics.Raycast(_attackPoint.position, transform.forward, out RaycastHit hit, _damageRange))
            {
                Debug.DrawRay(transform.position, transform.forward * _damageRange, Color.red, 5f);
                
                if (hit.collider.CompareTag("Player"))
                {
                    hit.transform.gameObject.GetComponent<PlayerController>().Damage(_damage);
                    
                    _agent.speed = _baseSpeed;
                    _agent.acceleration = _baseAcceleration;
                }
            }
            
            _attackTimer = _attackCooldown;
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
                RoomManagerReference.RegisterEnemyDeath(this);
            }
            else
            {
                Debug.LogWarning("Enemy is not belonging to a room");
            }
            
            Destroy(gameObject);
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
