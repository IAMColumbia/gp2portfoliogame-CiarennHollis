using BurnoutBuster.Utility;
using MonoGame.Extended.Collisions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Character
{
    public interface ITaggedCollidable : ICollisionActor , ITaggable
    {
        // combines the two interfaces together :P
    }
}
