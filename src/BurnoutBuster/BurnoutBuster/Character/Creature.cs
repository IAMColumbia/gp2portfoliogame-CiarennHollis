using BurnoutBuster.Items;
using Microsoft.Xna.Framework;
using System;

namespace BurnoutBuster.Character
{
    public enum CreatureState { Normal, Overwhelmed, Shutdown } // state for later use
    public class Creature : IDamageable
    {
        // P R O P E R T I E S
        public string Name;
        public int HitPoints
        {
            get;
            set;
        }

        private CreatureState state;
        public CreatureState State
        {
            get { return state; }
            set
            {
                if (state != value)
                {
                    this.Log($"{this.ToString()} was: {state} now {value}");
                    state = value;
                }
            }
        }

        public IWeapon MyWeapon;

        // C O N S T R U C T O R
        public Creature()
        {
            Name = "Yippee";
            HitPoints = 10;
            MyWeapon = new SimpleSword();
        }

        // M E T H O D S 
        public virtual void Die()
        {
            if (this.State != CreatureState.Shutdown)
            {
                this.State = CreatureState.Shutdown;
            }
        }
        public void Hit(int damageAmount)
        {
            HitPoints -= damageAmount;
        }
        public void Attack(IDamageable target)
        {
            MyWeapon.Use(target);
        }
        public void KnockBack(Vector2 knockbackVector)
        {

        }
        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }

    }
}
