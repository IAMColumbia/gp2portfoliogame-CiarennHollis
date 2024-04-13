using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Sprite.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.UI
{
    public class Screen
    {
        // P R O P E R T I E S
        Sprite visual;
        SpriteFont fontBold;
        SpriteFont fontRegular;
        public string primaryText;
        public string secondaryText;
        public string tertiaryText;

        Vector2 visualPosition;
        Vector2 primaryTextPosition;
        Vector2 secondaryTextPosition;
        Vector2 tertiaryTextPosition;

        // C O N S T R U C T O R
        public Screen()
        {
            //TD hard coded positions
            visualPosition = Vector2.Zero;
            primaryTextPosition = new Vector2(250, 350);
            secondaryTextPosition = new Vector2(250, 400);
            tertiaryTextPosition = new Vector2(250, 450);
        }

        // M E T H O D S
        public void LoadContent(Game game, string visualFilePath)
        {
            visual = new Sprite(game);
            visual.SpriteTexture = game.Content.Load<Texture2D>(visualFilePath);
            visual.Location = visualPosition;

            fontBold = game.Content.Load<SpriteFont>("Fonts/ImpactBold");
            fontRegular = game.Content.Load<SpriteFont>("Fonts/Impact");
        }
        public void LoadContent(Game game)
        {
            visual = null;

            fontBold = game.Content.Load<SpriteFont>("Fonts/ImpactBold");
            fontRegular = game.Content.Load<SpriteFont>("Fonts/Impact");
        }
        public void DrawScreen(SpriteBatch spriteBatch)
        {
            if (visual != null) 
                spriteBatch.DrawSprite(visual);

            spriteBatch.DrawString(fontBold, primaryText, primaryTextPosition, Color.BlanchedAlmond);
            spriteBatch.DrawString(fontBold, secondaryText, secondaryTextPosition, Color.BlanchedAlmond);
        }
    }
}
