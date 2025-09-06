using UnityEngine;
using UnityEngine.Events;

namespace GravityManipulationPuzzle.Events
{
    [CreateAssetMenu(menuName = "Custom/GameEvents")]
    public class GameEvents : ScriptableObject
    {
        public EventChannel<int> UpdateTimeUIEvent = new();
        public EventChannel<Empty> GameCompleteEvent = new();
        public EventChannel<Empty> FreeFallEvent = new();
        public EventChannel<(int, int)> UpdateCollectedCubeCountUIEvent = new();
        public EventChannel<Empty> TimeEndEvent = new();
    }

    public class Empty { }

    public class EventChannel<T>
    {
        public event UnityAction<T> OnEventRaised;
        public void RaiseEvent(T value) => OnEventRaised?.Invoke(value);
    }
}