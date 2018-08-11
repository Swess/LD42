using System;
using Core.Types;
using UnityEngine;

namespace Grid.Blocks {

    /// <summary>
    /// Node to simulate a virtual chain built on the fly by Physics detection and block rules
    /// </summary>
    public class BlockBase : MonoBehaviour {

        public bool isFixed = true;

        private BlockBase _next = null;    // Pointer to next block on the current wanted direction check


        public void MoveAt(Vector3 pos) { transform.position = pos; }


        /// <summary>
        /// MoveToward overload
        /// </summary>
        /// <param name="direction"></param>
        /// <returns>bool for success</returns>
        protected bool MoveToward(Vector3 direction) {
            Direction dir = TypesHelper.VectorToDirection(direction);
            return MoveToward(dir);
        }

        /// <summary>
        /// Moves the block in the given direction if possible
        /// </summary>
        /// <param name="direction"></param>
        /// <param name="forceMove">Ensure that is doesn't double check the chain for nothing</param>
        /// <returns></returns>
        protected bool MoveToward(Direction direction, bool forceMove = false) {
            if ( !forceMove && !CanMoveToward(direction) )
                return false;

            // Virtual propagation chain & moving everything in the given direction
            if ( _next )
                _next.MoveToward(direction, true);

            MoveAt( transform.position + TypesHelper.DirectionToVector(direction) );
            return true;
        }


        /// <summary>
        /// Determine if the movement is allowed in a direction on the grid.
        /// Build a virtual chain at the same time.
        /// </summary>
        /// <param name="direction"></param>
        /// <returns></returns>
        protected bool CanMoveToward(Direction direction) {
            if ( isFixed || direction == Direction.None )
                return false;

            bool result;
            Vector3 wantedPosition = transform.position + TypesHelper.DirectionToVector(direction);
            _next = GridAPI.GetBlockAt(wantedPosition);

            if ( _next ) {
                result = _next.CanMoveToward(direction);
            } else {
                // No block found => Can move by default
                result = true;
            }

            return FilterCanMoveForward(result, _next);
            // TODO : Add a filter that does the opposite: receives the object pushing.
        }


        protected virtual bool FilterCanMoveForward(bool state, BlockBase nextBlock) { return state; }

    }
}