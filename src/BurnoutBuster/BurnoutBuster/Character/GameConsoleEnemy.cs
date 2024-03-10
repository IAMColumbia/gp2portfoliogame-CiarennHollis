using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Character
{
    public class GameConsoleEnemy : Enemy
    {
        // P R O P E R T I E S
        public GameConsole Console;
        // C O N S T R U C T O R S
        public GameConsoleEnemy()
        {
            this.Console = null;
        }
        public GameConsoleEnemy(GameConsole console)
        {
            this.Console = console;
        }

        // M E T H O D S
        public override void Log(string message)
        {
            if (Console != null)
            {
                Console.GameConsoleWrite(message);
            }
            else
            {
                base.Log(message);
            }
        }

        public virtual void Log(string label, string contents)
        {
            if (Console != null)
            {
                Console.Log(label, contents);
            }
            else
            {
                base.Log($"{label} : {contents}");
            }
        }
    }
}
