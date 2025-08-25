using UnityEditor;
using static UnityEditor.EditorGUILayout;
using UnityEngine;

namespace GravityManipulationPuzzle
{
    public class ProceduralLevelGenerator : EditorWindow
    {
        private int _floors;
        private float _floorHeight;
        private float _wallThickness;
        private float _length;
        private float _width;
        private bool _hasRoof = false;
        private Color _previewColor = Color.blue;

        [MenuItem("Tools/Procedural Level Generator")]
        public static void ShowWindow()
        {
            GetWindow<ProceduralLevelGenerator>("Procedural Level Generator");
        }

        private void OnGUI()
        {
            Space(10);

            GUILayout.Label("Procedural Building", EditorStyles.boldLabel);

            HelpBox("Use this tool to create procedural building", MessageType.Info);

            GUILayout.Label("Building Dimensions", EditorStyles.toolbarButton);

            _floors = IntField(new GUIContent("Floors", "Number of floors for building"), _floors);

            _wallThickness = Slider("Wall Thickness", _wallThickness, 0.1f, 2f);

            BeginHorizontal();

            _length = FloatField("Length", _length);

            EndHorizontal();
        }
    }
}