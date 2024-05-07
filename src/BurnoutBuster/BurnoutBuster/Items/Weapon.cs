using BurnoutBuster.Character;

namespace BurnoutBuster.Items
{
    abstract class Weapon : IWeapon
    {
        // P R O P E R T I E S 
        public string Name { get; set; }
        public int Damage { get; set; }
        public int AttackRadius { get; set; }

        public WeaponType Type;

        // M E T H O D S
        public virtual void Use()
        {

        }
        public virtual void Use(IDamageable target)
        {
            target.Hit(Damage);
        }
        /// </summary>
        /// <param name="target">IDamageable target being hit</param>
        /// <param name="modifier">Amount to modify the damage by. Uses multiplication</param>
        public virtual void PerformAttack(IDamageable target, int modifier)
        {
            target.Hit(Damage * modifier);
        }

        public virtual void PerformHeavyAttack(IDamageable target, int modifier)
        {
            target.Hit((Damage * 2) * modifier);
        }
        public virtual void PerformDashAttack(IDamageable target, int modifier)
        {
            target.Hit((Damage + 3) * modifier);

        }
        public virtual void PerformComboAttack(IDamageable target, int modifier)
        {
            target.Hit((Damage * 3) * modifier);
        }
        public virtual void PerformFinisherAttack(IDamageable target, int modifier)
        {
            target.Hit((Damage + 5) * modifier);
        }
    }
}
