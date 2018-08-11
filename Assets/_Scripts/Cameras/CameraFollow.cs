using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cameras {
    /// <summary>
    /// This script makes the current GameObject follow his target
    /// in the 2D plane for a 2D game setup.
    /// </summary>
    public class CameraFollow : MonoBehaviour {

        public Transform target;
        public bool      lookAHeadNSmooth;
        public float     damping                = 0.3f;
        public float     lookAheadFactor        = 5f;
        public float     lookAheadReturnSpeed   = 6f;
        public float     lookAheadMoveThreshold = 0.1f;

        private Vector3 _lastTargetPosition;
        private Vector3 _currentVelocity;
        private Vector3 _lookAheadPos;


        private void Start() {
            transform.position  = new Vector3(target.position.x, target.position.y, transform.position.z);
            _lastTargetPosition = target.position;
        }


        private void LateUpdate() {
            // only update lookahead pos if accelerating or changed direction
            float   xMoveDelta = (target.position - _lastTargetPosition).x;
            Vector3 newPos;

            if ( lookAHeadNSmooth ) {
                bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

                if ( updateLookAheadTarget ) {
                    _lookAheadPos = lookAheadFactor * Vector3.right * Mathf.Sign(xMoveDelta);
                } else {
                    _lookAheadPos = Vector3.MoveTowards(_lookAheadPos, Vector3.zero, Time.deltaTime * lookAheadReturnSpeed);
                }

                Vector3 aheadTargetPos = target.position + Vector3.forward;
                newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref _currentVelocity, damping);
            } else {
                newPos = target.position;
            }




            newPos.z           =  transform.position.z; // Dont update z axis for a 2D game
            transform.position =  newPos;

            _lastTargetPosition = target.position;
        }

    }
}