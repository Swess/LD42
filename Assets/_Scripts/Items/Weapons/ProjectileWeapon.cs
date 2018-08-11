using UnityEngine;

namespace Items.Weapons {
    public class ProjectileWeapon : Weapon {



        /// <summary>
        /// Default weapon's 'Usage' Method
        /// </summary>
        /// <param name="dir"></param>
        protected override void OnUse(Vector3 dir) {
            base.OnUse(dir);



            // Use once Only
            // Destroy(gameObject);
        }

    }
}