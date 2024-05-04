using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace BurnoutBuster.Items
{
    class MonogameSimpleSword : MonogameWeapon
    {
        // C O N S T R U C T O R
        public MonogameSimpleSword(Game game) : base(game)
        {
            this.Weapon = new SimpleSword();
        }

        // I N I T
        public override void SetUpAnimations()
        {
            this.IsCollisionOn = false;

            Animations.Add("BasicAttack",
                new SpriteAnimation("SSBasicAttackAnim", "Items/SimpleSwordHeavyAnim", 24, 7, 1, false)
                {
                    IsPaused = true
                });
            //Animations.Add("HeavyAttack",
            //    new SpriteAnimation("SSHeavyAttackAnim", "Items/SimpleSwordHeavyAnim", 24, 7, 1, false)
            //    {
            //        IsPaused = true
            //    });

            base.SetUpAnimations();
        }
        protected override void LoadContent()
        {
            this.spriteTexture = Game.Content.Load<Texture2D>("Items/SimpleSword");
            base.LoadContent();
        }

        //U P D A T E 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

    }
}
