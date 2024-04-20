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
        public Rectangle Bounds { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public bool IsCollisionOn { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public GameComponent GameObject => throw new NotImplementedException();

        public Tags Tag => throw new NotImplementedException();

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
            throw new NotImplementedException();
        }

        public void OnInteraction(IInteract subject)
        {
            throw new NotImplementedException();
        }
    }
}
