using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.UI
{
    public class HUD : DrawableSprite
    {
        // P R O P E R T I E S
        private HUD instance;
        public HUD Instance
        {
            get
            {
               return instance; //TD -> doesn't check to make sure this isn't null
            }
        }
        List<HudSlot> slots;

        // C O N S T R U C T O R S 
        public HUD(Game game) : base(game)
        {
            slots = new List<HudSlot>();
        }

        // I N I T
        public override void Initialize()
        {
            base.Initialize();
        }

        // U P D A T E 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        // D R A W 
        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            this.Draw(spriteBatch);
            spriteBatch.End();
            base.Draw(gameTime);
        }

        // UI MANAGEMENT
        public void AddItem(string label, Object itemReference)
        {
            HudSlot slot = new HudSlot(this.Game)
            {
                Label = label,
                Item = itemReference
            };
            slots.Add(slot);
        }
    }
}
