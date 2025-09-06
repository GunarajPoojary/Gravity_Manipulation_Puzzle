using GravityManipulationPuzzle.Events;
using TMPro;
using UnityEngine;

namespace GravityManipulationPuzzle.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _collectedCountText;
        [SerializeField] private TMP_Text _timerText;
        [SerializeField] private GameEvents _gameEvents;

        private void OnEnable()
        {
            _gameEvents.UpdateCollectedCubeCountUIEvent.OnEventRaised += UpdateCollectedCountUI;
            _gameEvents.UpdateTimeUIEvent.OnEventRaised += UpdateTimeUI;
        }

        private void OnDisable()
        {
            _gameEvents.UpdateCollectedCubeCountUIEvent.OnEventRaised -= UpdateCollectedCountUI;
            _gameEvents.UpdateTimeUIEvent.OnEventRaised -= UpdateTimeUI;
        }

        private void UpdateCollectedCountUI((int collected, int total) collectorData)
        {
            _collectedCountText.text = $"Collected Cubes: {collectorData.collected} / {collectorData.total}";
        }

        public void UpdateTimeUI(int remainingTime)
        {
            int minutes = remainingTime / 60;
            int seconds = remainingTime % 60;
            _timerText.text = $"{minutes:00}:{seconds:00}";
        }
    }
}