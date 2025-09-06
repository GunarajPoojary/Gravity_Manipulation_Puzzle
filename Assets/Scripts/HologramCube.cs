using UnityEngine;

namespace GravityManipulationPuzzle
{
    public class HologramCube : MonoBehaviour, ICollectible
    {
        public void Collect() => gameObject.SetActive(false);
    }
}