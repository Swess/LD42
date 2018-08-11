using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using Rewired;
using UnityEngine.Events;

namespace Core {
    public class PauseScreen : MonoBehaviour {

        private          bool     _isPaused      = false;
        private readonly string[] _noPauseScenes = {"MainMenu"};

        private Player _systemInputs;

        public UnityEvent onPause;
        public UnityEvent onUnpause;

        private void Start() {
            _systemInputs = ReInput.players.GetPlayer(0);
            ChangeActiveStateTo(false);
        }


        private void Update() {
            if ( _systemInputs.GetButtonDown("Pause")
            || _systemInputs.GetButtonDown("UICancel") && _isPaused) {
                TogglePause();
            }

        }


        public bool IsPaused() { return _isPaused; }


        /// <summary>
        /// Toggle pause menu state and trigger the relative mecanism
        /// </summary>
        private void TogglePause() {
            if ( !CheckCanPause() ) return;

            _isPaused = !_isPaused;
            ChangeActiveStateTo(_isPaused);

            if ( _isPaused )
                onPause.Invoke();
            else
                onUnpause.Invoke();

            Time.timeScale = _isPaused ? 0f : 1f;
        }


        public void SetTimeScale(float scale) { Time.timeScale = scale; }


        /// <summary>
        /// Pause state setter
        /// </summary>
        /// <param name="state"></param>
        public void SetStateTo(bool state) {
            _isPaused = state;
            ChangeActiveStateTo(state);

            Time.timeScale = state ? 0f : 1f;
        }


        /// <summary>
        /// Check if the user can currently pause the game, relative to the scene
        /// </summary>
        /// <returns></returns>
        private bool CheckCanPause() {
            string name = SceneManager.GetActiveScene().name;
            return !((IList) _noPauseScenes).Contains(name);
        }


        /// <summary>
        /// Change the active state of child elements
        /// </summary>
        /// <param name="state"></param>
        private void ChangeActiveStateTo(bool state) {
            GetComponent<CanvasGroup>().alpha = state ? 1f : 0f;
            foreach ( Transform child in transform ) { child.gameObject.SetActive(state); }
        }

    }
}