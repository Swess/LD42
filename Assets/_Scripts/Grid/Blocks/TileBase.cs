using System;
using Entities.Player;
using UnityEngine;
using UnityEngine.Events;

namespace Grid.Blocks {
    public class TileBase : MonoBehaviour {

        [HideInInspector] public BlockBase occupiedBy;

        [Serializable]
        public class TileEvent : UnityEvent<BlockBase> { }

        public TileEvent onTileEnter;
        public TileEvent onTileExit;


        public bool IsOccupied {
            get { return occupiedBy != null; }
        }


        public void OnTriggerEnter(Collider other) {
            BlockBase block = other.GetComponent<BlockBase>();
            if ( !block )
                return;

            onTileEnter.Invoke(block);
            occupiedBy  = block;
        }


        public void OnTriggerExit(Collider other) {
            BlockBase block = other.GetComponent<BlockBase>();
            if ( !block )
                return;

            onTileExit.Invoke(block);
            occupiedBy  = null;
        }

    }
}