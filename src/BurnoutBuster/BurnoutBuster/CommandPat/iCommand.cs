using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.CommandPat
{
    public interface ICommand
    {
        // executes on a game component
        void Execute(ICommandComponent cc);
    }
}
