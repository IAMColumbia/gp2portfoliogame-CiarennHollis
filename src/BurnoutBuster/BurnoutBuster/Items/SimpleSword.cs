﻿

namespace BurnoutBuster.Items
{
    class SimpleSword : Weapon
    {
        
        // C O N S T R U C T O R
        public SimpleSword() 
        {
            this.Name = "Simple Sword";
            this.Type = WeaponType.Melee;
            this.Damage = 1;
            this.AttackRadius = 40; // in pixels
        }
    }
}
