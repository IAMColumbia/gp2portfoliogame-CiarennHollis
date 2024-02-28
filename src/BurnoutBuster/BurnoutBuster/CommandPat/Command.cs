using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.CommandPat
{
    public class Command : ICommand
    {
        // P R O P E R T I E S
        public string CommandName;

        // M E T H O D S
        public void Execute(GameComponent gc)
        {
            throw new NotImplementedException();
        }

        protected virtual string Log()
        {
            string LogString = $" {CommandName} executed";

            return LogString;
        }
    }
}
