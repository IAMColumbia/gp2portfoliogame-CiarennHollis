using Microsoft.Xna.Framework.Input;

namespace BurnoutBuster.Utility
{
    public class PressedKey
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
