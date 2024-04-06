using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BurnoutBuster.Utility
{
    public enum FlashingState { NotFlashing, NormalColor, FlashingColor }
    interface IFlashableTexture
    {
        Color flashColor { get; }
        bool canStartFlashing { get; set; }
        FlashingState flashingState { get; set; }
        Timer flashingTimer { get; set; }
        Timer individualFlashTimer { get; set; }

        void HandleFlash(Color color, float time);
    }
}
