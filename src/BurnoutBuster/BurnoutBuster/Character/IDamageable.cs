

using Microsoft.Xna.Framework;

namespace BurnoutBuster.Character
{
    public interface IDamageable
    {
        void Hit(int damageAmount);
        void KnockBack(Vector2 knockbackVector);
    }
}
