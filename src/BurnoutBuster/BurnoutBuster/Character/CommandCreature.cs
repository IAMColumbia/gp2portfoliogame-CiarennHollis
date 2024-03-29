using BurnoutBuster.CommandPat;
using Microsoft.Xna.Framework;

namespace BurnoutBuster.Character
{
    class CommandCreature : MonogameCreature, ICommandComponent
    {
        // P R O P E R T I E S
        Vector2 moveOnNextUpdate;

        // C O N S T R U C T O R
        //DEPENDENCY FOR POC: enemy ref
        public CommandCreature(Game game) : base(game)
        {
            moveOnNextUpdate = Vector2.Zero;
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            UpdateCreatureLocation(gameTime);
            base.Update(gameTime);
        }

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
            this.Location += (moveOnNextUpdate * this.Speed * (float)gameTime.ElapsedGameTime.TotalSeconds);
            this.Bounds.Position = this.Location;

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

        // C O M M A N D S
        #region 'Commands'
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


        public void Dash()
        {
            // logic for dash action
            moveOnNextUpdate *= 25;
        }
        public void Attack()
        {
            // logic for attack
            //if (CollisionCheck())
            //{
            //    Weapon.PerformAttack(enemy);
            //}
        }
        public void HeavyAttack()
        {
            // logic for heavy attack
            //if (CollisionCheck())
            //{
            //    Weapon.PerformHeavyAttack(enemy);
            //}
        }

        public void DashAttack()
        {
            // logic for dash attack (dash + 2x attack)
            //if (CollisionCheck())
            //{
            //    Weapon.PerformDashAttack(enemy);
            //}
        }
        public void ComboAttack()
        {
            // logic for combo attack (attack + heavy attack)
            //if (CollisionCheck())
            //{
            //    Weapon.PerformComboAttack(enemy);
            //}
        }
        public void FinisherAttack()
        {
            // logic for combo attack (attack + heavy + attack)
            //if (CollisionCheck())
            //{
            //    Weapon.PerformFinisherAttack(enemy);
            //}
        }
        #endregion
    }
}
