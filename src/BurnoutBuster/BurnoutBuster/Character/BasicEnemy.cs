using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Character
{
    public class BasicEnemy : MonogameEnemy
    {
        public BasicEnemy(Game game) : base(game)
        {
            this.EnemyType = EnemyType.Minor;
        }
    }
}
