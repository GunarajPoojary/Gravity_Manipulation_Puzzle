using UnityEngine;

namespace GravityManipulationPuzzle
{
    [DefaultExecutionOrder(-2)]
    [RequireComponent(typeof(RectTransform))]
    public class SafeAreaPanel : MonoBehaviour
    {
        private RectTransform _panel;

        private void Awake()
        {
#if UNITY_ANDROID || UNITY_IOS
            Initialize();
#endif
        }

        private void Initialize()
        {
            _panel = GetComponent<RectTransform>();
            ApplySafeArea();
        }

        private void ApplySafeArea()
        {
            Rect safeArea = Screen.safeArea;

            Vector2 anchorMin = safeArea.position;
            Vector2 anchorMax = safeArea.position + safeArea.size;

            anchorMin.x /= Screen.width;
            anchorMin.y /= Screen.height;
            anchorMax.x /= Screen.width;
            anchorMax.y /= Screen.height;

            _panel.anchorMin = anchorMin;
            _panel.anchorMax = anchorMax;
            _panel.offsetMin = Vector2.zero;
            _panel.offsetMax = Vector2.zero;

#if UNITY_EDITOR
            Debug.Log($"[SafeAreaPanel] Applied safe area: {safeArea}");
#endif
        }
    }
}