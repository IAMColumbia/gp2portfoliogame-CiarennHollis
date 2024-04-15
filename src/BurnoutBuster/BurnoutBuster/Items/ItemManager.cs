using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    class ItemManager : GameComponent
    {
        // P R O P E R T I E S
        List<IWeapon> Weapons;

        // C O N S T R U C T O R
        public ItemManager(Game game) : base(game)
        {
            Weapons = new List<IWeapon>();
        }

        // M E T H O D S
    }
}
