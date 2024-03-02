using Microsoft.Xna.Framework;
using MonoGameLibrary.GameComponents.Player;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Utility
{
    class TimedPlayerController : PlayerController
    {
        // P R O P E R T I E S 

        //  C O N S T R U C T O R
        public TimedPlayerController(Game game) : base(game)
        {
            SetupIInputHander(game);
        }

        // I N I T 
        protected override void SetupIInputHander(Game game)
        {
            this.input = (TimedInputHandler)game.Services.GetService(typeof(IInputHandler));
            if (input == null)
            {
                input = new TimedInputHandler(game);
                this.Game.Components.Add((TimedInputHandler)input);
            }
            base.SetupIInputHander(game);
        }
    }
}
