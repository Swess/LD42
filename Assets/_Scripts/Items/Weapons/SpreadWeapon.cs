using System.Collections;
using UnityEngine;

namespace Items.Weapons {
    public class SpreadWeapon : Weapon {

        public GameObject projectile;
        public float projectileSpeed;

        public float spreadTime = 2.5f;

        private float _currentSpreadTime = 0f;
        private bool _inUse = false;

        /// <summary>
        /// Default weapon's 'Usage' Method
        /// </summary>
        /// <param name="dir"></param>
        protected override void OnUse(Vector3 dir) {
            if ( _inUse )
                return;

            base.OnUse(dir);

            StartCoroutine(Spread(dir));
            _inUse = true;
        }


        private IEnumerator Spread(Vector3 firstDir) {
            Vector3 dir = firstDir;

            while ( _currentSpreadTime < spreadTime ) {
                GameObject  obj   = Instantiate(projectile, transform.position, Quaternion.identity);
                Rigidbody2D objRb = obj.GetComponent<Rigidbody2D>();

                // Recompute direction
                if( owner.GetComponent<Rigidbody2D>().velocity.normalized.magnitude > 0 )
                    dir = owner.GetComponent<Rigidbody2D>().velocity.normalized;

                objRb.velocity = owner.GetComponent<Rigidbody2D>().velocity;
                objRb.AddForce( dir * projectileSpeed, ForceMode2D.Impulse );

                // Rotation randomly
                objRb.angularVelocity = Random.Range(-1000, 1000);

                _currentSpreadTime += Time.deltaTime;
                yield return true;
            }

            Destroy(gameObject);
        }

    }
}