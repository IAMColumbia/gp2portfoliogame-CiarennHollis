using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Collision
{
    public interface ICollidable : ITaggable
    {
        Rectangle Bounds { get; }
        GameComponent gameObject { get; } // dependency, in order to collided, thing must be a game componenet
        void OnCollisionEnter(Collision collision);
    }
}
