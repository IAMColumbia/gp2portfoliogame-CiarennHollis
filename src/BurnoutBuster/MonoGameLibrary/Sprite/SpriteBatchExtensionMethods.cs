using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace MonoGameLibrary.Sprite.Extensions
{
    public static class SpriteBatchExtensionMethods
    {
        public static void DrawSprite(this SpriteBatch sb, Sprite sprite)
        {
            sb.Draw(sprite.SpriteTexture,
                sprite.Rectangle,
                null,
                Color.White,
                MathHelper.ToRadians(sprite.Rotate),
                sprite.Origin,
                sprite.SpriteEffects,
                0);

            sprite.DrawMarkers(sb);
        }

        public static void DrawSpriteWithShadow(this SpriteBatch sb, Sprite sprite)
        {
            sb.Draw(sprite.SpriteTexture,
               new Rectangle(sprite.Rectangle.X + 2, sprite.Rectangle.Y + 2, sprite.Rectangle.Width, sprite.Rectangle.Height),
               null,
               Color.FromNonPremultiplied(112, 112, 112, 50),
               MathHelper.ToRadians(sprite.Rotate),
               sprite.Origin,
               sprite.SpriteEffects,
               0);

            sb.Draw(sprite.SpriteTexture,
               new Rectangle(sprite.Rectangle.X + 1, sprite.Rectangle.Y + 1, sprite.Rectangle.Width, sprite.Rectangle.Height),
               null,
               Color.FromNonPremultiplied(35, 35, 35, 75),
               MathHelper.ToRadians(sprite.Rotate),
               sprite.Origin,
               sprite.SpriteEffects,
               0);

            sb.Draw(sprite.SpriteTexture,
                sprite.Rectangle,
                null,
                Color.White,
                MathHelper.ToRadians(sprite.Rotate),
                sprite.Origin,
                sprite.SpriteEffects,
                0);

           

            sprite.DrawMarkers(sb);
        }
    }
}
