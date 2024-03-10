using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Character
{
    public interface IDamageable
    {
        void Hit(int damageAmount);
    }
}
