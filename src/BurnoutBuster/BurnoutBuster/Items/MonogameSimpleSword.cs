using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
