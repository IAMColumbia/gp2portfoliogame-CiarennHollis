using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Utility
{
    interface IAnimatable
    {
        Dictionary<string, SpriteAnimation> Animations { get; set; }
        void SetUpAnimations();
        void PlayAnimation(SpriteAnimation anim);
    }
}
