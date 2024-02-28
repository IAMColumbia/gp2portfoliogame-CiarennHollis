using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace BurnoutBuster.Utility
{
    class KeyMap
    {
        public Dictionary<Keys, string> OnReleasedKeyMap, OnKeyDownMap;

        public KeyMap()
        {
            OnReleasedKeyMap = new Dictionary<Keys, string>();
            OnKeyDownMap = new Dictionary<Keys, string>();
            this.Initialize();
        }

        public virtual void Initialize()
        {
            OnKeyDownMap.Add(Keys.Up, "Action 1");
            OnKeyDownMap.Add(Keys.Down, "Action 2");
            OnKeyDownMap.Add(Keys.Left, "Action 3");
            OnKeyDownMap.Add(Keys.Right, "Action 4");
        }
    }

    class ButtonMap
    {
        public Dictionary<GamePadButtons, string> OnReleasedButtonMap, OnButtonDownMap;

        public ButtonMap()
        {
            OnReleasedButtonMap = new Dictionary<GamePadButtons, string>();
            OnButtonDownMap = new Dictionary<GamePadButtons, string>();
            this.Initialize();
        }

        public virtual void Initialize()
        {
            OnButtonDownMap.Add(GamePadButtons.A, "Action 1");
            OnButtonDownMap.Add(GamePadButtons.B, "Action 2");
            OnButtonDownMap.Add(GamePadButtons.X, "Action 3");
            OnButtonDownMap.Add(GamePadButtons.Y, "Action 4");
        }
    }
}
