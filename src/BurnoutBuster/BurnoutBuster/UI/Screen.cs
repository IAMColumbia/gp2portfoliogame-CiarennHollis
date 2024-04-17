﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Sprite.Extensions;

namespace BurnoutBuster.UI
{
    public class Screen
    {
        // P R O P E R T I E S
        public Texture2D visual;
        SpriteFont fontBold;
        SpriteFont fontRegular;
        public string primaryText;
        public string secondaryText;
        public string tertiaryText;

        public Vector2 primaryTextPosition;
        public Vector2 secondaryTextPosition;
        public Vector2 tertiaryTextPosition;

        // C O N S T R U C T O R
        public Screen()
        {
            //TD hard coded positions
            primaryTextPosition = new Vector2(250, 250);
            secondaryTextPosition = new Vector2(250, 300);
            tertiaryTextPosition = new Vector2(250, 350);
        }

        // M E T H O D S
        public void LoadContent(Game game, string visualFilePath)
        {
            visual = game.Content.Load<Texture2D>(visualFilePath);

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
                spriteBatch.Draw(visual, Vector2.Zero, Color.White);

            spriteBatch.DrawString(fontBold, primaryText, primaryTextPosition, Color.MistyRose);
            spriteBatch.DrawString(fontRegular, secondaryText, secondaryTextPosition, Color.MistyRose);

            if (tertiaryText != string.Empty)
                spriteBatch.DrawString(fontRegular, tertiaryText, tertiaryTextPosition, Color.MistyRose);
        }
    }
}
