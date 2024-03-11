

namespace BurnoutBuster.CommandPat
{
    public interface ICommandComponent
    {
        //movement
        void MoveUp();
        void MoveDown();
        void MoveLeft();
        void MoveRight();

        //actions 
        void Dash();
        void Attack();
        void HeavyAttack();
        void DashAttack();
        void ComboAttack();
        void FinisherAttack();
        
    }
}
