using Microsoft.Xna.Framework;
using System;

namespace BurnoutBuster.Character
{
    public abstract class Enemy : IEnemy, IDamageable
    {
        // P R O P E R T I E S
        public int HitPoints { get; set; }
        public int Damage { get; }
        public EnemyType Type;
        private EnemyState state;
        public EnemyState State
        {
            get {  return state; }
            set
            {
                if (state != value)
                {
                    this.Log($"{this.ToString()} was: {state} now {value}");
                    state = value;
                }
            }
        }

        // C O N S T R U C T O R
        public Enemy()
        {
            HitPoints = 8;
            State = EnemyState.Normal;
        }

        // M E T H O D S
        public virtual void Move()
        {
            // implement movement behavior [TD]
        }
        public virtual void Attack(IDamageable target)
        {
            // attack logic [TD]
            target.Hit(Damage);
        }

        public void Hit(int damageAmount)
        {
            HitPoints -= damageAmount;
        }

        public void KnockBack(Vector2 knockbackVector)
        {
            // knock back logic
        }
        public void Die()
        {
            if (this.State != EnemyState.Dead)
                this.State = EnemyState.Dead;
        }

        public virtual void Log(string message)
        {
            Console.WriteLine(message);
        }

    }
}
