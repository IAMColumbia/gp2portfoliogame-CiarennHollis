using MonoGameLibrary.Util;
using Microsoft.Xna.Framework;

namespace BurnoutBuster.CommandPat
{
    public class MGCommand : Command
    {
        GameConsole console;

        public MGCommand(Game game)
        {
            console = (GameConsole)game.Services.GetService<IGameConsole>();
            if (console == null)
            {
                console = new GameConsole(game);
                game.Components.Add(console);
            }
        }

        protected override string Log()
        {
            string LogString = base.Log();

            console.GameConsoleWrite(LogString);

            return LogString;
        }
    }
}
