using BurnoutBuster.Character;

namespace BurnoutBuster.Items
{
    public enum WeaponType { Melee, Ranged }
    public interface IWeapon
    {
        // P R O P E R T I E S
        public string Name { get; }
        public int Damage { get; }
        public int AttackRadius { get; }

        // M E T H O D S
        void Use(IDamageable target);
        void PerformAttack(IDamageable target);
        void PerformHeavyAttack(IDamageable target);
        void PerformDashAttack(IDamageable target);
        void PerformComboAttack(IDamageable target);
        void PerformFinisherAttack(IDamageable target);
    }
}
