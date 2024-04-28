using BurnoutBuster.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    abstract class Item : IItem
    {
        // P R O P E R T I E S
        public string Name { get; set; }

        // C O N S T R U C T O R
        public Item(string name)
        {
            this.Name = name;
        }

        // M E T H O D S
        public virtual void Use()
        {

        }
        public virtual void Use(IDamageable target)
        {

        }
    }
}
