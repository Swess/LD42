using System;
using Cameras;
using UnityEngine;

namespace Core.Types {
    public static class TypesHelper {

        // === Directions Helpers
        // ====================================


        public static Direction VectorToDirection(Vector3 direction) {
            if ( direction.normalized == Vector3.right )
                return Direction.Right;

            if ( direction.normalized == Vector3.left )
                return Direction.Left;

            if ( direction.normalized == Vector3.forward )
                return Direction.Forward;

            if ( direction.normalized == Vector3.back )
                return Direction.Backward;

            if ( direction.normalized == Vector3.up )
                return Direction.Up;

            if ( direction.normalized == Vector3.down )
                return Direction.Down;

            return Direction.None;
        }


        public static Vector3 DirectionToVector(Direction dir) {
            switch ( dir ) {
                case Direction.None:
                    return Vector3.zero;
                case Direction.Up:
                    return Vector3.up;
                case Direction.Down:
                    return Vector3.down;
                case Direction.Right:
                    return Vector3.right;
                case Direction.Left:
                    return Vector3.left;
                case Direction.Forward:
                    return Vector3.forward;
                case Direction.Backward:
                    return Vector3.back;
                default:
                    return Vector3.zero;
            }
        }



        // ====================================
        // ====================================

    }
}