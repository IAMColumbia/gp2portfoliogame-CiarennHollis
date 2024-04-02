using BurnoutBuster.CommandPat;
using BurnoutBuster.CommandPat.Commands;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using SharpDX.XAudio2;

namespace BurnoutBuster.Character
{
    class CommandCreature : MonogameCreature, ICommandComponent
    {
        // P R O P E R T I E S

        //movement
        Vector2 moveOnNextUpdate;
        float lerpAdjustment;

        //combat
        ActionCommands actionToPerform;

        // C O N S T R U C T O R
        //DEPENDENCY FOR POC: enemy ref
        public CommandCreature(Game game) : base(game)
        {
            moveOnNextUpdate = Vector2.Zero;
            lerpAdjustment = 5f;
            actionToPerform = ActionCommands.Null; 
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            console.Log("action to perform", actionToPerform.ToString());
            UpdateCreatureLocation(gameTime);
            base.Update(gameTime);
        }

        // M O V E M E N T 
        #region 'Movement'
        protected override void UpdateCreatureWithController(GameTime gameTime, float time)
        {
            //Don't update creature this one uses a controller
            //  - essentially disables the controller on the monogame creature
            //base.UpdateCreatureWithController(gameTime, time);

            if (moveOnNextUpdate != Vector2.Zero) 
                return;
        }

        private void UpdateCreatureLocation(GameTime gameTime)
        {
            if (moveOnNextUpdate == Vector2.Zero) 
                return;

            //move 
            //this.Location += (moveOnNextUpdate * this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            this.Location = Vector2.Lerp(this.Location, this.Location + moveOnNextUpdate, lerpAdjustment);
            //this.Bounds.Position = this.Location;

            //update texture facing direction
            UpdateFacingDirBasedOnDirection(moveOnNextUpdate);

            //reset moveOnNextUpdate
            moveOnNextUpdate = Vector2.Zero;
        }

        private void UpdateFacingDirBasedOnDirection(Vector2 direction)
        {
            if (direction.X < 0 )
            {
                // going left so face left
            }
            else if (direction.X > 0)
            {
                // going right so face right
            }
        }
        #endregion

        // C O L L I S I O N
        public override void OnCollisionEnter(Collision.Collision collision)
        {
            if (collision != null)
            {
                if (TagManager.CompareTag(collision.OtherObject, Tags.Enemy))
                {
                    PerformAttackAction((IDamageable)collision.OtherObject, actionToPerform);
                }
            }

            base.OnCollisionEnter(collision);
        }


        // A C T I O N   I M P L E M E N T A T I O N S
        private void PerformAttackAction(IDamageable target, ActionCommands action)
        {
            switch (action)
            {
                case ActionCommands.Null:
                    // do nothing
                    break;
                case ActionCommands.Attack:
                    Weapon.PerformAttack(target);
                    break;
                case ActionCommands.HeavyAttack:
                    Weapon.PerformHeavyAttack(target);
                    break;
                case ActionCommands.DashAttack:
                    Weapon.PerformDashAttack(target);
                    break;
                case ActionCommands.ComboAttack:
                    Weapon.PerformComboAttack(target);
                    break;
                case ActionCommands.FinisherAttack:
                    Weapon.PerformFinisherAttack(target);
                    break;
            }

            actionToPerform = ActionCommands.Null;
        }

        // C O M M A N D S
        #region 'Commands'
        // MOVEMENT
        public void MoveUp()
        {
            moveOnNextUpdate = new Vector2(0, -1);
        }
        public void MoveDown()
        {
            moveOnNextUpdate = new Vector2(0, 1);
        }
        public void MoveLeft()
        {
            moveOnNextUpdate = new Vector2(-1, 0);
        }
        public void MoveRight()
        {
            moveOnNextUpdate = new Vector2(1, 0);
        }

        //ACTIONS
        public void Dash()
        {
            // logic for dash action
            moveOnNextUpdate *= 50; //TD hard coded :P
        }
        public void Attack()
        {
            // logic for attack
            actionToPerform = ActionCommands.Attack;
        }
        public void HeavyAttack()
        {
            // logic for heavy attack
            actionToPerform = ActionCommands.HeavyAttack;
        }

        public void DashAttack()
        {
            // logic for dash attack (dash + 2x attack)
            actionToPerform = ActionCommands.DashAttack;
        }
        public void ComboAttack()
        {
            // logic for combo attack (attack + heavy attack)
            actionToPerform = ActionCommands.ComboAttack;
        }
        public void FinisherAttack()
        {
            // logic for combo attack (attack + heavy + attack)
            actionToPerform = ActionCommands.FinisherAttack;
        }
        #endregion
    }
}
