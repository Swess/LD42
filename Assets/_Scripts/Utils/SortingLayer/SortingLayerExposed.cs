using UnityEngine;

namespace Utils {
    // Component does nothing; editor script does all the magic
    [AddComponentMenu("Utils/Sorting Layer Exposed")]
    public class SortingLayerExposed : MonoBehaviour {

        private void OnEnable() {
            Renderer rend = GetComponent<Renderer>();
            Debug.Log(rend.sortingLayerName);
        }

    }
}