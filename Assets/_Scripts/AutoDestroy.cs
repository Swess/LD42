using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour {

    public  float destroyAfterNSeconds = 1f;
    private float _currentTimer        = 0f;


    // Update is called once per frame
    void Update() {
        _currentTimer += Time.deltaTime;

        if (_currentTimer > destroyAfterNSeconds) {
            Destroy(gameObject);
        }
    }

}