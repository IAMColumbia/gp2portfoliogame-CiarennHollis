using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Utility
{
    class PressedButton
    {
        public GamePadButtons Button;
        public Timer BtnTimer;

        public PressedButton(GamePadButtons button)
        {
            this.Button = button;
            BtnTimer = new Timer();
        }
    }

    public enum GamePadButtons
    {
        A = 0,
        B = 1,
        X = 8,
        Y = 9,
        Back = 2,
        LeftShoulder = 3,
        LeftStick = 4,
        RightShoulder = 6,
        RightStick = 7,
        None = 10
    }
}
