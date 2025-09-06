using GravityManipulationPuzzle.Events;
using UnityEngine;

namespace GravityManipulationPuzzle
{
    /// <summary>
    /// Manages the collection of cubes in the game.
    /// </summary>
    public class Collector : MonoBehaviour
    {
        [SerializeField] private LayerMask _collectibeLayer;
        [SerializeField] private int _numberOfAvailableCubes = 5;
        [SerializeField] private GameEvents _gameEvents;

        private int _collectedCount = 0;

        private void Start() => _gameEvents.UpdateCollectedCubeCountUIEvent.RaiseEvent((_collectedCount, _numberOfAvailableCubes));

        private void OnTriggerEnter(Collider col)
        {
            if (((1 << col.gameObject.layer) & _collectibeLayer) != 0)
            {
                if (!col.TryGetComponent(out ICollectible collectible)) return;

                collectible.Collect();

                _collectedCount++;

                _gameEvents.UpdateCollectedCubeCountUIEvent.RaiseEvent((_collectedCount, _numberOfAvailableCubes));

                if (_collectedCount >= _numberOfAvailableCubes)
                    _gameEvents.GameCompleteEvent.RaiseEvent(null);
            }
        }
    }
}