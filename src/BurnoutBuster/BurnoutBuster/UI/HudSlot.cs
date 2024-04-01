using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Sprites;
using MonoGame.Extended.TextureAtlases;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.UI
{
    public class HudSlot : MonoGameLibrary.Sprite.Sprite
    {
        // P R O P E R T I E S
        public string Label;
        public string Value;
        public object Item;

        // C O N S T R U C T O R S
        public HudSlot(Game game) : base(game)
        {
            Value = string.Empty;
        }

        // I N I T 
        public override void Initialize()
        {

        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        // D R A W
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}
