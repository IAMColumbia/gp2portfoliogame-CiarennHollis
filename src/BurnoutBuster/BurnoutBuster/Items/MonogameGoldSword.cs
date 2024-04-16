using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BurnoutBuster.Items
{
    class MonogameGoldSword : MonogameWeapon
    {
        // C O N S T R U C T O R
        public MonogameGoldSword(Game game) : base(game)
        {
            this.Weapon = new GoldSword();
        }

        // I N I T
        protected override void LoadContent()
        {
            this.spriteTexture = Game.Content.Load<Texture2D>("Items/GoldSword");
            base.LoadContent();
        }


    }
}
