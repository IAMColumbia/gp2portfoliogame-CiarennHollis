
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using Keys = Microsoft.Xna.Framework.Input.Keys;

namespace BurnoutBuster.Utility
{
    public class TimedInputHandler : InputHandler
    {
        // P R O P E R T I E S
        public Timer timer;
        private float currentTime;

        public Keys[] ActionKeys = new Keys[4];
        public GamePadButtons[] ActionButtons = new GamePadButtons[4];

        public PressedKey PressedKey;
        public PressedButton PressedButton;

        bool listeningForSecondPress;
            
        // C O N S T R U C T O R 
        public TimedInputHandler(Game game) : base(game)
        {
            timer = new Timer();
            PressedKey = new PressedKey(Keys.None);
            PressedButton = new PressedButton(GamePadButtons.None);
        }

        // I N I T
        public override void Initialize()
        {
            InitKeyAndButtonArrays();
            base.Initialize();
        }

        private void InitKeyAndButtonArrays()
        {
            ActionKeys[0] = Keys.Left;
            ActionKeys[1] = Keys.Right;
            ActionKeys[2] = Keys.Up;
            ActionKeys[3] = Keys.Left;

            ActionButtons[0] = GamePadButtons.A;
            ActionButtons[1] = GamePadButtons.B;
            ActionButtons[2] = GamePadButtons.X;
            ActionButtons[3] = GamePadButtons.Y;
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            currentTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            timer.UpdateTimer(currentTime); 
            PressedKey.KeyTimer.UpdateTimer(currentTime);
            PressedButton.BtnTimer.UpdateTimer(currentTime);

            base.Update(gameTime);
        }

        #region 'Keyboard Methods'
        // K E Y    M E T H O D S
        public bool WasAnArrowKeyPressed()
        {
            foreach (Keys key in ActionKeys)
            {
                if (keyboardHandler.WasKeyPressed(key))
                {
                    PressedKey = new PressedKey(key);
                    return true;
                }
            }
            return false;
        }

        public bool WasKeyDoublePressed()
        {
            if (listeningForSecondPress == false)
            {
                if (WasAnArrowKeyPressed() == true)
                {
                    PressedKey.KeyTimer.StartTimer(currentTime, 250);
                    listeningForSecondPress = true;
                }

            }
            else
            {
                if (WasTheKeyPressedAgain(PressedKey.Key))
                {
                    return true;
                }
            }
            return false;
        }

        public bool WasTheKeyPressedAgain(Keys key)
        {
            if (PressedKey.KeyTimer.State == TimerState.Running)
            {
                if (WasKeyPressed(PressedKey.Key))
                {
                    listeningForSecondPress = false;
                    return true;
                }
            }
            else if (PressedKey.KeyTimer.State == TimerState.Ended)
            {
                listeningForSecondPress = false;
                return false;
            }


            return false;
        }
        #endregion


        #region 'GamePad Methods'
        // G A M E P A D   M E T H O D S 
        public bool WasAnActionButtonPressed()
        {
            foreach(GamePadButtons button in ActionButtons)
            {
                if (gamePadHandler.WasButtonPressed(0, (ButtonType)button))
                {
                    PressedButton = new PressedButton(button);
                    return true;
                }
            }
            return false;
        }

        public bool WasButtonDoublePressed()
        {
            if (!listeningForSecondPress)
            {
                if (WasAnActionButtonPressed())
                {
                    PressedButton.BtnTimer.StartTimer(currentTime, 250);
                    listeningForSecondPress = true;
                }
            }
            else
            {
                if (WasTheButtonPressedAgain(PressedButton.Button))
                {
                    return true;
                }
            }

            return false;
        }

        public bool WasTheButtonPressedAgain(GamePadButtons button)
        {
            if (PressedButton.BtnTimer.State == TimerState.Running)
            {
                if (WasButtonPressed(0, (ButtonType)button))
                {
                    listeningForSecondPress = false;
                    return true;
                }
            }
            else if (PressedButton.BtnTimer.State == TimerState.Ended)
            {
                listeningForSecondPress = false;
                return false;
            }

            return false;
        }
        #endregion

    }
}
