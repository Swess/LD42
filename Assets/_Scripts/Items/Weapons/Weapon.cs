using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Items.Weapons {
    [RequireComponent(typeof(AudioSource))]
    public abstract class Weapon : Item {

        public int damage;

        [Tooltip("After the attack is performed, delay for cooldown before next attack.")]
        public float cooldownTime;

        public AudioClip usageSound;
        public AudioClip pickupSound;

        private bool _activated = false; // Is the initial delay passed

        protected bool        inCooldown = false;
        private   AudioSource _audioSource;
        private   float       _cooldownTimer;
        private   float       _activationCooldown = 0.05f; // Activation delay

        [HideInInspector] public UnityEvent onCooldownBegin;
        [HideInInspector] public UnityEvent onCooldownDone;

        // ========================================================
        // ========================================================


        protected virtual void Start() {
            _audioSource = GetComponent<AudioSource>();
            PlayPickupAudio();
            StartCoroutine(ActivationDelay());
        }


        protected virtual void Update() {
            if ( !_activated ) return;

            _cooldownTimer += Time.deltaTime;
            if ( _cooldownTimer > cooldownTime ) {
                inCooldown = false;
                onCooldownDone.Invoke();
            }
        }


        // ========================================================
        // ========================================================
        // ========================================================


        /// <summary>
        /// Default weapon's 'Usage' Method
        /// </summary>
        /// <param name="dir"></param>
        protected override void OnUse(Vector3 dir) {
            if ( usageSound )
                _audioSource.PlayOneShot(usageSound);

            inCooldown     = true;
            onCooldownBegin.Invoke();
            _cooldownTimer = 0;
        }


        /// <summary>
        /// Defines if the weapon can be used
        /// </summary>
        /// <returns></returns>
        public override bool CanUse() { return !inCooldown && _activated; }


        /// @Override
        public override void PlayPickupAudio() {
            if ( pickupSound ) {
                _audioSource.PlayOneShot(pickupSound);
            }
        }


        /// <summary>
        /// Counter started at creation to delay usage of weapons.
        /// Activate the weapon so it can be used.
        /// </summary>
        /// <returns></returns>
        IEnumerator ActivationDelay() {
            while ( true ) {
                yield return new WaitForSeconds(_activationCooldown);

                _activated = true;
                break;
            }
        }

    } // Class
}