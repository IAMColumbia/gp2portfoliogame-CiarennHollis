using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.CommandPat.Commands
{
    public class MoveLeftCommand : MGCommand
    {
        public MoveLeftCommand(Game game) : base (game)
        {
            this.CommandName = "Move Left";
        }

        public override void Execute(ICommandComponent cc)
        {
            cc.MoveLeft();
            base.Execute(cc);
#if DEBUG
            this.Log();
#endif
        }
    }

    public class MoveRightCommand : MGCommand
    {
        public MoveRightCommand(Game game) : base(game)
        {
            this.CommandName = "Move Right";
        }

        public override void Execute(ICommandComponent cc)
        {
            cc.MoveRight();
            base.Execute(cc);
#if DEBUG
            this.Log();
#endif
        }
    }

    public class MoveUpCommand : MGCommand
    {
        public MoveUpCommand(Game game) : base(game)
        {
            this.CommandName = "Move Up";
        }

        public override void Execute(ICommandComponent cc)
        {
            cc.MoveUp();
            base.Execute(cc);
#if DEBUG
            this.Log();
#endif
        }
    }

    public class MoveDownCommand : MGCommand
    {
        public MoveDownCommand(Game game) : base(game)
        {
            this.CommandName = "Move Down";
        }

        public override void Execute(ICommandComponent cc)
        {
            cc.MoveDown();
            base.Execute(cc);
#if DEBUG
            this.Log();
#endif
        }
    }
}
