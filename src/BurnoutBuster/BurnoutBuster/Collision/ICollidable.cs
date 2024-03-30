using Microsoft.Xna.Framework;
using MonoGame.Extended;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Collision
{
    public interface ICollidable
    {
        Rectangle Bounds { get; }
        IGameComponent gameObject { get; } // dependency, in order to collided, thing must be a game componenet
        void OnCollisionEnter(ICollidable other);
    }
}
