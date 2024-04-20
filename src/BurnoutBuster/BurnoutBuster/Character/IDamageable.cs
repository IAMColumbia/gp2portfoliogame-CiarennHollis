

using Microsoft.Xna.Framework;

namespace BurnoutBuster.Character
{
    public interface IDamageable
    {
        public int HitPoints { get; set; }
        void Hit(int damageAmount);
        void Heal(int healAmount);
        void KnockBack(Vector2 knockbackVector);
    }
}
