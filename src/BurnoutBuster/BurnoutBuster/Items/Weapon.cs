using BurnoutBuster.Character;

namespace BurnoutBuster.Items
{
    abstract class Weapon : IWeapon
    {
        // P R O P E R T I E S 
        public string Name { get; set; }
        public int Damage { get; set; }
        public int ReducedDamage { get => Damage * (2 / 3); }
        public int AttackRadius { get; set; }

        public WeaponType Type;

        // M E T H O D S
        public void Use(IDamageable target)
        {
            target.Hit(Damage);
        }
        public virtual void PerformAttack(IDamageable target, bool isReduced)
        {
            if (isReduced) { target.Hit(ReducedDamage); }
            else { target.Hit(Damage); }
        }

        public virtual void PerformHeavyAttack(IDamageable target, bool isReduced)
        {
            if (isReduced) { target.Hit(ReducedDamage); }
            else { target.Hit(Damage * 2); }
        }
        public virtual void PerformDashAttack(IDamageable target, bool isReduced)
        {
            if (isReduced) { target.Hit(ReducedDamage); }
            else { target.Hit(Damage + 3); }

        }
        public virtual void PerformComboAttack(IDamageable target, bool isReduced)
        {
            if (isReduced) { target.Hit(ReducedDamage); }
            else { target.Hit(Damage * 3); }
        }
        public virtual void PerformFinisherAttack(IDamageable target, bool isReduced)
        {
            if (isReduced) { target.Hit(ReducedDamage); }
            else { target.Hit(Damage + 5); }
        }
    }
}
