using System;
using Core.Inputs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Core {
    public class GameController : MonoBehaviour {

        public ContextualInputsState menuInputsContext;
        public ContextualInputsState gameplayInputsContext;

        private static GameController _instance; // Singleton instance

        public static GameController Instance {
            get { return _instance; }
        }


        private bool _testingMode = false; // Define if we are testing a scene alone or using the SceneController Loader

        /// References
        private GameObject _player1;

        private GameObject _player2;

        [SerializeField] private SceneController _sceneController;
        [SerializeField] private UIController    _uiController;
        [SerializeField] private PauseScreen     _pauseScreen;


        // ====================================
        // ====================================

        /// <summary>
        /// Accessors for different important Components
        /// </summary>

        public GameObject Player {
            get {
                if ( _player1 == null ) _player1 = GameObject.Find("Player");
                return _player1;
            }
        }

        public SceneController SceneController {
            get { return _sceneController; }
        }

        public UIController UiController {
            get { return _uiController; }
        }

        public ActionsMapsHelper actionsMapsHelper { get; protected set; }

        // ====================================
        // ====================================


        /// <summary>
        /// Brute constructor.
        /// Called even before Awake() calls.
        /// </summary>
        public GameController() {
            // Setup Singleton
            if ( _instance == null )
                _instance = this;
            else if ( _instance != this ) Destroy(gameObject);
        }


        private void Awake() {
            // Define if we are testing a scene
            if ( SceneManager.GetActiveScene().name != "Persistent" ) {
                _testingMode = true;
            }

            actionsMapsHelper = new ActionsMapsHelper();

            _pauseScreen.onPause.AddListener(EnableMenuInputContext);
            _pauseScreen.onUnpause.AddListener(EnableGameInputContext);
        }


        // ========================================================
        // ========================================================
        // ========================================================


        public void EnableMenuInputContext() {
            actionsMapsHelper.ApplyContext(menuInputsContext);
        }


        public void EnableGameInputContext() {
            actionsMapsHelper.ApplyContext(gameplayInputsContext);
        }


        public bool IsTesting() { return _testingMode; }


        /// <summary>
        /// Quit the entire application (Only in builds)
        /// </summary>
        public void KillGame() { Application.Quit(); }

    } // Class
}