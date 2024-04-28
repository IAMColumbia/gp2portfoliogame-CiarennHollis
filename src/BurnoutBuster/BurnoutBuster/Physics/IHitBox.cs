using Microsoft.Xna.Framework;

namespace BurnoutBuster.Physics
{
    public interface IHitBox : ICollidable
    {
        Rectangle HitBox { get; set; } 
        void OnHitBoxEnter(Collision collision);
    }
}
