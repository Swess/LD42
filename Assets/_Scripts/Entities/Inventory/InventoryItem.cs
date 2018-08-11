using UnityEngine;

namespace Inventory {
	[CreateAssetMenu(order = 30)]
	public class InventoryItem : ScriptableObject {

		[Range(0,100)] public int weight;
		public Sprite sprite;
		public GameObject gameObject;

	}// Class
}
