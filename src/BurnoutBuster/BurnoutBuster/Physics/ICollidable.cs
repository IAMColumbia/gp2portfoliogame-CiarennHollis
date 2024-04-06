using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Physics
{
    public interface ICollidable : ITaggable
    {
        Rectangle Bounds { get; set; }
        GameComponent GameObject { get; } // dependency, in order to collided, thing must be a game componenet
        void OnCollisionEnter(Collision collision);
    }
}
