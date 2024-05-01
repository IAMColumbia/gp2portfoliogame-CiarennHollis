using BurnoutBuster.CommandPat;
using BurnoutBuster.CommandPat.Commands;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;

namespace BurnoutBuster.Character
{
    class CommandCreature : MonogameCreature, ICommandComponent
    {
        // P R O P E R T I E S

        //MOVEMENT
        Vector2 moveOnNextUpdate;
        float lerpAdjustment;
        float reducedLerpAdjustment { get => lerpAdjustment - 2;  }

        //COMBAT
        ActionCommands actionToPerform;

        // C O N S T R U C T O R
        public CommandCreature(Game game) : base(game)
        {
            moveOnNextUpdate = Vector2.Zero;
            lerpAdjustment = 7f;
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
            {
                this.PlayAnimation(Animations["Idle"]);
                return;
            }

            //move 
            switch (this.CreatureState)
            {
                case CreatureState.Normal:
                    this.Location = Vector2.Lerp(this.Location, this.Location + moveOnNextUpdate, lerpAdjustment);
                    break;

                case CreatureState.Overwhelmed:
                    this.Location = Vector2.Lerp(this.Location, this.Location + moveOnNextUpdate, reducedLerpAdjustment);
                    break;

                case CreatureState.Shutdown:
                    //can't move if shutdown
                    break;
            }

            //play animation
            this.PlayAnimation(Animations["Walk"]);
            console.Log("Current Creature Anim", spriteAnimationAdapter.CurrentAnimation.TextureName);

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
        public override void OnCollisionEnter(Physics.Collision collision)
        {
            if (collision != null)
            {
                if (TagManager.CompareTag(collision.OtherObject, Tags.Enemy))
                {
                    
                }
            }

            base.OnCollisionEnter(collision);
        }
        public override void OnHitBoxEnter(Physics.Collision collision)
        {
            if (collision != null)
            {
                if (TagManager.CompareTag(collision.OtherObject, Tags.Enemy))
                {
                    console.GameConsoleWrite("Hit enemy");
                    PerformAttackAction((IDamageable)collision.OtherObject, actionToPerform);
                }
            }
        }


        // A C T I O N   I M P L E M E N T A T I O N S
        #region 'Actions'
        private void PerformAttackAction(IDamageable target, ActionCommands action)
        {
            switch (action)
            {
                case ActionCommands.Null:
                    // do nothing
                    break;
                case ActionCommands.Attack:
                    if (CheckCreatureState(CreatureState.Overwhelmed)) { MGWeapon.PerformAttack(target, (2 / 3) ); }
                    else { MGWeapon.PerformAttack(target, 1); }
                    
                    break;
                case ActionCommands.HeavyAttack:
                    if (CheckCreatureState(CreatureState.Overwhelmed)) { MGWeapon.PerformHeavyAttack(target, (2 / 3)); }
                    else { MGWeapon.PerformHeavyAttack(target, 1); }
                    break;
                case ActionCommands.DashAttack:
                    if (CheckCreatureState(CreatureState.Overwhelmed)) { MGWeapon.PerformDashAttack(target, (2 / 3) ); }
                    else { MGWeapon.PerformDashAttack(target, 1); }
                    break;
                case ActionCommands.ComboAttack:
                    if (CheckCreatureState(CreatureState.Overwhelmed)) { MGWeapon.PerformComboAttack(target, (2 / 3) ); }
                    else { MGWeapon.PerformComboAttack(target, 1); }
                    break;
                case ActionCommands.FinisherAttack:
                    if (CheckCreatureState(CreatureState.Overwhelmed)) { MGWeapon.PerformFinisherAttack(target, (2 / 3) ); }
                    else { MGWeapon.PerformFinisherAttack(target, 1); }
                    break;
            }

            actionToPerform = ActionCommands.Null;
        }
        #endregion

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
