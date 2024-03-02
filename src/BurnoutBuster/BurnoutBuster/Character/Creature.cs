using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Character
{
    public enum CreatureState { Normal, Overwhelmed, Shutdown }
    public class Creature
    {
        // P R O P E R T I E S
        public string Name;
        public int HitPoints
        {
            get;
            private set;
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

        // C O N S T R U C T O R
        public Creature()
        {
            Name = "Yippee";
            HitPoints = 10;
        }

        // M E T H O D S 
        public virtual void Die()
        {
            if (this.State != CreatureState.Shutdown)
            {
                this.State = CreatureState.Shutdown;
            }
        }

        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }
    }
}
