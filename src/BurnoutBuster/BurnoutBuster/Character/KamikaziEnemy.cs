using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace BurnoutBuster.Character
{
    public class KamikaziEnemy : MonogameEnemy
    {
        // C O N S T R U C T O R
        public KamikaziEnemy(Game game, MonogameCreature creature) : base(game, creature)
        {
            this.EnemyType = EnemyType.Minor;
            this.movementMode = EnemyMovementMode.ChargePlayer;

            this.HitPoints = 7;
            this.Damage = 3;
        }

        // I N I T
        public override void SetUpAnimations()
        {
            Animations.Add("KamikaziEnemyAnim",
                new SpriteAnimation("KamikaziEnemyAnim", "CharacterSprites/KamikazeEnemyAnim", 6, 4, 1));

            base.SetUpAnimations();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            //texture set up
            this.SpriteTexture = this.Game.Content.Load<Texture2D>("CharacterSprites/KamikaziEnemy");
        }

        // U P D A T E
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
