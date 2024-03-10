using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Character
{
    public enum EnemyState {  Normal, Stunned, Dead }
    public enum EnemyType {  Minor, Ranged, Melee, Heavy }
    interface IEnemy 
    {
        // P R O P E R T I E S
        public int HitPoints { get; }
        public EnemyState State { get; }

        // M E T H O D S
        void Attack();
        void Die();
        
    }
}
