using Microsoft.Xna.Framework;

namespace BurnoutBuster.Utility
{
    public interface IPoolable
    {
        void Reset();
        void Activate(Vector2 spawnLocation);
    }
}
