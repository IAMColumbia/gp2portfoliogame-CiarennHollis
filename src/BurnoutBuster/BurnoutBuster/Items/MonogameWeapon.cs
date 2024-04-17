using BurnoutBuster.Character;
using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;

namespace BurnoutBuster.Items
{
    public class MonogameWeapon : DrawableSprite, IInteractable, ICreatureObserver 
    {
        // P R O P E R T I E S 
        protected IWeapon Weapon { get; set; }
        /// <summary>
        /// The offset for position the weapon on the player
        /// </summary>
        public Vector2 RenderOffset;
        public Vector2 HolderPosition { get => creatureSubject.Location; }
        /// <summary>
        /// Reference to the creature/player for having it move with the player
        /// </summary>
        public MonogameCreature creatureSubject { get; set; }
        public bool isHeld { get; set; }

        //COLLISION
        public Rectangle Bounds { get; set; }
        public bool IsCollisionOn { get; set; }
        public GameComponent GameObject { get => this; }
        public Tags Tag { get => Tags.Weapon; }


        // C O N S T R U C T O R
        public MonogameWeapon(Game game) : base(game)
        {
            //collision
            IsCollisionOn = true;
            
            //placing on the player
            RenderOffset = new Vector2(40, 0);
            isHeld = false;
        }

        public IWeapon GetWeapon()
        {
            return this.Weapon;
        }
        // I N I T
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            base.LoadContent();
        }

        // U P D A T E 
        public override void Update(GameTime gameTime)
        {
            UpdateBounds();
            if (isHeld)
                this.Location = HolderPosition - RenderOffset;

            base.Update(gameTime);
        }
        public void UpdateObserver()
        {

        }
        public void UpdateObserver(MonogameCreature creature)
        {
            isHeld = true;
            creatureSubject = creature;
        }
        // D R A W
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        // W E A P O N
        #region 'Weapon Actions'
        public void Use(IDamageable target)
        {
            this.Weapon.Use(target);
        }
        public virtual void PerformAttack(IDamageable target, int damageModifier)
        {
            this.Weapon.PerformAttack(target, damageModifier);
        }

        public virtual void PerformHeavyAttack(IDamageable target, int damageModifier)
        {
            this.Weapon.PerformHeavyAttack(target, damageModifier);  
        }
        public virtual void PerformDashAttack(IDamageable target, int damageModifier)
        {
            this.Weapon.PerformDashAttack(target, damageModifier);   

        }
        public virtual void PerformComboAttack(IDamageable target, int damageModifier)
        {
            this.Weapon.PerformComboAttack(target, damageModifier);
        }
        public virtual void PerformFinisherAttack(IDamageable target, int damageModifier)
        {
            this.Weapon.PerformFinisherAttack(target, damageModifier);
        }
        #endregion

        // C O L L I S I O N
        public void OnCollisionEnter(Collision collision)
        {
            
        }

        public void OnInteraction(IInteract subject)
        {
            IsCollisionOn = false;
            this.Enabled = true;
        }

        private void UpdateBounds()
        {
            Bounds = this.Rectangle;

            int increaseAmount = this.Weapon.AttackRadius;
        }
    }
}
