using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Audio {
    [CreateAssetMenu(order = 220)]
    public class AmbienceSet : ScriptableObject {

        public Clip[] clips;

        [Space(10)] [Header("Randomizer:")]
        // ===
        public float minRandomTime = 0f;

        public float maxRandomTime = 30f;


        /// <summary>
        /// Called from the inspector button.
        /// Randomize all the loopTimes within a given range.
        /// Randomize the startTime between 0 and max value.
        /// </summary>
        public void RandomizeTimes() {
            for ( int i = 0; i < clips.Length; i++ ) {
                clips[i].loopTime  = Random.Range(minRandomTime, maxRandomTime);
                clips[i].startTime = Random.Range(0f,            clips[i].loopTime);
            }
        }


        /// <summary>
        /// Called from the inspector button.
        /// Set all clips volume to 1
        /// </summary>
        public void MaxVolume() {
            for ( int i = 0; i < clips.Length; i++ ) {
                clips[i].volumeScale  = 1f;
            }
        }


        /// <summary>
        /// The Clip Struct
        /// </summary>
        [Serializable]        // Needed to display the struct in the inspector
        public struct Clip {

            public                 AudioClip clip;
            public                 float     loopTime;
            public                 float     startTime;
            [Range(0f, 1f)] public float     volumeScale;

        }

    }
}