using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BurnoutBuster.Input
{
    class KeyMap
    {
        public Dictionary<Keys, string> OnReleasedKeyMap, OnKeyDownMap;

        public KeyMap()
        {
            OnReleasedKeyMap = new Dictionary<Keys, string>();
            OnKeyDownMap = new Dictionary<Keys, string>();
            Initialize();
        }

        public virtual void Initialize()
        {
            //movement
            OnKeyDownMap.Add(Keys.A, "Left");
            OnKeyDownMap.Add(Keys.D, "Right");
            OnKeyDownMap.Add(Keys.W, "Up");
            OnKeyDownMap.Add(Keys.S, "Down");

            //actions
            OnKeyDownMap.Add(Keys.Up, "Heavy");
            OnKeyDownMap.Add(Keys.Down, "Action 2");
            OnKeyDownMap.Add(Keys.Left, "Attack");
            OnKeyDownMap.Add(Keys.Right, "Dash");
        }
    }

    class ButtonMap
    {
        public Dictionary<GamePadButtons, string> OnReleasedButtonMap, OnButtonDownMap;

        public ButtonMap()
        {
            OnReleasedButtonMap = new Dictionary<GamePadButtons, string>();
            OnButtonDownMap = new Dictionary<GamePadButtons, string>();
            Initialize();
        }

        public virtual void Initialize()
        {
            // look at how the game pad handles input 
            /// DPAD
            //OnButtonDownMap.Add(GamePadButtons.B, "Left");
            //OnButtonDownMap.Add(GamePadButtons.X, "Right");
            //OnButtonDownMap.Add(GamePadButtons.Y, "Up");
            //OnButtonDownMap.Add(GamePadButtons.Y, "Down");

            /// Left thumbstick
            //OnButtonDownMap.Add(GamePadButtons.B, "Left");
            //OnButtonDownMap.Add(GamePadButtons.X, "Right");
            //OnButtonDownMap.Add(GamePadButtons.Y, "Up");
            //OnButtonDownMap.Add(GamePadButtons.Y, "Down");

            //actions
            OnButtonDownMap.Add(GamePadButtons.A, "Heavy");
            OnButtonDownMap.Add(GamePadButtons.B, "Action 2");
            OnButtonDownMap.Add(GamePadButtons.X, "Attack");
            OnButtonDownMap.Add(GamePadButtons.Y, "Dash");
        }
    }
}
