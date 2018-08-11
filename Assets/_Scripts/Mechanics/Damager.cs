using System;
using Core.Types;
using Entities;
using UnityEngine;
using UnityEngine.Events;

namespace Mechanics {
    public class Damager : MonoBehaviour {

        [Serializable]
        public class DamagableEvent : UnityEvent<Damager, Damageable> { }

        [Serializable]
        public class NonDamagableEvent : UnityEvent<Damager> { }

        public float   damage = 1f;
        public Vector2 offset = new Vector2(1.5f, 1f);
        public Vector2 size   = new Vector2(2.5f, 1f);

        [Tooltip(
            "If this is set, the offset x will be changed base on the entity's facing. e.g. Allow to make the damager always forward in the direction of the entity. (needs to be a component of an entity)")]
        public bool offsetBasedOnFacing = true;

        [Tooltip("If disabled, damager ignore trigger when casting for damage")]
        public bool canHitTriggers;

        [Tooltip("Disables the damager once it hits something ? (Reenabling necessary after)")]
        public bool disableDamageAfterHit = false;

        [Tooltip("If set, the player will be forced to respawn to latest checkpoint in addition to loosing life")]
        public bool forceRespawn = false;

        [Tooltip("If set, an invincible damageable hit will still get the onHit message (but won't loose any life)")]
        public bool ignoreInvincibility = false;

        [Tooltip("Set enabled state on Start. For environmental hazards.")]
        public bool startEnabled = false;

        public bool debugDamageBox = false;

        public LayerMask         hittableLayers;
        public DamagableEvent    onDamageableHit;
        public NonDamagableEvent onNonDamageableHit;

        protected EntitiesController entity;
        protected bool               canDamage = false;
        protected ContactFilter2D    attackContactFilter;
        protected Collider2D[]       attackOverlapResults = new Collider2D[10];
        protected Collider2D         lastHit;
        protected Direction          originalFacing;


        // Kept only for displaying debug box in editor mode
        private Vector2 _pointA;
        private Vector2 _pointB;

        // call that from inside the onDamageableHIt or OnNonDamageableHit to get what was hit.
        public Collider2D LastHit {
            get { return lastHit; }
        }

        // ========================================================
        // ========================================================
        // ========================================================


        void Awake() {
            attackContactFilter.layerMask    = hittableLayers;
            attackContactFilter.useLayerMask = true;
            attackContactFilter.useTriggers  = canHitTriggers;

            if ( offsetBasedOnFacing ) {
                entity = GetComponent<EntitiesController>();
            }
        }


        void Start() {
            if ( offsetBasedOnFacing && entity != null ) {
                originalFacing = entity.Facing;
            }

            canDamage = startEnabled;
        }


        void FixedUpdate() {
            if ( !canDamage ) return;

            Vector2 scale = transform.lossyScale;

            Vector2 facingOffset = Vector2.Scale(offset, scale);
            if ( offsetBasedOnFacing && entity != null && entity.Facing != originalFacing ) {
                facingOffset = new Vector2(-offset.x * scale.x, offset.y * scale.y);
            }

            Vector2 scaledSize = Vector2.Scale(size, scale);

            _pointA = (Vector2) transform.position + facingOffset - scaledSize * 0.5f;
            _pointB = _pointA + scaledSize;

            // Debug detection box
            if ( debugDamageBox ) {
                Debug.DrawLine(_pointA, new Vector2(_pointB.x, _pointA.y), Color.green);
                Debug.DrawLine(new Vector2(_pointB.x,          _pointA.y), _pointB,                           Color.green);
                Debug.DrawLine(_pointB,                                    new Vector2(_pointA.x, _pointB.y), Color.green);
                Debug.DrawLine(new Vector2(_pointA.x,                                             _pointB.y), _pointA, Color.green);
            }

            int hitCount = Physics2D.OverlapArea(_pointA, _pointB, attackContactFilter, attackOverlapResults);

            for ( int i = 0; i < hitCount; i++ ) {
                lastHit = attackOverlapResults[i];
                Damageable damageable = lastHit.GetComponent<Damageable>();

                if ( damageable ) {
                    damageable.TakeDamage(this, ignoreInvincibility);
                    onDamageableHit.Invoke(this, damageable);
                    if ( disableDamageAfterHit ) DisableDamage();
                } else {
                    onNonDamageableHit.Invoke(this);
                }
            }
        }


        // ========================================================
        // ========================================================
        // ========================================================

        public void EnableDamage() { canDamage = true; }

        public void DisableDamage() { canDamage = false; }

        public void SetEntity(EntitiesController entity) { this.entity = entity; }


        /// <summary>
        /// Display debug hitbox in editor mode
        /// </summary>
        private void OnDrawGizmos() {
            if ( Application.isPlaying )
                return;

            Vector2 scale = transform.lossyScale;

            Vector2 facingOffset = Vector2.Scale(offset, scale);
            if ( offsetBasedOnFacing && entity != null && entity.Facing != originalFacing ) {
                facingOffset = new Vector2(-offset.x * scale.x, offset.y * scale.y);
            }

            Vector2 scaledSize = Vector2.Scale(size, scale);

            _pointA = (Vector2) transform.position + facingOffset - scaledSize * 0.5f;
            _pointB = _pointA + scaledSize;

            if ( debugDamageBox ) {
                Debug.DrawLine(_pointA, new Vector2(_pointB.x, _pointA.y), Color.green);
                Debug.DrawLine(new Vector2(_pointB.x,          _pointA.y), _pointB,                           Color.green);
                Debug.DrawLine(_pointB,                                    new Vector2(_pointA.x, _pointB.y), Color.green);
                Debug.DrawLine(new Vector2(_pointA.x,                                             _pointB.y), _pointA, Color.green);
            }
        }

    }
}