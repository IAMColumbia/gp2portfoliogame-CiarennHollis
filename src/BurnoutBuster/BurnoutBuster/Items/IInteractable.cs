using BurnoutBuster.Physics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    public interface IInteractable 
    {
        void OnInteraction(IInteract subject);
    }
}
