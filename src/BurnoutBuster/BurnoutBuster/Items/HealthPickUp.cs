using BurnoutBuster.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    class HealthPickUp : Item
    {
        // P R O P E R T I E S
        private int healthIncrease;

        // C O N S T R U C T O R
        public HealthPickUp(string name, int healthIncrease) : base(name)
        {
            this.healthIncrease = healthIncrease;
        }

        // M E T H O D S
        public override void Use(IDamageable target)
        {
            target.Heal(healthIncrease);
            base.Use(target);
        }
    }
}
