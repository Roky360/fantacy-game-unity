using UnityEngine;

namespace Player
{
    public partial class PlayerController : MonoBehaviour
    {
        // [SerializeField] float movementSpeed = 8;
        // [SerializeField] float jumpForce = 8;
        // [SerializeField] float rotationSpeed = 2;
        // [SerializeField] float gravity = -15f;

        [SerializeField] Transform groundCheck;
        [SerializeField] LayerMask ground;

        private Rigidbody _rb;
        private CapsuleCollider _collider;
        private Animator _animator;
        private static readonly int Jump = Animator.StringToHash("Jump");
        private static readonly int IsMoving = Animator.StringToHash("isMoving");
        private static readonly int OnGround = Animator.StringToHash("onGround");

        // Start is called before the first frame update
        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _collider = GetComponent<CapsuleCollider>();
            _animator = GetComponent<Animator>();
            Physics.gravity = new Vector3(0, Stats.gravity, 0);
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetButtonDown("Jump") && IsOnGround())
            {
                _rb.velocity = new Vector3(_rb.velocity.x, Stats.jumpForce, _rb.velocity.z);
                _animator.SetTrigger(Jump);
            }
        }

        bool IsOnGround()
        {
            float radius = (float)(_collider.radius * .8);
            // return Physics.CheckSphere(groundCheck.position, 0.1f, ground);
            return Physics.CheckBox(groundCheck.position, new Vector3(radius, .1f, radius),
                new Quaternion(), ground);
        }

        // Updates physics related behaviors
        private void FixedUpdate()
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            // Movement
            _rb.velocity = new Vector3(
                horizontalInput * Stats.movementSpeed, _rb.velocity.y, verticalInput * Stats.movementSpeed);

            /* Face the character to the moving direction */
            // Get the direction the character is moving in
            Vector3 direction = _rb.velocity;
            direction.y = 0f; // Make sure the character doesn't tilt up or down

            // Only rotate if the character is moving
            if (direction.magnitude > 0.01f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
                transform.rotation =
                    Quaternion.Slerp(transform.rotation, targetRotation,
                        Time.fixedDeltaTime *
                        Stats.rotationSpeed); // Rotate the character towards the direction it's moving

                _animator.SetBool(IsMoving, true);
            }
            else
            {
                _animator.SetBool(IsMoving, false);
            }

            _animator.SetBool(OnGround, IsOnGround());
        }
    }
}