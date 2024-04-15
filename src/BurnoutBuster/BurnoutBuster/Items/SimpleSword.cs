

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
            this.AttackRadius = 50; // in pixels
        }
    }

    class GoldSword : Weapon
    {

        // C O N S T R U C T O R
        public GoldSword()
        {
            this.Name = "Gold Sword";
            this.Type = WeaponType.Melee;
            this.Damage = 5;
            this.AttackRadius = 90; // in pixels
        }
    }
}
