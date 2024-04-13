using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Character
{
    public class KamikaziEnemy : MonogameEnemy
    {
        // P R O P E R T I E S

        // C O N S T R U C T O R
        public KamikaziEnemy(Game game, MonogameCreature creature) : base(game, creature)
        {
            this.EnemyType = EnemyType.Heavy;
            this.movementMode = EnemyMovementMode.SlowApproach;

            this.HitPoints = 20;
            this.Damage = 5;
        }

        public override void Update(GameTime gameTime)
        {
            this.Move(gameTime);

            base.Update(gameTime);
        }

        // M E T H O D S

        public override void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {

                if (TagManager.CompareTag(collision.OtherObject, Tags.Player))
                {
                    this.Attack((IDamageable)collision.OtherObject);
                    this.Die();
                }
            }

            base.OnCollisionEnter(collision);
        }

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
