using System.Collections;
using Entities;
using Mechanics;
using UnityEngine;
using UnityEngine.Events;

namespace Items.Weapons {
    [RequireComponent(typeof(Damager))]
    [RequireComponent(typeof(Animator))]
    public class MeleeWeapon : Weapon {

        public  float    attackKnockback = 50f;
        private Damager  _damager;
        private Animator _animator;
        private bool     _isUsing = false;

        [HideInInspector] public UnityEvent onMeleeAttackBegin;
        [HideInInspector] public UnityEvent onMeleeAttackDone;

        // ========================================================
        // ========================================================


        protected void Awake() {
            _damager  = GetComponent<Damager>();
            _animator = GetComponent<Animator>();
        }


        protected override void Start() {
            base.Start();
            _damager.damage = damage; // Set the melee damage with the weapon damage
            _damager.SetEntity(GetComponentInParent<EntitiesController>());
        }


        // ========================================================
        // ========================================================
        // ========================================================


        protected override void OnUse(Vector3 dir) {
            base.OnUse(dir);

            _isUsing = true;
            _damager.EnableDamage();
            onMeleeAttackBegin.Invoke();
            _animator.SetTrigger("Attack");

            // TODO : Trigger animation in the given direction
        }


        /// <summary>
        /// End the attack itself (not the animation)
        /// </summary>
        protected void EndMeleeAttack() {
            _isUsing = false;
            onMeleeAttackDone.Invoke();
            _damager.DisableDamage();
        }


        public override bool CanUse() { return base.CanUse() && !_isUsing; }


        /// <summary>
        /// Apply knockback if possible to damaged entity
        /// </summary>
        public void ApplyKnockback(Damager damager, Damageable otherDamageable) {
            Vector2 dir = otherDamageable.GetDamageDirection();
            // Modify y component to make small jumping effect on impulse
            dir.y += 1;
            dir.Normalize();

            Rigidbody2D rb = otherDamageable.gameObject.GetComponent<Rigidbody2D>();

            if ( rb && otherDamageable.respondToDamageImpulse ) {
                rb.AddForce(dir * 10 * GetAttackImpulseForce());
            }
        }


        /// <summary>
        /// Returns the current attack impulse knockback
        /// </summary>
        /// <returns></returns>
        private float GetAttackImpulseForce() {
            // TODO: Add variants/modifier effects here
            return attackKnockback;
        }

    }
}