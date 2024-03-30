using BurnoutBuster.Utility;

namespace BurnoutBuster.Input
{
    public class PressedButton
    {
        public GamePadButtons Button;
        public Timer BtnTimer;

        public PressedButton(GamePadButtons button)
        {
            Button = button;
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
