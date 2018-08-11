using System;
using Entities.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Grid.Blocks {
    public class EndTile : TileBase {

        [Serializable]
        public class WinEvent : UnityEvent<PlayerController, PlayerController> { }

        public WinEvent onWin;        // Triggered when 2 player are on the tile

        private void Awake() {
            onTileEnter.AddListener(OnEnter);
            onTileExit.AddListener(OnExit);
        }

        public void OnEnter(BlockBase other) {
            if (typeof(PlayerController) == other.GetType()) {
                PlayerController playerController = (PlayerController) other;
                playerController.SetFinish(true);

                // If the other block is also a player
                if (occupiedBy && typeof(PlayerController) == occupiedBy.GetType() && !occupiedBy.Equals(playerController)) {
                    onWin.Invoke((PlayerController)occupiedBy, playerController);
                }
            }
        }


        public void OnExit(BlockBase other) {
            if (typeof(PlayerController) == other.GetType()) {
                PlayerController playerController = (PlayerController) other;
                playerController.SetFinish(false);
            }
        }

    }
}