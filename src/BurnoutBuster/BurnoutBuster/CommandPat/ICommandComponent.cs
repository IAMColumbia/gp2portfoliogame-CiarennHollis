using SharpDX.XAudio2;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.CommandPat
{
    public interface ICommandComponent
    {
        //movement
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();

        //actions 
        void Dash();
        void Attack();
        void HeavyAttack();
        
    }
}
