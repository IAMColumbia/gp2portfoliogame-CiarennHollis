using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Sprite.Extensions;

namespace BurnoutBuster.UI
{
    public class Screen
    {
        // P R O P E R T I E S
        public Sprite visual;
        SpriteFont fontBold;
        SpriteFont fontRegular;
        public string primaryText;
        public string secondaryText;
        public string tertiaryText;

        Vector2 visualPosition;
        public Vector2 primaryTextPosition;
        public Vector2 secondaryTextPosition;
        public Vector2 tertiaryTextPosition;

        // C O N S T R U C T O R
        public Screen()
        {
            //TD hard coded positions
            visualPosition = Vector2.Zero;
            primaryTextPosition = new Vector2(250, 250);
            secondaryTextPosition = new Vector2(250, 300);
            tertiaryTextPosition = new Vector2(250, 350);
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
            //if (visual != null) 
            //    spriteBatch.DrawSprite(visual);

            spriteBatch.DrawString(fontBold, primaryText, primaryTextPosition, Color.BlanchedAlmond);
            spriteBatch.DrawString(fontRegular, secondaryText, secondaryTextPosition, Color.BlanchedAlmond);

            if (tertiaryText != string.Empty)
                spriteBatch.DrawString(fontRegular, tertiaryText, tertiaryTextPosition, Color.BlanchedAlmond);
        }
    }
}
