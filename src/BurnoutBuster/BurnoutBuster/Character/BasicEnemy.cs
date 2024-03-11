using Microsoft.Xna.Framework;

namespace BurnoutBuster.Character
{
    public class BasicEnemy : MonogameEnemy
    {
        public BasicEnemy(Game game, MonogameCreature creature) : base(game, creature)
        {
            this.EnemyType = EnemyType.Minor;
        }
    }
}
