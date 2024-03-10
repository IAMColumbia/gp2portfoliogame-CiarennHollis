using BurnoutBuster.Character;
using SharpDX.Direct3D11;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
