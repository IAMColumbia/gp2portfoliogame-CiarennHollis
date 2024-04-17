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

        //for command pattern bits with the player
        void PerformAttack(IDamageable target, int damageModifier);
        void PerformHeavyAttack(IDamageable target, int damageModifier);
        void PerformDashAttack(IDamageable target, int damageModifier);
        void PerformComboAttack(IDamageable target, int damageModifier);
        void PerformFinisherAttack(IDamageable target, int damageModifier);
    }
}
