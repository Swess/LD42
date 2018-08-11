using Cameras;
using Core.Types;
using Grid;
using Grid.Blocks;
using UnityEngine;
using Rewired;

namespace Entities.Player {
    public class PlayerController : EntitiesController {

        [PlayerIdProperty(typeof(RewiredConsts.Player))]
        public int player;

        private Animator _animator;
        private bool     _onFinish = false; // Is this player on the finish tile.

        public Rewired.Player PlayerInputs { get; protected set; }

        // ========================================================
        // ========================================================


        protected override void Awake() {
            base.Awake();
            PlayerInputs = ReInput.players.GetPlayer(player); // Get the MainPlayer's inputs
        }


        private void Update() { CheckForMovement(); }


        // ========================================================
        // ========================================================
        // ========================================================


        private void CheckForMovement() {
            // Check here
        }


        /// <summary>
        /// Filter the logic that determine if the block (Player in that case) can move to the next position
        /// </summary>
        /// <param name="state">Current state of being able to move</param>
        /// <param name="nextBlock">The next block script</param>
        /// <returns></returns>
        protected override bool FilterCanMoveForward(bool state, BlockBase nextBlock) {
            if ( nextBlock && nextBlock.gameObject.CompareTag("Player") ) {
                if ( typeof(PlayerController) == nextBlock.GetType() ) {
                    PlayerController otherPlayerController = (PlayerController) nextBlock;

                    // Move the player to the end tile without checking if possible
                    if ( otherPlayerController._onFinish )
                        MoveAt(otherPlayerController.transform.position);

                    return false; // Can't move normally
                }
            }

            return state;
        }


        /// <summary>
        /// Called when thje player enter the finish tile
        /// </summary>
        /// <param name="state"></param>
        public void SetFinish(bool state) {
            // Play sound here
            _onFinish = state;
        }

    }
}