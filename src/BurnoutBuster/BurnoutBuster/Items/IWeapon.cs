using BurnoutBuster.Character;

namespace BurnoutBuster.Items
{
    public enum WeaponType { Melee, Ranged }
    public interface IWeapon : IItem
    {
        // P R O P E R T I E S
        public int Damage { get; }
        public int AttackRadius { get; }

        // M E T H O D S
        //void Use(IDamageable target);

        //for command pattern bits with the player
        void PerformAttack(IDamageable target, int modifier);
        void PerformHeavyAttack(IDamageable target, int modifier);
        void PerformDashAttack(IDamageable target, int modifier);
        void PerformComboAttack(IDamageable target, int modifier);
        void PerformFinisherAttack(IDamageable target, int modifier);
    }
}
