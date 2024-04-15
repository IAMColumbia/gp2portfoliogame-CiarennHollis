using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    public interface IInteract
    {
        void Interact(IInteractable item);
    }
}
