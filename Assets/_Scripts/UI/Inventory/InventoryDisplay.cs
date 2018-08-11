using Core;
using Inventory;
using UnityEngine;
using UnityEngine.Events;

namespace UI {
    public class InventoryDisplay : MonoBehaviour {

        public GameObject slotTemplate; // Instance of the slot prefab

        private QuickInventory _inventory;
        private int            _invSize = QuickInventory.INVENTORY_SIZE;
        private GameObject[]   _slots   = new GameObject[QuickInventory.INVENTORY_SIZE];


        private void Awake() { _inventory = GameController.Instance.Player.GetComponent<QuickInventory>(); }


        private void Start() {
            CreateSlots();
            _inventory.onChange.AddListener(UpdateSlots);
            UpdateSlots(); // Update at first frame also
        }


        // ========================================================
        // ========================================================
        // ========================================================


        private void UpdateSlots() { transform.BroadcastMessage("UpdateState"); }


        /// <summary>
        /// Create all necessary UI slot for the QuickInventory
        /// </summary>
        private void CreateSlots() {
            for ( int i = 0; i < _invSize; i++ ) {
                _slots[i] = Instantiate(slotTemplate, Vector3.zero, Quaternion.identity, transform);

                InventorySlot script = _slots[i].GetComponent<InventorySlot>();
                script.SetInventoryReference(_inventory);
                script.SetIndex(i);
            }
        }

    }
}