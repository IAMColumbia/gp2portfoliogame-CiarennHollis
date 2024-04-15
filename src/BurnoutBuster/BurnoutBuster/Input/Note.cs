using BurnoutBuster.CommandPat.Commands;
using BurnoutBuster.Utility;

namespace BurnoutBuster.Input
{
    /// <summary>
    /// An ICommand with a duration
    /// </summary>
    public class Note 
    {
        public ActionCommands Command;
        public Timer Duration;

        public Note(ActionCommands command, float currentTime, float timerDuration)
        {
            Command = command;
            Duration = new Timer();
            Initialize(currentTime, timerDuration);
        }

        void Initialize(float currentTime, float timerDuration)
        {
            Duration.StartTimer(currentTime, timerDuration);
        }

    }
}
