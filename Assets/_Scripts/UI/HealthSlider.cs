using Core;
using Entities.Player;
using Mechanics;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    [RequireComponent(typeof(Slider))]
    public class HealthSlider : MonoBehaviour {

        private GameController   _core;
        private Slider           _slider;
        private Damageable       _damageable;


        private void Awake() { GameController.Instance.SceneController.AfterSceneLoad += OnSceneLoad; }

        private void Start() {
            _core   = GameController.Instance;
            _slider = GetComponent<Slider>();
            PlayerController player = _core.Player.gameObject.GetComponent<PlayerController>();
            _damageable = player.GetDamageable();

            Init();
        }


        /// <summary>
        /// When a new scene is loaded.
        /// Makes sure the slider update itself and refreshes references
        /// </summary>
        public void OnSceneLoad() {
            Start();
        }


        // Init
        private void Init() {
            _slider.maxValue = _damageable.maxHealth;
            _slider.minValue = 0f;
            _slider.value    = _damageable.CurrentHealth;

            _damageable.onTakeDamage.AddListener(UpdateHealth);

            _slider.value = _damageable.CurrentHealth;
        }


        public void UpdateHealth(Damager damager, Damageable otherDamageable) {
            _slider.value = _damageable.CurrentHealth;
        }

    }
}