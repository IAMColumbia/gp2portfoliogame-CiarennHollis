using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;

namespace BurnoutBuster.Physics
{
    public interface ICollidable : ITaggable
    {
        Rectangle Bounds { get; set; }
        /// <summary>
        /// Reference to the game component that the thing collided with
        /// </summary> 
        GameComponent GameObject { get; } // dependency, in order to collided, thing must be a game componenet
        void OnCollisionEnter(Collision collision);
    }
}
