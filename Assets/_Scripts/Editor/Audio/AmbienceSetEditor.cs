using UnityEditor;
using UnityEngine;

namespace Audio {

    [CustomEditor(typeof(AmbienceSet))]
    public class AmbienceSetEditor : Editor {

        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            serializedObject.Update();

            AmbienceSet myScript = (AmbienceSet) target;

            if ( GUILayout.Button("Randomize times") ) {
                myScript.RandomizeTimes();
            }

            if ( GUILayout.Button("Set All Volumes to 100%") ) {
                myScript.MaxVolume();
            }

            EditorGUILayout.HelpBox("You cannot undo the randomize function.", MessageType.Info, true);

        }

    }
}

