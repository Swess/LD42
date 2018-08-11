using Core;
using UnityEngine;
using System;
using Entities.Player;
using UnityEngine.Events;

namespace Inventory {
    /// <summary>
    ///
    /// QuickInventory
    /// Used for the quick inventory of the players
    ///
    /// </summary>
    public class QuickInventory : MonoBehaviour {

        public const int INVENTORY_SIZE = 1; // The number of items the inventory can hold

        [Header("Inventory is fixed size at 1 slots.")]
        public InventoryItem[] inventory;

        [HideInInspector] public UnityEvent onChange; // On any inventory changes

        private int              _currentActivePosition; // The user current active position in the inventory
        private Rewired.Player   _inputs;                // Reference to the the player inputs from Rewired
        private PlayerItemHolder _holder;


        // =================


        private void Awake() { _holder = GetComponentInChildren<PlayerItemHolder>(); }


        private void Start() {
            // Get reference on start to make sure that it exist in PlayerController
            _inputs = GetComponent<PlayerController>().PlayerInputs; // Get the MainPlayer's inputs


            // Setup default Inventory
            InventoryItem[] tempInv = inventory;
            inventory = new InventoryItem[INVENTORY_SIZE];

            // Placing item in new inventory
            for ( int i = 0; i < inventory.Length && i < tempInv.Length; i++ ) {
                inventory[i] = tempInv[i];
            }

            // Start with first item
            _currentActivePosition = 0;
            _holder.ChangeSelectedItem(_currentActivePosition);
        }


        private void Update() {
            // Equip item
            for ( int i = 1; i < INVENTORY_SIZE+1; i++ ) {
                if ( _inputs.GetButtonDown("InvItem" + i) ) {
                    _currentActivePosition = i - 1;
                    _holder.ChangeSelectedItem(_currentActivePosition);
                    onChange.Invoke();
                }
            }

            if ( _inputs.GetButtonDown("NextItem") ) {
                NextItem();
            }
        }


        // ========================================================
        // ========================================================
        // ========================================================


        ///
        /// GETTERS & SETTERS
        ///
        public int GetCurrentPosition() { return _currentActivePosition; }


        public void SetCurrentPosition(int index) {
            _currentActivePosition = index;
            _holder.ChangeSelectedItem(_currentActivePosition);
            onChange.Invoke();
        }

        /* ------------------ */


        /// <summary>
        /// Return current InventoryItem
        /// </summary>
        /// <returns></returns>
        public InventoryItem GetCurrentItem() {
            // TODO: return instance in holder ! Not the array reference
            return inventory[_currentActivePosition];
        }


        /// <summary>
        /// Replace the item in the current position
        /// </summary>
        /// <param name="itemToAdd"></param>
        public void AddItem(InventoryItem itemToAdd) {
            for ( int i = 0; i < INVENTORY_SIZE; i++ ) {
                if ( inventory[i] == null ) {
                    inventory[i] = itemToAdd;
                    onChange.Invoke();
                    return;
                }
            }

            // If inventory full, replace current
            ReplaceCurrentItem(itemToAdd);
        }


        public void NextItem() {
            if ( _currentActivePosition + 2 > INVENTORY_SIZE || !HasItemAt(_currentActivePosition+1) ) {        // Out of range
                Debug.Log("Inventory index outOfRange or no item at index");
                return;

                // TODO : Add action not possible sound
            }

            _currentActivePosition += 1;
            _holder.ChangeSelectedItem(_currentActivePosition);
            onChange.Invoke();
        }


        /// <summary>
        /// Replace the item in the current position
        /// </summary>
        /// <param name="itemToAdd"></param>
        public void ReplaceCurrentItem(InventoryItem itemToAdd) {
            RemoveItem(_currentActivePosition);
            inventory[_currentActivePosition] = itemToAdd;
            _holder.ChangeSelectedItem(_currentActivePosition);
            onChange.Invoke();
        }


        /// <summary>
        /// Remove the item in the specified position
        /// </summary>
        /// <param name="pos"></param>
        public void RemoveItem(int pos) {
            inventory[pos] = null;
            onChange.Invoke();
        }


        /// <summary>
        /// Get total item's weight
        /// </summary>
        /// <returns></returns>
        public int GetTotalWeight() {
            int total = 0;
            for ( int i = 0; i < INVENTORY_SIZE; i++ ) {
                if ( inventory[i] == null ) continue;

                total += inventory[i].weight;
            }

            return total;
        }


        /// <summary>
        /// Check whenever there is an item at the index
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        public bool HasItemAt(int index) { return inventory[index] != null; }

    }
}