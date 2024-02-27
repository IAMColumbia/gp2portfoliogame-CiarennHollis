using Microsoft.Xna.Framework.Input;

namespace BurnoutBuster.Utility
{
    class PressedKey
    {
        public Keys Key;
        public Timer KeyTimer;

        public PressedKey(Keys key)
        {
            Key = key;
            KeyTimer = new Timer();
        }
    }
}
