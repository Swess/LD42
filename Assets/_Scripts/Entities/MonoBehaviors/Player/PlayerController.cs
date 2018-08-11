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

        public AudioSource walkingAudioSource;

        private Animator         _animator;
        private Rigidbody2D      _rb;
        private Damageable       _damageable;
        private PlayerItemHolder _holder;

        private Vector3 _previousDirection;

        public Rewired.Player PlayerInputs { get; protected set; }

        // ========================================================
        // ========================================================


        protected void Awake() {
            _animator   = GetComponent<Animator>();
            _rb         = GetComponent<Rigidbody2D>();
            _damageable = GetComponent<Damageable>();
            _holder     = GetComponentInChildren<PlayerItemHolder>();

            PlayerInputs = ReInput.players.GetPlayer(player); // Get the MainPlayer's inputs
        }


        private void Update() {
            SlowDownPlayer();
            CheckForMovement();
            CheckForUseItem();
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

            walkingAudioSource.volume = forceAxis.magnitude > 0 ? 1f : 0f;

            _rb.AddForce(forceAxis * accelerationSpeed);

            // Change Rotation
            if ( forceAxis.magnitude > 0 )
                _previousDirection = forceAxis;

            float angle = Mathf.Atan2(_previousDirection.x, _previousDirection.y) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, new Vector3(0, 0, -1));

            if ( _rb.velocity.magnitude > maxVelocity ) {
                _rb.velocity = _rb.velocity.normalized * maxVelocity;
            }
        }


        private void CheckForUseItem() {
            if ( PlayerInputs.GetButtonDown("UseItem") ) {
                _holder.UseItem(GetDirection());
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


        public Vector3 GetDirection() { return _rb.velocity.normalized; }

    }
}