using Core;
using Mechanics;
using UnityEngine;
using Rewired;

namespace Entities.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : EntitiesController {

        [PlayerIdProperty(typeof(RewiredConsts.Player))]
        public int player;

        public                float accelerationSpeed = 10f;
        public                float maxVelocity       = 10f;
        [Range(0, 20)] public float frictionFactor    = 4f;


        private Animator    _animator;
        private Rigidbody2D _rb;
        private Damageable  _damageable;

        public Rewired.Player PlayerInputs { get; protected set; }

        // ========================================================
        // ========================================================


        protected void Awake() {
            _animator   = GetComponent<Animator>();
            _rb         = GetComponent<Rigidbody2D>();
            _damageable = GetComponent<Damageable>();

            PlayerInputs = ReInput.players.GetPlayer(player); // Get the MainPlayer's inputs
        }


        private void Update() {
            SlowDownPlayer();
            CheckForMovement();
        }


        // ========================================================
        // ========================================================
        // ========================================================


        private void SlowDownPlayer() {
            Vector2 opposite = -_rb.velocity;
            _rb.AddForce(opposite * frictionFactor);
        }


        private void CheckForMovement() {
            // Check here
            Vector2 forceAxis = new Vector2(PlayerInputs.GetAxisRaw("Horizontal"), PlayerInputs.GetAxisRaw("Vertical"));

            _rb.AddForce(forceAxis * accelerationSpeed);

            if ( _rb.velocity.magnitude > maxVelocity ) {
                _rb.velocity = _rb.velocity.normalized * maxVelocity;
            }
        }


        /// <summary>
        ///
        /// </summary>
        /// <param name="damager"></param>
        /// <param name="damageable"></param>
        public void OnDie(Damager damager, Damageable damageable) {
            GameController.Instance.actionsMapsHelper.DisableMap("Gameplay");

            // Trigger animation here

            // After animation :
            GameController.Instance.SceneController.FadeAndLoadScene("MainMenu");
        }

        public Damageable GetDamageable() { return _damageable; }

    }
}