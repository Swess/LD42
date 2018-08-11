using System;
using UnityEngine;

public class AutoTileTexture : MonoBehaviour {

	public bool tileXAxis = true;
	public bool tileYAxis = true;

	void OnDrawGizmos() {
		Renderer renderer = GetComponent<Renderer>();

		float xScale = 1;
		float yScale = 1;

		if (tileXAxis)
			xScale = gameObject.transform.lossyScale.x - 0.001f;		// Minus decimals prevents loop pixel glitch

		if (tileYAxis)
			yScale = gameObject.transform.lossyScale.y - 0.001f;		// Minus decimals prevents loop pixel glitch


		// Set
		renderer.sharedMaterial.SetTextureScale("_MainTex",new Vector2(xScale,yScale));
		CleanName(renderer);
	}

	///
	/// Remove the traillings "(Instance)" strings added when dynamicly editing the meterial
	///
	void CleanName(Renderer renderer) {
		String name = renderer.sharedMaterial.name;
		if (name.EndsWith("(Instance)")) {
			renderer.sharedMaterial.name = name.Substring(0, name.Length-11);

			CleanName(renderer);
		}
		return;
	}
}