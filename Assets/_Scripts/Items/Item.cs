using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Items {
    /// <summary>
    ///
    /// Base Abstract class for items.
    ///
    /// </summary>
    public abstract class Item : MonoBehaviour {

        protected GameObject owner;


        /// <summary>
        /// What is called to use the item
        /// </summary>
        /// <param name="direction"></param>
        public void Use(Vector3 direction) {
            if(CanUse())
                OnUse(direction);
        }

        /// <summary>
        /// The actual usage of the item.
        /// Should only be called by Use() method
        /// </summary>
        /// <param name="direction"></param>
        protected abstract void OnUse(Vector3 direction);

        public abstract bool CanUse();

        public abstract void PlayPickupAudio();

        public void SetOwner(GameObject newOwner) { owner = newOwner; }

        public GameObject GetOwner() { return owner; }

    }
}