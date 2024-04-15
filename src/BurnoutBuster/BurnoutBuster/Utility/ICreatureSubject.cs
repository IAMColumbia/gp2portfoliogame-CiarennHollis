using System.Collections.Generic;

namespace BurnoutBuster.Utility
{
    interface ICreatureSubject
    {
        List<ICreatureObserver> observers { get; set; }
    }
}
