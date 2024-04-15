using BurnoutBuster.Character;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Utility
{
    public interface ICreatureObserver : IObserver
    {
        MonogameCreature creatureSubject { get; set; }
        void UpdateObserver(MonogameCreature creature);
    }
}
