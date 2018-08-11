using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class FireLogic : MonoBehaviour {

    [HideInInspector] public Tilemap    parentTilemap;
    [HideInInspector] public Vector3Int tilemapPos;

    public FireTile spreadTile;
    public float    spreadAfter = 1f;


    void Start() {
        StartCoroutine(Spread());

        tilemapPos = new Vector3Int(
            Mathf.FloorToInt(transform.position.x),
            Mathf.FloorToInt(transform.position.y),
            Mathf.FloorToInt(transform.position.z)
        );
    }


    private IEnumerator Spread() {
        yield return new WaitForSeconds(spreadAfter);

        if ( parentTilemap ) {
            // Spreading
            SpreadAt(tilemapPos + new Vector3Int(1,  0,  0)); // Right
            SpreadAt(tilemapPos + new Vector3Int(-1, 0,  0)); // Left
            SpreadAt(tilemapPos + new Vector3Int(0,  1,  0)); // Up
            SpreadAt(tilemapPos + new Vector3Int(0,  -1, 0)); // Down

            SpreadAt(tilemapPos + new Vector3Int(1,  1,  0)); // top-Right
            SpreadAt(tilemapPos + new Vector3Int(-1, 1,  0)); // Top-Left
            SpreadAt(tilemapPos + new Vector3Int(-1, -1, 0)); // Bottom-left
            SpreadAt(tilemapPos + new Vector3Int(1,  -1, 0)); // Bottom-Right
        }

    }


    void SpreadAt(Vector3Int pos) {
        TileBase tile = parentTilemap.GetTile(pos);
        if ( !tile ) {
            parentTilemap.SetTile(pos, spreadTile);
        }
    }

}