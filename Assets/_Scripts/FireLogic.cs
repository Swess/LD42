using System.Collections;
using System.Collections.Generic;
using Core;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Tilemaps;

public class FireLogic : MonoBehaviour {

    [HideInInspector] public Tilemap    parentTilemap;
    [HideInInspector] public Vector3Int tilemapPos;

    public FireTile spreadTile;
    public float    spreadAfter = 1f;

    public  float    timeSoundDelay = 1f;

    public GameObject particleObject;
    private float    _timer         = 0.99f;
    private Renderer _rend;
    private Light    _light;



    void Start() {
        _rend = GetComponentInChildren<Renderer>();
        _light = GetComponentInChildren<Light>();

        StartCoroutine(Spread());

        // Grab current tilemap position
        tilemapPos = new Vector3Int(Mathf.FloorToInt(transform.position.x),
                                    Mathf.FloorToInt(transform.position.y),
                                    Mathf.FloorToInt(transform.position.z));
    }


    void Update() {
        if ( !_light )
            return;

        _light.enabled = _rend.isVisible;

        if(particleObject)
            particleObject.SetActive(_rend.isVisible);
    }


    private IEnumerator Spread() {
        while ( true ) {
            yield return new WaitForSeconds(Random.Range(spreadAfter, spreadAfter + 1f));

            if ( parentTilemap ) {
                // Spreading
                SpreadAt(tilemapPos + new Vector3Int(1,  0,  0)); // Right
                SpreadAt(tilemapPos + new Vector3Int(-1, 0,  0)); // Left
                SpreadAt(tilemapPos + new Vector3Int(0,  1,  0)); // Up
                SpreadAt(tilemapPos + new Vector3Int(0,  -1, 0)); // Down
            }
        }
    }


    void SpreadAt(Vector3Int pos) {
        TileBase tile = parentTilemap.GetTile(pos);
        if ( !tile ) {
            parentTilemap.SetTile(pos, spreadTile);
        }
    }


    public void PlayOnce() {
        if ( _timer > timeSoundDelay ) {
            GetComponent<AudioSource>().Play();
            _timer = 0;
        }

        _timer += Time.deltaTime;
    }


    public void AddScore() {
        GameController.Instance.score += 1;
        GameController.Instance.onScoring.Invoke();
    }

}