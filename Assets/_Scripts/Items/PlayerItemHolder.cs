using System;
using UnityEngine;
using System.Collections;
using Core;
using Items;
using Inventory;
using Items.Weapons;
using Mechanics;


namespace Entities.Player {
    /// <summary>
    ///
    /// Used to select item from player's inventory, instanciate the item and use it properly in the scene.
    ///
    /// </summary>
    public class PlayerItemHolder : MonoBehaviour {

        public  GameObject     item;
        private Item           _itemScript;
        private QuickInventory _quickInventory;
        private Animator       _playerAnimator;

        protected GameController core;


        ///////
        private void Start() {
            core = GameController.Instance;

            _playerAnimator = transform.parent.GetComponent<Animator>();
            _quickInventory = transform.parent.GetComponent<QuickInventory>();

            if ( item ) {
                ChangeItem(item);
            }
        }


        // ========================================================
        // ========================================================
        // ========================================================



        /// <summary>
        /// Change/Replace the item in the item holder
        /// </summary>
        /// <param name="newItem"></param>
        public void ChangeItem(GameObject newItem) {
            if ( !newItem.GetComponent<Item>() ) {
                return;
            } // Check if the gameobject is an Item

            EmptyHolder();
            GameObject itemInst = Instantiate(newItem, Vector3.zero, Quaternion.identity, transform);

            item        = itemInst;
            _itemScript = itemInst.GetComponent<Item>();

            _itemScript.SetOwner(core.Player);

            // Place item in holder
            item.transform.position = transform.position;

            // Check if the item is a weapon
            // _playerAnimator.SetBool("HasWeapon", _itemScript.GetType().IsInstanceOfType( typeof(Weapon) ));
        }


        /// <summary>
        /// Empty the item holder of any items
        /// </summary>
        private void EmptyHolder() {
            foreach ( Transform child in transform ) {
                Destroy(child.gameObject);
            }
        }


        /// <summary>
        /// Use the current item in the holder
        /// </summary>
        /// <param name="dir"></param>
        public void UseItem(Vector2 dir) {
            if ( _itemScript != null ) {
                _itemScript.Use(dir);
            }
        }


        /// <summary>
        /// Continously place the item on the player
        /// </summary>
        private void FixedUpdate() {
            if ( item != null ) {
                item.transform.position = transform.position;
            }
        }

    } // class
}     // namespace