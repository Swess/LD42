using UnityEngine;

namespace Grid {
    public static class GridAPI {

//        private const string BLOCK_MASK = "Block";
//        private const string PLAYER_MASK = "Player";
//
//        private const string TILE_MASK = "Tile";
//
//
//        public static bool HasBlockAt(Vector3 point) {
//            LayerMask blocksMask = LayerMask.GetMask(BLOCK_MASK, PLAYER_MASK); // BitMasking
//            return Physics.CheckBox(point + new Vector3(0.5f, 0.5f, 0.5f),
//                                    new Vector3(0.48f, 0.48f, 0.48f),
//                                    Quaternion.identity,
//                                    blocksMask,
//                                    QueryTriggerInteraction.Collide);
//        }
//
//
//        /// <summary>
//        /// Gets the BlockBase script of the block located at a specific location
//        /// </summary>
//        /// <param name="point"></param>
//        /// <returns></returns>
//        public static BlockBase GetBlockAt(Vector3 point) {
//            LayerMask blocksMask = LayerMask.GetMask(BLOCK_MASK, PLAYER_MASK); // BitMasking
//            Collider[] hits = Physics.OverlapBox(point + new Vector3(0.5f, 0.5f, 0.5f),
//                                                 new Vector3(0.48f, 0.48f, 0.48f),
//                                                 Quaternion.identity,
//                                                 blocksMask,
//                                                 QueryTriggerInteraction.Collide);
//
//            for ( int i = 0; i < hits.Length; i++ ) {
//                BlockBase script = hits[i].GetComponent<BlockBase>();
//                if ( script )
//                    return script;
//            }
//
//            return null;
//        }
//
//
//        /// <summary>
//        /// Gets the TileBase script of the Tile located at a specific location
//        /// </summary>
//        /// <param name="point"></param>
//        /// <returns></returns>
//        public static TileBase GetTileAt(Vector3 point) {
//            LayerMask blocksMask = LayerMask.GetMask(TILE_MASK); // BitMasking
//            Collider[] hits = Physics.OverlapBox(point + new Vector3(0.5f, 0.5f, 0.5f),
//                                                 new Vector3(0.48f, 0.48f, 0.48f),
//                                                 Quaternion.identity,
//                                                 blocksMask,
//                                                 QueryTriggerInteraction.Collide);
//
//            for ( int i = 0; i < hits.Length; i++ ) {
//                TileBase script = hits[i].GetComponent<TileBase>();
//                if ( script )
//                    return script;
//            }
//
//            return null;
//        }

    }
}