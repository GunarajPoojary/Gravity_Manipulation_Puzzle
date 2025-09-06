using GravityManipulationPuzzle.Events;
using UnityEngine;

namespace GravityManipulationPuzzle
{
    public class FreeFallDetector : MonoBehaviour
    {
        [SerializeField] private float _checkRadius = 20f; // Radius of the sphere used to check for ground presence
        [SerializeField] private GameEvents _gameEvents;
        [SerializeField] private LayerMask _groundLayer;
        private bool _isNearGround;

        private void Update() => CheckGround();

        private void CheckGround()
        {
            Vector3 start = transform.position;

            // Perform a CheckSphere at the character's position to detect ground within the radius
            bool isGrounded = Physics.CheckSphere(start, _checkRadius, _groundLayer);

            // If the character was previously near the ground but now isn't, trigger Game Over
            if (!isGrounded && _isNearGround)
                _gameEvents.FreeFallEvent.RaiseEvent(null);

            // Update the ground status for the next frame
            _isNearGround = isGrounded;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = _isNearGround ? Color.green : Color.green;
            Gizmos.DrawWireSphere(transform.position, _checkRadius);
        }
    }
}