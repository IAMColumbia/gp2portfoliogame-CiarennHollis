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
        void Use(IDamageable target);

        //for command pattern bits with the player
        void PerformAttack(IDamageable target, bool isReduced);
        void PerformHeavyAttack(IDamageable target, bool isReduced);
        void PerformDashAttack(IDamageable target, bool isReduced);
        void PerformComboAttack(IDamageable target, bool isReduced);
        void PerformFinisherAttack(IDamageable target, bool isReduced);
    }
}
