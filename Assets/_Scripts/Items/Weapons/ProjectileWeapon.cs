using Rewired.ComponentControls.Effects;
using UnityEngine;

namespace Items.Weapons {
    public class ProjectileWeapon : Weapon {

        public GameObject projectile;
        public float projectileSpeed;

        /// <summary>
        /// Default weapon's 'Usage' Method
        /// </summary>
        /// <param name="dir"></param>
        protected override void OnUse(Vector3 dir) {
            base.OnUse(dir);

            GameObject obj = Instantiate(projectile, transform.position, Quaternion.identity);
            Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();

            objRb.velocity = owner.GetComponent<Rigidbody2D>().velocity;
            objRb.AddForce( dir * projectileSpeed, ForceMode2D.Impulse );

            // Rotation randomly
            objRb.angularVelocity = Random.Range(-500, 500);

            // Use once Only
            Destroy(gameObject);
        }

    }
}