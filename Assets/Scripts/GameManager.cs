using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GravityManipulationPuzzle
{
    [DefaultExecutionOrder(-2)]
    public class GameManager : MonoBehaviour
    {
        // Singleton instance for easy access
        public static GameManager Instance { get; private set; }

        [SerializeField] private TMP_Text _gameStateText;

        private void Awake()
        {
            Instance = this;

            _gameStateText.gameObject.SetActive(false);

            Time.timeScale = 1;

            if (_gameStateText == null)
            {
                Debug.LogWarning("GameOverText is not assigned in the inspector.", this);
                return;
            }

            // Ensure the game state text is hidden
            _gameStateText.gameObject.SetActive(false);
        }

        public void RestartGame() => SceneManager.LoadSceneAsync(0);

        public void QuitGame() => Application.Quit();

        public void GameWon()
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