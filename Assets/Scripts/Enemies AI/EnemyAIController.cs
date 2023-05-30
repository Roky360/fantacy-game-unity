using UnityEngine;
using UnityEngine.Serialization;

namespace Enemies_AI
{
    public class EnemyAIController : MonoBehaviour
    {
        /* Public fields */
        public float detectionRange = 10f; // Range at which the enemy detects the player
        public float attackRange = 2f; // Range at which the enemy attacks the player
        public float attackMovementSpeed = 5f; // Movement speed of the enemy
        public float idleMovementSpeed = 2.5f; // Movement speed of the enemy
        public float attackDelay = 2f; // Delay between attacks
        public float minMoveDuration = 1.5f; // Minimum duration for random movement
        public float maxMoveDuration = 3f; // Maximum duration for random movement
        public float minIdleDuration = 1f;
        public float maxIdleDuration = 2f;
        public float maxPlayerDistance = 30f; // Maximum distance at which the enemy stops moving
        public float rotationSpeed = 7;
        
        /* Private fields */
        private Transform _player; // Reference to the player's transform
        private Animator _animator; // Reference to the animator component
        private Rigidbody _rb;
        
        private bool _isPlayerInRange; // Flag to track if the player is in range
        private bool _isAttacking; // Flag to track if the enemy is currently attacking
        private float _attackTimer; // Timer for tracking attack delays
        
        private Vector3 _idleMovingDirection;
        public float _moveTimer; // Timer for tracking movement duration
        public float _idleTimer;
        public bool isMoving;
        private Vector3 _targetPosition; // Random target position for movement
        
        /* Triggers string hashes */
        private static readonly int PlayerInRange = Animator.StringToHash("PlayerInRange");
        private static readonly int PlayerNotInRange = Animator.StringToHash("PlayerNotInRange");
        private static readonly int Active = Animator.StringToHash("Active");
        private static readonly int NotActive = Animator.StringToHash("NotActive");

        // Start is called before the first frame update
        protected void Start()
        {
            _player = GameObject.FindGameObjectWithTag("Player").transform; // Assuming the player has the "Player" tag
            Debug.Log(_player);
            _animator = GetComponent<Animator>(); // Assuming the enemy has an animator component
            _rb = GetComponent<Rigidbody>();
        }

        // Update is called once per frame
        protected void FixedUpdate()
        {
            // Calculate the distance between the enemy and the player
            float distanceToPlayer = Vector3.Distance(transform.position, _player.position);

            if (distanceToPlayer <= detectionRange)
            {
                // Player is in range
                _isPlayerInRange = true;
                _animator.SetTrigger(PlayerInRange);

                if (distanceToPlayer > attackRange)
                {
                    // Player is in attacking range but not close enough to attack yet
                    if (distanceToPlayer <= maxPlayerDistance)
                    {
                        MoveTowardsPlayer();
                    }
                }
                else
                {
                    // Player is in attack range
                    if (!_isAttacking)
                    {
                        // Start the attack if not currently attacking
                        Attack();
                    }
                }
            }
            else
            {
                // Player is not in range
                _isPlayerInRange = false;
                _animator.SetTrigger(PlayerNotInRange);

                // Stop moving if the player is too far away
                if (distanceToPlayer <= maxPlayerDistance)
                {
                    _animator.SetTrigger(Active);
                    MoveNaturally();
                }
                else
                {
                    _animator.SetTrigger(NotActive);
                }
            }

            // Handle attack delay
            if (_isAttacking)
            {
                _attackTimer += Time.deltaTime;
                if (_attackTimer >= attackDelay)
                {
                    _attackTimer = 0f;
                    _isAttacking = false;
                }
            }
        }

        // Move towards the player
        void MoveTowardsPlayer()
        {
            Vector3 direction = (_player.position - transform.position).normalized;
            direction.y = 0;

            // Move towards the player
            transform.Translate(direction * (attackMovementSpeed * Time.fixedDeltaTime), Space.World);
            // _rb.AddForce(direction * (attackMovementSpeed * Time.deltaTime));

            // rotate to the moving direction
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation =
                Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);
        }

        // Move naturally
        void MoveNaturally()
        {
            if (!isMoving)
            {
                _idleTimer -= Time.fixedDeltaTime;
                if (_idleTimer <= 0)
                {
                    isMoving = true;
                }
            }
            else
            {
                if (_moveTimer <= 0f)
                {
                    // Set a new random direction for movement
                    float angle = Random.Range(0f, 360f);
                    _idleMovingDirection = Quaternion.Euler(0f, angle, 0f) * transform.forward;

                    // Set a new random duration for movement
                    float duration = Random.Range(minMoveDuration, maxMoveDuration);
                    _moveTimer = duration;

                    // set idle timer
                    _idleTimer = Random.Range(minIdleDuration, maxIdleDuration);
                    isMoving = false;
                }

                // Move forward in the current direction
                transform.Translate(transform.forward * (idleMovementSpeed * Time.fixedDeltaTime), Space.World);
                // _rb.velocity = transform.forward * (idleMovementSpeed /** Time.deltaTime*/);

                // rotate to the moving direction
                Quaternion targetRotation = Quaternion.LookRotation(_idleMovingDirection, Vector3.up);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation, Time.fixedDeltaTime * rotationSpeed);

                // Update the movement timer
                _moveTimer -= Time.fixedDeltaTime;
            }
        }

        // // Move naturally
        // void MoveNaturally()
        // {
        //     if (_isMoving)
        //     {
        //         Quaternion targetRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
        //         // Rotate towards the movement direction
        //         transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime);
        //         // Move forward
        //         transform.Translate(Vector3.forward * Time.deltaTime);
        //         _moveTimer += Time.deltaTime;
        //     
        //         // Check if movement duration is reached
        //         if (_moveTimer >= currentMoveDuration)
        //         {
        //             _moveTimer = 0f;
        //             _isMoving = false;
        //             currentIdleDuration = Random.Range(minIdleDuration, maxIdleDuration);
        //         }
        //     }
        //     else
        //     {
        //         _moveTimer += Time.deltaTime;
        //
        //         // Check if idle duration is reached
        //         if (_moveTimer >= currentIdleDuration)
        //         {
        //             _moveTimer = 0f;
        //             isMoving = true;
        //             currentMoveDuration = Random.Range(minMoveDuration, maxMoveDuration);
        //             // Calculate a random rotation for the next movement direction
        //         }
        //     }
        // }

        // Perform the attack action
        void Attack()
        {
            // Implement your attack logic here
            // This could involve dealing damage to the player, playing attack animations, etc.

            _isAttacking = true;
        }
    }
}