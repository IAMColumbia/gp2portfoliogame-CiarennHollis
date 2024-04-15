﻿using Microsoft.Xna.Framework;

namespace BurnoutBuster.Character
{
    public class BasicEnemy : MonogameEnemy
    {
        // P R O P E R T I E S


        // C O N S T R U C T O R 
        public BasicEnemy(Game game, MonogameCreature creature) : base(game, creature)
        {
            this.EnemyType = EnemyType.Minor;
            this.movementMode = EnemyMovementMode.FollowPlayer;


            this.HitPoints = 10;
            this.Damage = 1;
        }

        public override void Update(GameTime gameTime)
        {
            this.Move(gameTime);

            base.Update(gameTime);
        }
        // M E T H O D S
        public override void Move(GameTime gameTime)
        {
            base.Move(gameTime);
        }

        public override void Hit(int damageAmount)
        {
            this.moveVector = Vector2.Zero;
            base.Hit(damageAmount);
        }
    }
}
