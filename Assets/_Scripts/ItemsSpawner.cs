using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ItemsSpawner : MonoBehaviour {

    public GameObject[] itemsToSpawn;
    public float        spawnTime = 2;
    public Tilemap      fireTilemap;

    public int width  = 50;
    public int height = 50;


    public void Start() { StartCoroutine(SpawnItems()); }


    private IEnumerator SpawnItems() {
        while ( true ) {
            yield return new WaitForSeconds(spawnTime);

            Vector2 pos = GetRandomPos();
            SpawnOne(pos);
        }
    }


    private void SpawnOne(Vector2 pos) {
        if ( itemsToSpawn.Length == 0 )
            return;

        Instantiate(itemsToSpawn[Random.Range(0, itemsToSpawn.Length)], pos, Quaternion.identity);
    }


    private Vector2 GetRandomPos() {
        Vector2 pos = new Vector2(Mathf.FloorToInt(transform.position.x + Random.Range(-width / 2,  width / 2)),
                                  Mathf.FloorToInt(transform.position.y + Random.Range(-height / 2, height / 2)));

        Vector3Int intPos = new Vector3Int( (int)pos.x, (int)pos.y, 0 );


        int counter = 0;
        while ( counter < ((width*height)-5) && fireTilemap && fireTilemap.GetTile(intPos) ) {
            pos = new Vector2(Mathf.FloorToInt(transform.position.x + Random.Range(-width / 2,  width / 2)),
                                            Mathf.FloorToInt(transform.position.y + Random.Range(-height / 2, height / 2)));

            intPos = new Vector3Int( (int)pos.x, (int)pos.y, 0 );
            counter++;
        }

        return pos;
    }


    private void OnDrawGizmos() { Gizmos.DrawWireCube(transform.position, new Vector3(width, height, 1)); }

}