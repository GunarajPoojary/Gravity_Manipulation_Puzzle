using UnityEngine;
using UnityEngine.InputSystem;

namespace GravityManipulationPuzzle
{
    [RequireComponent(typeof(CapsuleCollider), typeof(Rigidbody))]
    public class PlayerMovement : MonoBehaviour
    {
        [SerializeField] private float _moveSpeed = 5f;
        [SerializeField] private float _turnSmoothTime = 0.1f;
        [SerializeField] private Animator _animator;

        [Header("Jump Settings")]
        [SerializeField] private float _jumpForce = 5f;
        [SerializeField] private LayerMask _groundLayer;

        [SerializeField] private Transform _groundCheck;
        [SerializeField] private float _groundCheckRadius = 0.1f;
        [SerializeField] private float _fallDistanceThreshold = 1f;

        private Vector3 _movementInput;
        private Rigidbody _rb;
        private Transform _cam;
        private IGravityDirectionProvider _gravityShift;

        private static readonly int IsRunningHash = Animator.StringToHash("IsRunning");
        private static readonly int IsFallingHash = Animator.StringToHash("IsFalling");

        private bool _isGrounded;
        private bool _isFalling;

        private void Awake() => InitializeComponents();

        private void Update()
        {
            CheckGrounded();
            CheckFalling();
            UpdateAnimations();
        }

        private void FixedUpdate()
        {
            if (_movementInput.sqrMagnitude > 0.01f) Move();
        }

        private void InitializeComponents()
        {
            _cam = Camera.main.transform;
            _rb = GetComponent<Rigidbody>();
            _gravityShift = GetComponent<GravityShift>();

            if (_animator == null)
                Debug.Log("Animator component is not assigned");
        }

        #region Input Methods
        public void OnMove(InputAction.CallbackContext ctx)
        {
            var inputVector = ctx.ReadValue<Vector2>();

            _movementInput = new Vector3(inputVector.x, 0, inputVector.y);
        }

        public void OnJump(InputAction.CallbackContext ctx)
        {
            if (_isGrounded) Jump();
        }
        #endregion

        private void Move()
        {
            var gravityUp = -_gravityShift.GravityDirection.normalized;

            var moveDir = Vector3.ProjectOnPlane(_cam.forward * _movementInput.z + _cam.right * _movementInput.x, gravityUp).normalized;

            if (moveDir.sqrMagnitude >= 0.01f)
            {
                var targetRotation = Quaternion.LookRotation(moveDir, gravityUp);
                _rb.rotation = Quaternion.Slerp(_rb.rotation, targetRotation, _turnSmoothTime);
                _rb.MovePosition(_rb.position + _moveSpeed * Time.deltaTime * moveDir);
            }
        }

        private void Jump() => _rb.AddForce(-_gravityShift.GravityDirection * _jumpForce, ForceMode.Impulse);

        private void CheckGrounded() => _isGrounded = Physics.CheckSphere(_groundCheck.position, _groundCheckRadius, _groundLayer);

        private void CheckFalling()
        {
            // Cast a ray downward to check if the ground is within the fall threshold
            bool isGroundBelow = Physics.Raycast(_groundCheck.position, -_groundCheck.up, out RaycastHit hit, _fallDistanceThreshold, _groundLayer);

            _isFalling = !isGroundBelow && !_isGrounded;
        }

        private void UpdateAnimations()
        {
            bool isRunning = _movementInput.magnitude >= 0.1f && !_isFalling;

            _animator.SetBool(IsRunningHash, isRunning);
            _animator.SetBool(IsFallingHash, _isFalling);
        }
    }
}