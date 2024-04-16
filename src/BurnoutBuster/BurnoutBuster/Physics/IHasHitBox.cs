using Microsoft.Xna.Framework;

namespace BurnoutBuster.Physics
{
    public interface IHasHitBox : ICollidable
    {
        Rectangle HitBox { get; set; } 
        void OnHitBoxEnter(Collision collision);
    }
}
