using TMPro;
using UnityEngine;

namespace GravityManipulationPuzzle
{
    /// <summary>
    /// Handles updating the UI for the Collector system.
    /// </summary>
    public class CollectorUI : MonoBehaviour
    {
        [SerializeField] private Collector _collector;
        [SerializeField] private TMP_Text _collectedCountText;

        public void UpdateCollectedCountUI(int collected, int total)
        {
            _collectedCountText.text = $"Collected Cubes: {collected} / {total}";
        }
    }
}
