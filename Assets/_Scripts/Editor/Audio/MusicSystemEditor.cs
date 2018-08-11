using UnityEditor;
using UnityEngine;

namespace Audio {
    [CustomEditor(typeof(MusicSystem))]
    public class MusicSystemEditor : Editor {

        SerializedProperty _times;

        private void OnEnable() { _times = serializedObject.FindProperty("_times"); }


        public override void OnInspectorGUI() {
            DrawDefaultInspector();
            serializedObject.Update();

            EditorGUILayout.Space();
            EditorGUILayout.Space();
            EditorGUILayout.Space();

            MusicSystem myScript = (MusicSystem) target;

            if ( GUILayout.Button("Play") ) {
                myScript.Play();
            }

            if ( GUILayout.Button("Stop") ) {
                myScript.Stop();
            }


            SerializedProperty timesArraySize = _times.FindPropertyRelative("Array.size");
            for ( int i = 0; i < myScript.set.clips.Length && i < timesArraySize.intValue; i++ ) {
                // Creates progress bar
                ProgressBar(_times.GetArrayElementAtIndex(i).floatValue / myScript.set.clips[i].loopTime,
                            i.ToString());
            }
        }


        /// <summary>
        ///  Creates a visual progress bar
        /// </summary>
        /// <param name="value"></param>
        /// <param name="label"></param>
        private void ProgressBar(float value, string label) {
            // Get a rect for the progress bar using the same margins as a textfield:
            Rect rect = GUILayoutUtility.GetRect(18, 18, "TextField");
            EditorGUI.ProgressBar(rect, value, label);
            EditorGUILayout.Space();
        }

    }
}