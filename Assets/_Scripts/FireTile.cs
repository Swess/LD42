using UnityEngine;
using UnityEngine.Tilemaps;


[CreateAssetMenu]
public class FireTile : TileBase {

    public Sprite     sprite; //The sprite of tile in a palette and in a scene
    public GameObject prefab; //The gameobject to spawn


    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData) {
        // Assign variables
        if ( !Application.isPlaying ) tileData.sprite = sprite;
        else tileData.sprite                          = null;

        tileData.sprite = null; // Fuck that

        if ( prefab )
            tileData.gameObject = prefab;

    }


    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go) {
        // Streangly the position of gameobject starts at Left Bottom point of cell and not at it center
        if ( go ) {
            go.transform.position += Vector3.up * 0.5f + Vector3.right * 0.5f;

            FireLogic script = prefab.GetComponent<FireLogic>();
            Tilemap tm = tilemap.GetComponent<Tilemap>();
            script.parentTilemap = tm;
        }

        return base.StartUp(position, tilemap, go);
    }


    public override bool GetTileAnimationData(Vector3Int location, ITilemap tileMap, ref TileAnimationData tileAnimationData) {
        // Make sprite of tile invisiable
        tileAnimationData.animatedSprites    = new Sprite[] {null};
        tileAnimationData.animationSpeed     = 0;
        tileAnimationData.animationStartTime = 0;
        return true;
    }

}