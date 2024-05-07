using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;

namespace BurnoutBuster.Items
{
    class MonogameGoldSword : MonogameWeapon
    {
        // C O N S T R U C T O R
        public MonogameGoldSword(Game game) : base(game)
        {
            this.Weapon = new GoldSword();

            //placing on the player
            RenderOffset = new Vector2(70, 12);
        }

        // I N I T
        public override void SetUpAnimations()
        {
            Animations.Add("BasicAttack",
                new SpriteAnimation("GSBasicAttackAnim", "Items/GoldSwordHeavyAnim", 12, 7, 1, false)
                {
                    IsPaused = true
                });
            //Animations.Add("HeavyAttack",
            //    new SpriteAnimation("GSHeavyAttackAnim", "Items/GoldSwordHeavyAnim", 8, 7, 1, false)
            //    {
            //        IsPaused = true
            //    });


            base.SetUpAnimations();
        }
        protected override void LoadContent()
        {
            this.spriteTexture = Game.Content.Load<Texture2D>("Items/GoldSword");
            base.LoadContent();
        }


    }
}
