using UnityEngine;
using UnityEngine.InputSystem;

namespace GravityManipulationPuzzle
{
    /// <summary>
    /// Provides the gravity direction for the gravity shift system.
    /// </summary>
    public interface IGravityDirectionProvider
    {
        Vector3 GravityDirection { get; }
    }

    /// <summary>
    /// Handles gravity shifting mechanics, including hologram preview and alignment.
    /// </summary>
    public class GravityShift : MonoBehaviour, IGravityDirectionProvider
    {
        [SerializeField] private GameObject _hologramPlayer;

        private Rigidbody _rb;
        private Transform _cam;

        private static readonly Vector3[] _worldDirectionAxes = new[]
        {
            Vector3.right,
            Vector3.left,
            Vector3.up,
            Vector3.down,
            Vector3.forward,
            Vector3.back
        };

        private Vector3 _inputGravityDirection;
        private Vector3 _lastFramePosition;
        private Quaternion _lastFrameRotation;
        private Vector3 _hologramGravityDirection;

        private const float GRAVITY = -9.81f;
        private Vector3 _gravityDirection;

        public Vector3 GravityDirection => _gravityDirection;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
            _cam = Camera.main.transform;

            _gravityDirection = Vector3.down;
            Physics.gravity = GRAVITY * -_gravityDirection;
            _hologramPlayer.SetActive(false);
        }

        private void Update()
        {
            if (_hologramPlayer.activeSelf)
                UpdateHologramPosition();
        }

        private void Start()
        {
            _lastFramePosition = transform.position;
            _lastFrameRotation = transform.rotation;
        }

        #region Input Methods
        /// <summary>
        /// Handles gravity shift preview input.
        /// </summary>
        /// <param name="ctx">Input action callback context.</param>
        public void OnGravityShiftPreview(InputAction.CallbackContext ctx)
        {
            if (!ctx.performed) return;

            var input = ctx.ReadValue<Vector2>();

            // Accept only cardinal directions or (0,0)
            if (!(Mathf.Abs(input.x) <= 1f && Mathf.Abs(input.y) <= 1f && (input.x == 0 || input.y == 0)))
            {
#if UNITY_EDITOR
                Debug.Log($"<color=red>Input is invalid {input}</color>");
#endif
                return;
            }

#if UNITY_EDITOR
            Debug.Log($"<color=green>Input is valid {input}</color>");
#endif

            _inputGravityDirection = new Vector3(input.x, 0f, input.y);

            _hologramPlayer.SetActive(true);
            _hologramPlayer.transform.SetPositionAndRotation(transform.position, transform.rotation);

            AlignHologramPreview();
        }

        public void OnDiscardGravityShiftPreview(InputAction.CallbackContext ctx) => _hologramPlayer.SetActive(false);

        /// <summary>
        /// Applies the gravity shift when the input is performed.
        /// </summary>
        /// <param name="ctx">Input action callback context.</param>
        public void OnApplyGravityShift(InputAction.CallbackContext ctx)
        {
            if (_hologramPlayer.activeSelf)
                ShiftGravity();
        }
        #endregion

        /// <summary>
        /// Updates the hologram's position to match the player's.
        /// </summary>
        private void UpdateHologramPosition()
        {
            if (transform.position != _lastFramePosition)
            {
                _hologramPlayer.transform.position = transform.position;
                _lastFramePosition = transform.position;
            }

            if (transform.rotation != _lastFrameRotation)
            {
                Quaternion rotationOffset = Quaternion.FromToRotation(transform.up, _hologramPlayer.transform.up);
                _hologramPlayer.transform.rotation = rotationOffset * transform.rotation;

                _lastFrameRotation = transform.rotation;
            }
        }

        /// <summary>
        /// Aligns the hologram based on the player's input direction and camera orientation.
        /// </summary>
        private void AlignHologramPreview()
        {
            Vector3 gravityUp = -Physics.gravity.normalized;
            Vector3 projectedForward = Vector3.ProjectOnPlane(_cam.forward, gravityUp).normalized;

            if (_inputGravityDirection == Vector3.right)
                AlignYAxisToClosestWorldAxis(_cam.right);
            else if (_inputGravityDirection == Vector3.left)
                AlignYAxisToClosestWorldAxis(-_cam.right);
            else if (_inputGravityDirection == Vector3.forward)
                AlignYAxisToClosestWorldAxis(projectedForward);
            else if (_inputGravityDirection == Vector3.back)
                AlignYAxisToClosestWorldAxis(-projectedForward);
        }

        private void ShiftGravity()
        {
            _gravityDirection = _hologramGravityDirection;

            transform.up = _hologramPlayer.transform.up;
            _hologramPlayer.SetActive(false);

            Vector3 projectedVelocity = Vector3.Project(_rb.linearVelocity, _gravityDirection);
            _rb.linearVelocity = projectedVelocity;

            Physics.gravity = GRAVITY * -_gravityDirection;
        }

        /// <summary>
        /// Aligns the hologram's Y-axis to the closest world axis.
        /// </summary>
        /// <param name="alignAxis">The axis to align towards.</param>
        private void AlignYAxisToClosestWorldAxis(Vector3 alignAxis)
        {
            float smallestAngle = float.MaxValue;
            Vector3 closestWorldAxis = Vector3.up;

            foreach (var worldAxis in _worldDirectionAxes)
            {
                float angle = Vector3.Angle(alignAxis, worldAxis);
                if (angle < smallestAngle)
                {
                    smallestAngle = angle;
                    closestWorldAxis = worldAxis;
                }
            }

            _hologramGravityDirection = closestWorldAxis;

            if (_hologramGravityDirection == -transform.up) return;

            _hologramPlayer.transform.rotation = Quaternion.FromToRotation(
                _hologramPlayer.transform.up, -closestWorldAxis
            ) * _hologramPlayer.transform.rotation;
        }
    }
}