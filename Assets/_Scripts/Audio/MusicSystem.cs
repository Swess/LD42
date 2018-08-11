using System.Collections;
using UnityEngine;

namespace Audio {
    [RequireComponent(typeof(AudioSource))]
    public class MusicSystem : MonoBehaviour {

        public AmbienceSet set;
        public bool        playOnAwake = false;

        private AudioSource _audioPlayer;
        private bool        _canPlay = false;

        [SerializeField] [HideInInspector] private float[] _times;

        // ========================================================
        // ========================================================
        // ========================================================


        private void Awake() {
            _audioPlayer = GetComponent<AudioSource>();
            if ( playOnAwake ) Play();
        }


        private void Update() {
            if ( !_canPlay ) return;

            if ( !set ) {
                Debug.LogWarning("No AmbienceSet attached to the script");
                return;
            }

            for ( int i = 0; i < set.clips.Length; i++ ) {
                float time = _times[i];
                if ( time >= set.clips[i].loopTime ) {
                    _audioPlayer.PlayOneShot(set.clips[i].clip, set.clips[i].volumeScale);
                    _times[i] = 0f;
                } else {
                    _times[i] = time + Time.deltaTime;
                }
            }
        }


        // ========================================================
        // ========================================================
        // ========================================================


        public float[] GetTimes() { return _times; }


        /// <summary>
        /// Initialize some stuff for the audio set
        /// </summary>
        private void Init() {
            _times = new float[set.clips.Length];
            for ( int i = 0; i < set.clips.Length; i++ ) {
                _times[i] = set.clips[i].startTime;
            }
        }


        /// <summary>
        /// Start the system
        /// </summary>
        public void Play() {
            if ( set )
                Init();

            _canPlay = true;
        }


        /// <summary>
        /// Stop the system none baldly
        /// </summary>
        public void Stop() { _canPlay = false; }

    }
}