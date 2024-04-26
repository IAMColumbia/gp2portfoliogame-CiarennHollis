using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace BurnoutBuster.Character
{
    public class HeavyEnemy : MonogameEnemy
    {
        
        // C O N S T R U C T O R
        public HeavyEnemy(Game game, MonogameCreature creature) : base(game, creature)
        {
            this.EnemyType = EnemyType.Heavy;
            this.movementMode = EnemyMovementMode.SlowApproach;

            this.HitPoints = 20;
            this.Damage = 5;
        }

        // I N I T
        public override void SetUpAnimations()
        {
            Animations.Add("MainAnim",
                new SpriteAnimation("MainAnim", "CharacterSprites/HeavyEnemyAnim", 6, 4, 1));

            base.SetUpAnimations();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
            //texture set up
            this.SpriteTexture = this.Game.Content.Load<Texture2D>("CharacterSprites/HeavyEnemy");
        }

        // U P D A T E
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
