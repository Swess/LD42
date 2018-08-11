using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras {
    /// <summary>
    /// Make the GameObject move in both axis X & Y relative to a fraction of the camera movement.
    /// The amplitude of the effect is determined by the distance of the transform from the z = 0.
    /// The paralaxSpeed = distance/100 if distance>0, and distance/2 if < 0 ;
    /// </summary>
    public class ParallaxScrolling : MonoBehaviour {

        public                 bool  inverseX;
        public                 bool  inverseY;
        [Range(0f, 2f)] public float yProportion = 0.2f;
        public                 bool  autoBlur;

        private Camera         _cam;
        private Transform      _camTransform;
        private Vector3        _lastCamPos;
        private float          _paralaxSpeed;
        private SpriteRenderer _renderer;


        private void Awake() {
            _renderer     = GetComponent<SpriteRenderer>();
            _cam          = Camera.main;
            _camTransform = _cam.transform;
        }


        private void Start() {
            // If its a front platform parallax
            if ( transform.position.z < 0 ) {
                _paralaxSpeed = transform.position.z / 2;
            } else {
                _paralaxSpeed = transform.position.z <= 100 ? transform.position.z / 100 : 1f;
            }

            // Apply AutoBlur
            if ( autoBlur && _renderer.material.shader.name == "Custom/GaussianBlur" ) {
                _renderer.material.SetFloat("radius", Mathf.Max(Mathf.Min(transform.position.z / 2, 100), 0));
            } else if ( autoBlur ) {
                Debug.LogWarning("GameObject \""
                                 + gameObject.name
                                 + "\" doesn't have the right shader for AutoBlur. Custom/GaussianBlur expected.");
            }
        }


        private void Update() {
            float dX = _camTransform.position.x - _lastCamPos.x;
            float dY = _camTransform.position.y - _lastCamPos.y;
            transform.position += dX * Vector3.right * _paralaxSpeed * (inverseX ? -1 : 1);
            transform.position += dY * Vector3.up * _paralaxSpeed * yProportion * (inverseY ? -1 : 1);
            _lastCamPos        =  _camTransform.position;
        }

    }
}