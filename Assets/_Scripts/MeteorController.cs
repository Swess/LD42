using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeteorController : MonoBehaviour {

	public Tilemap tilemapToSpawnOn;
	public FireTile fireTile;

	void Start() {
		//
		HitTheGround();
	}


	public void HitTheGround() {
		// Spawn Fire at pos
		Vector3Int pos = new Vector3Int(
				Mathf.RoundToInt(transform.position.x),
				Mathf.RoundToInt(transform.position.y),
				Mathf.RoundToInt(transform.position.z)
			);

		Debug.Log( pos );

		TileBase tile = tilemapToSpawnOn.GetTile( pos );
		if ( !tile ) {
			// Spawn tile here then
		}

		Debug.Log(tile);
	}


}
