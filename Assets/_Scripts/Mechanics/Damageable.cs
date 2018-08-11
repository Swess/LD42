using System;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics {
    public class Damageable : MonoBehaviour {

        [Serializable]
        public class HealthEvent : UnityEvent<Damageable> { }

        [Serializable]
        public class DamageEvent : UnityEvent<Damager, Damageable> { }

        [Serializable]
        public class HealEvent : UnityEvent<float, Damageable> { }

        public float maxHealth               = 5f;
        public float startingHealth          = 5f;

        [Tooltip("Can it receives knockbacks")]
        public bool  respondToDamageImpulse  = true;
        public bool  invulnerableAfterDamage = true;
        public float invulnerabilityDuration = 3f;
        public bool  disableOnDeath          = false;

        [Tooltip("An offset from the object position used to set from where the distance to the damager is computed")]
        public Vector2 centreOffset = new Vector2(0f, 1f);

        public HealthEvent onHealthSet;
        public DamageEvent onTakeDamage;
        public DamageEvent onDie;
        public HealEvent   onGainHealth;

        protected bool    invulnerable;
        protected float   inulnerabilityTimer;
        protected float   currentHealth;
        protected Vector2 damageDirection;

        public float CurrentHealth {
            get { return currentHealth; }
        }

        // ========================================================
        // ========================================================
        // ========================================================


        void OnEnable() {
            currentHealth = startingHealth;

            onHealthSet.Invoke(this);

            DisableInvulnerability();
        }


        void Update() {
            if ( invulnerable ) {
                inulnerabilityTimer -= Time.deltaTime;
                if ( inulnerabilityTimer <= 0f ) invulnerable = false;
            }
        }


        // ========================================================
        // ========================================================
        // ========================================================


        public void EnableInvulnerability(bool ignoreTimer = false) {
            invulnerable = true;

            // technically don't ignore timer, just set it to an insanly big number. Allow to avoid to add more test & special case.
            inulnerabilityTimer = ignoreTimer ? float.MaxValue : invulnerabilityDuration;
        }


        public void DisableInvulnerability() { invulnerable = false; }

        public Vector2 GetDamageDirection() { return damageDirection; }


        /// <summary>
        /// Called when an damager hit a damageable.
        /// Reduce health an call all TakeDamage events.
        /// </summary>
        /// <param name="damager"></param>
        /// <param name="ignoreInvincible"></param>
        public void TakeDamage(Damager damager, bool ignoreInvincible = false) {
            if ( (invulnerable && !ignoreInvincible) || currentHealth <= 0 ) return;

            // we can reach that point if the damager was one that was ignoring invincible state.
            // We still want the callback that we were hit, but not the damage to be removed from health.
            if ( !invulnerable ) {
                currentHealth -= damager.damage;
                onHealthSet.Invoke(this);
                if ( invulnerableAfterDamage )
                    EnableInvulnerability();
            }

            damageDirection = transform.position + (Vector3) centreOffset - damager.transform.position;

            onTakeDamage.Invoke(damager, this);

            if ( currentHealth <= 0 ) {
                onDie.Invoke(damager, this);
                EnableInvulnerability();
                if ( disableOnDeath ) gameObject.SetActive(false);
            }
        }


        /// <summary>
        /// Gain health by the given amount
        /// </summary>
        /// <param name="amount"></param>
        public void GainHealth(float amount) {
            currentHealth += amount;

            if ( currentHealth > maxHealth ) currentHealth = maxHealth;

            onHealthSet.Invoke(this);

            onGainHealth.Invoke(amount, this);
        }


        /// <summary>
        /// Set the health to the given amount
        /// </summary>
        /// <param name="amount"></param>
        public void SetHealth(float amount) {
            currentHealth = amount;

            onHealthSet.Invoke(this);
        }

    }
}