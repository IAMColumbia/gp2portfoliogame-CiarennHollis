using Microsoft.Xna.Framework;

namespace BurnoutBuster.CommandPat.Commands
{
    public enum ActionCommands { Null, Attack, Dash, HeavyAttack, DashAttack, ComboAttack, FinisherAttack }
    public class NullCommand : MGCommand
    {
        public NullCommand(Game game) : base(game)
        {
            this.CommandName = "Null";
        }
        public override void Execute(ICommandComponent cc)
        {
            //base.Execute(cc);
#if DEBUG
            this.Log();
#endif
        }
    }

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
#if DEBUG
            this.Log();
#endif
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
#if DEBUG
            this.Log();
#endif
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
#if DEBUG
            this.Log();
#endif
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
#if DEBUG
            this.Log();
#endif
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
#if DEBUG
            this.Log();
#endif
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
#if DEBUG
            this.Log();
#endif
        }
    }
}
