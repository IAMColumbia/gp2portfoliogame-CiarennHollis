using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Utility
{
    public interface IPoolable
    {
        void Reset();
        void Activate(Vector2 spawnLocation);
    }
}
