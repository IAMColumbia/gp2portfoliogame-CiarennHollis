using BurnoutBuster.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public void Use(IDamageable target)
        {
            target.Hit(Damage);
        }
        public virtual void PerformAttack(IDamageable target)
        {
            target.Hit(Damage);
        }

        public virtual void PerformHeavyAttack(IDamageable target)
        {
            target.Hit(Damage * 2);
        }
    }
}
