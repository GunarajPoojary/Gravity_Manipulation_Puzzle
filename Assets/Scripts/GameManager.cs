using GravityManipulationPuzzle.Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GravityManipulationPuzzle
{
    [DefaultExecutionOrder(-2)]
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private TMP_Text _gameStateText;
        [SerializeField] private GameEvents _gameEvents;

        private void Awake()
        {
            _gameStateText.gameObject.SetActive(false);

            Time.timeScale = 1;

            if (_gameStateText == null)
            {
                Debug.LogWarning("GameOverText is not assigned in the inspector.", this);
                return;
            }

            _gameStateText.gameObject.SetActive(false);
        }

        private void OnEnable()
        {
            _gameEvents.GameCompleteEvent.OnEventRaised += GameWon;
            _gameEvents.FreeFallEvent.OnEventRaised += HandleFreeFall;
            _gameEvents.TimeEndEvent.OnEventRaised += HandleTimeOver;
        }

        private void OnDisable()
        {
            _gameEvents.GameCompleteEvent.OnEventRaised -= GameWon;
            _gameEvents.FreeFallEvent.OnEventRaised -= HandleFreeFall;
            _gameEvents.TimeEndEvent.OnEventRaised -= HandleTimeOver;
        }

        private void HandleFreeFall(Empty e = null) => GameOver("Free Fall!");

        private void HandleTimeOver(Empty e = null) => GameOver("Time Over");

        public void RestartGame() => SceneManager.LoadSceneAsync(0);

        public void QuitGame() => Application.Quit();

        private void GameWon(Empty e = null)
        {
            _gameStateText.gameObject.SetActive(true);
            _gameStateText.text = "You won!";

            // Pause the game
            Time.timeScale = 0;
        }

        public void GameOver(string text)
        {
            _gameStateText.gameObject.SetActive(true);
            _gameStateText.text = text;

            Time.timeScale = 0;
        }
    }
}