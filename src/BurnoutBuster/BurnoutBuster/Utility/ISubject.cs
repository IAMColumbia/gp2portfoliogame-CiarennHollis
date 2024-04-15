using System.Collections.Generic;

namespace BurnoutBuster.Utility
{
    public interface ISubject
    {
        List<IObserver> creatureObservers { get; set; }
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}
