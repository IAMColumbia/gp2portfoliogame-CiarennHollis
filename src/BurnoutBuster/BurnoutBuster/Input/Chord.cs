using BurnoutBuster.CommandPat.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BurnoutBuster.Input
{
    /// <summary>
    /// A collection of 2-3 notes.
    /// </summary>
    public struct Chord
    {
        // P R O P E R T I E S
        public ActionCommands Note1;
        public ActionCommands Note2;
        public ActionCommands Note3;

        // C O N S T R U C T O R
        public Chord(ActionCommands note1, ActionCommands note2, ActionCommands note3)
        {
            Note1 = note1;
            Note2 = note2;
            Note3 = note3;
        }

    }
}
