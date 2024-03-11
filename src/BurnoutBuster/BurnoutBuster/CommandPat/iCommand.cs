

namespace BurnoutBuster.CommandPat
{
    public interface ICommand
    {
        // executes on a game component
        void Execute(ICommandComponent cc);
    }
}
