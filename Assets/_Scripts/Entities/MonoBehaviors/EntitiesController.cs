using Core.Types;
using Grid.Blocks;

namespace Entities {
    /// <summary>
    /// Base class of all entity (Characters)
    /// </summary>
    public class EntitiesController : BlockBase {

        public Direction Facing { get; protected set; }

        // ========================================================
        // ========================================================
        // ========================================================


        protected virtual void Awake() {
            // Default facing
            Facing = Direction.Right;
        }


    }
}