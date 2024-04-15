using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Utility
{
    public interface ISubject
    {
        List<IObserver> observers { get; set; }
        void Attach(IObserver observer);
        void Detach(IObserver observer);
        void Notify();
    }
}
