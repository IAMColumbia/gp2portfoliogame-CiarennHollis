using BurnoutBuster.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    public interface IItem
    {
        // P R O P E R T I E S
        public string Name { get; }

        // M E T H O D S
        void Use();
        void Use(IDamageable target);

    }
}
