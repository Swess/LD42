using System.Collections;
using System.Collections.Generic;
using Core;
using Entities.Player;
using Items.Weapons;
using UnityEngine;

namespace Inventory {
	public class PickupItem : MonoBehaviour {
		

		public InventoryItem whatToPickup;

		// ==========

		///
		/// On Collision with the player only
		///
		private void OnCollisionEnter2D(Collision2D other) {
			if (!other.gameObject.CompareTag("Player") || !whatToPickup) return;

			// Weapon weaponScript = whatToPickup.gameObject.GetComponent<Weapon>();

			// Play sound (Played by holder)
			// weaponScript.PlayPickupAudio();

			// Send to item holder
			PlayerItemHolder holder = GameController.Instance.Player.GetComponentInChildren<PlayerItemHolder>();
			holder.ChangeItem( whatToPickup.gameObject );

			Destroy(gameObject);
		}




	}
}
