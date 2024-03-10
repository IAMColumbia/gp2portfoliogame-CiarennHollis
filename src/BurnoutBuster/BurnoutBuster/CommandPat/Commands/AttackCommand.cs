using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.CommandPat.Commands
{
    public class AttackCommand : MGCommand
    {
        public AttackCommand(Game game) : base(game)
        {
            this.CommandName = "Attack";
        }
        public override void Execute(ICommandComponent cc)
        {
            cc.Attack();
            base.Execute(cc);
        }
    }

    public class DashCommand : MGCommand
    {
        public DashCommand(Game game) : base(game)
        {
            this.CommandName = "Dash";
        }
        public override void Execute(ICommandComponent cc)
        {
            cc.Dash();
            base.Execute(cc);
        }
    }

    public class HeavyAttackCommand : MGCommand
    {
        public HeavyAttackCommand(Game game) : base(game)
        {
            this.CommandName = "Heavy Attack";
        }
        public override void Execute(ICommandComponent cc)
        {
            cc.HeavyAttack();
            base.Execute(cc);
        }
    }

    public class DashAttackCommand : MGCommand
    {
        public DashAttackCommand(Game game) : base(game)
        {
            this.CommandName = "Dash Attack";
        }
        public override void Execute(ICommandComponent cc)
        {
            cc.DashAttack();
            base.Execute(cc);
        }
    }

    public class ComboAttackCommand : MGCommand
    {
        public ComboAttackCommand(Game game) : base(game)
        {
            this.CommandName = "Combo Attack";
        }
        public override void Execute(ICommandComponent cc)
        {
            cc.ComboAttack();
            base.Execute(cc);
        }
    }

    public class FinisherAttackCommand : MGCommand
    {
        public FinisherAttackCommand(Game game) : base(game)
        {
            this.CommandName = "Finisher Attack";
        }
        public override void Execute(ICommandComponent cc)
        {
            cc.FinisherAttack();
            base.Execute(cc);
        }
    }
}
