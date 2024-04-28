using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    class MonogameItem : DrawableGameComponent, IInteractable
    {

        // P R O P E R T I E S

        protected Item item;

        //collision
        public Rectangle Bounds { get; set; }
        public bool IsCollisionOn { get; set; }

        public GameComponent GameObject { get => this; }

        public Tags Tag{ get; set; }

        // C O N S T R U C T O R
        public MonogameItem(Game game) : base(game)
        {

        }

        // I N I T
        public override void Initialize()
        {
            base.Initialize();
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

        // C O L L I S I O N
        public void OnCollisionEnter(Collision collision)
        {
            
        }

        public void OnInteraction(IInteract subject)
        {
            
        }
    }
}
