using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Physics
{
    public class Collision 
    {
        /// <summary>
        /// the object attached to the collision
        /// </summary>
        /// <param name="OtherObject">OtherObject</param>
        /// <returns>the other object attached to the collision </returns>
        public ICollidable OtherObject { get; internal set; }

        /// <summary>
        /// the object attached to the collision/// </summary>
        /// <param name="PenetrationVector">PenetrationVector</param>
        /// <returns>the vector of the collision, the how far the objects collided into each other</returns>
        public Microsoft.Xna.Framework.Vector2 PenetrationVector { get; internal set; }
    }
}
