using BurnoutBuster.Physics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    class MonogameHealthPU : MonogameItem 
    {
        // P R O P E R T I E S
        protected HealthPickUp healthPickUp { get; set; }

        // C O N S T R U C T O R
        public MonogameHealthPU(Game game) : base(game)
        {

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
            base.Draw(gameTime);
        }

        // C O L L I S I O N
    }
}
