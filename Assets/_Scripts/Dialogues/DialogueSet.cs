using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Entities {
	/// <summary>
	/// Provide a way to structure and store the dialogues.
	/// </summary>
	[CreateAssetMenu(menuName = "Gameplay/Dialogue Set", order = 150)]
	public class DialogueSet : ScriptableObject {

		private int _counter = 0;

		[TextArea]
		public string[] dialogues;


		/// <summary>
		/// Increment the counter and get the next string in the set if present
		/// </summary>
		/// <returns>Null if there is no next string</returns>
		public string GetNext() {
			if (dialogues.Length >= ++_counter)
				return dialogues[_counter - 1];

			return null;
		}


		public void Reset() { _counter = 0; }


	}
}
