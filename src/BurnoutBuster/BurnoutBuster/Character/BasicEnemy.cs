using Microsoft.Xna.Framework;

namespace BurnoutBuster.Character
{
    public class BasicEnemy : MonogameEnemy
    {
        // C O N S T R U C T O R 
        public BasicEnemy(Game game, MonogameCreature creature) : base(game, creature)
        {
            this.EnemyType = EnemyType.Minor;
        }

        public override void Update(GameTime gameTime)
        {
            this.Move(gameTime);

            base.Update(gameTime);
        }
        // M E T H O D S
        public override void Move(GameTime gameTime)
        {
            // follows the player
            Vector2 _moveVector = this.creature.Location - this.Location;
            _moveVector *= (float)gameTime.ElapsedGameTime.TotalSeconds;

            this.Location += _moveVector;

            base.Move(gameTime);
        }
    }
}
