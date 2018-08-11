using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MeteorController : MonoBehaviour {

	private Tilemap _tilemapToSpawnOn;
	public FireTile fireTile;

	void Start() {
		_tilemapToSpawnOn = GameObject.Find("FireTilemap").GetComponent<Tilemap>();

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

		TileBase tile = _tilemapToSpawnOn.GetTile( pos );
		if ( !tile ) {
			// Spawn tile here then
			_tilemapToSpawnOn.SetTile( pos, fireTile );
		}
	}


}
