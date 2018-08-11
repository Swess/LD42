using UnityEngine;
using Rewired;

namespace Entities.Player {
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : EntitiesController {

        [PlayerIdProperty(typeof(RewiredConsts.Player))]
        public int player;

        public float health            = 100f;
        public float accelerationSpeed = 10f;
        public float maxVelocity       = 10f;


        private Animator    _animator;
        private Rigidbody2D _rb;

        public Rewired.Player PlayerInputs { get; protected set; }

        // ========================================================
        // ========================================================


        protected void Awake() {
            _animator = GetComponent<Animator>();
            _rb       = GetComponent<Rigidbody2D>();

            PlayerInputs = ReInput.players.GetPlayer(player); // Get the MainPlayer's inputs
        }


        private void Update() { CheckForMovement(); }


        // ========================================================
        // ========================================================
        // ========================================================


        private void CheckForMovement() {
            // Check here
            Vector2 forceAxis = new Vector2(PlayerInputs.GetAxisRaw("Horizontal"), PlayerInputs.GetAxisRaw("Vertical"));

            _rb.AddForce(forceAxis * accelerationSpeed);

            if ( _rb.velocity.magnitude > maxVelocity ) {
                _rb.velocity = _rb.velocity.normalized * maxVelocity;
            }
        }

    }
}