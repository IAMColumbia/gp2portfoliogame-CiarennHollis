

namespace BurnoutBuster.Character
{
    public enum EnemyState { InActive, Normal, Stunned, Dead }
    public enum EnemyType {  Minor, Ranged, Melee, Heavy }
    interface IEnemy 
    {
        // P R O P E R T I E S
        public int HitPoints { get; }
        public EnemyState State { get; }
        public int Damage { get; }

        // M E T H O D S
        void Move();
        void Attack(IDamageable target);
        void Die();
        
    }
}
