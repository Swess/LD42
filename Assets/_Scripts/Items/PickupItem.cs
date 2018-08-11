using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;

namespace Inventory {
	public class PickupItem : MonoBehaviour {
		
		public enum PickupReaction {
			Ammo, Health
		}
		public PickupReaction whatToPickup = PickupReaction.Ammo;
		public int ammount;

		// [SerializeField] [HideInInspector]
		//AmmoType _ammoType;

		// ==========

		///
		/// On Collision with the player only
		///
		private void OnCollisionEnter(Collision other) {
			if (!other.gameObject.CompareTag("Player")) return;

			if (whatToPickup == PickupReaction.Ammo) {
				//PickupAmmo(other);
			} else {
				// PickupHealth(other);
			}

			PlaySound();
			Destroy(gameObject);
		}


		// Splitting logic for organization
//		private void PickupAmmo(Collision other) {
//			Inventory inventory = other.transform.GetComponent<Inventory>();
//			if (inventory != null) {
//				/Ammo ammoStockage = inventory.GetAmmoByType(_ammoType);
//				ammoStockage.AddAmmo(ammount);
//			}
//		}

//		private void PickupHealth(Collision other) {
//			PlayerController playerScript = other.transform.GetComponent<PlayerController>();
//			playerScript.Heal( ammount );
//		}



		private void PlaySound() {
			// TODO : Play sound on pickup
		}



	}
}
