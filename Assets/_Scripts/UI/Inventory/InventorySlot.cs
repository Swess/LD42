using Inventory;
using UnityEngine;
using UnityEngine.UI;

namespace UI {
    public class InventorySlot : MonoBehaviour {

        public Color      activeTint;
        public Color      inactiveTint;
        public Sprite     emptySprite;
        public GameObject childSpriteHolder;

        private QuickInventory _inventory;
        private int            _index;
        private bool           _active;


        // ========================================================
        // ========================================================
        // ========================================================


        /// <summary>
        /// Update visual state of the element
        /// Called by broadcast
        /// </summary>
        public void UpdateState() {
            CheckActive();
            Image img = childSpriteHolder.GetComponent<Image>();

            if (_inventory.inventory[_index]) { // Check if it's empty
                img.sprite = _inventory.inventory[_index].sprite;
                img.color = _active ? activeTint : inactiveTint;
            } else {
                img.sprite = emptySprite;
                img.color  = inactiveTint;
            }
        }


        /// <summary>
        /// Set the inventory index to display in this slot
        /// </summary>
        /// <param name="index"></param>
        public void SetIndex(int index) { _index = index; }

        public void SetInventoryReference(QuickInventory inventory) { _inventory = inventory; }


        /// <summary>
        /// Check if the tracking item is the current one
        /// </summary>
        public void CheckActive() {
            _active = _inventory.GetCurrentPosition() == _index;
        }

    }
}