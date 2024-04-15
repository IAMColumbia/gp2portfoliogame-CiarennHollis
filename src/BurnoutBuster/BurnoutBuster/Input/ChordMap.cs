using BurnoutBuster.CommandPat.Commands;
using System.Collections.Generic;

namespace BurnoutBuster.Input
{
    class ChordMap
    {
        public Dictionary<Chord, ActionCommands> MappedCommands;

        public ChordMap()
        {
            MappedCommands = new Dictionary<Chord, ActionCommands>();
            Initialize();
        }

        public void Initialize()
        {
            MappedCommands.Add(new Chord(0, 0, 0), ActionCommands.Null);

            MappedCommands.Add(new Chord(ActionCommands.Attack, 0, 0), ActionCommands.Attack);
            MappedCommands.Add(new Chord(ActionCommands.HeavyAttack, 0, 0), ActionCommands.HeavyAttack);
            MappedCommands.Add(new Chord(ActionCommands.Attack, ActionCommands.HeavyAttack, 0), ActionCommands.ComboAttack);
            MappedCommands.Add(new Chord(ActionCommands.Attack, ActionCommands.HeavyAttack, ActionCommands.Attack), ActionCommands.FinisherAttack);

            MappedCommands.Add(new Chord(ActionCommands.Dash, 0, 0), ActionCommands.Dash);
            MappedCommands.Add(new Chord(ActionCommands.Dash, ActionCommands.Attack, 0), ActionCommands.DashAttack);
        }
    }
}
