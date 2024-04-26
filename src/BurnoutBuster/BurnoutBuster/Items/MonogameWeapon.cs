using BurnoutBuster.Character;
using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System.Collections.Generic;

namespace BurnoutBuster.Items
{
    public class MonogameWeapon : DrawableAnimatableSprite, IInteractable, ICreatureObserver, IAnimatable 
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

        //IANIMATABLE
        public Dictionary<string, SpriteAnimation> Animations { get; set; }


        // C O N S T R U C T O R
        public MonogameWeapon(Game game) : base(game)
        {
            //collision
            IsCollisionOn = true;
            
            //placing on the player
            RenderOffset = new Vector2(48, 0);
            isHeld = false;

            //animation
            Animations = new Dictionary<string, SpriteAnimation>();
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
            this.Animations = new Dictionary<string, SpriteAnimation>();
            SetUpAnimations();

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

        // I A N I M A T A B L E
        #region 'Animation Handling'
        public virtual void SetUpAnimations()
        {
            //Animations.Add("Test",
            //    new SpriteAnimation("test", "SpriteSheetTest", 2, 5, 1, true));
            //Animations.Add("Idle",
            //    new SpriteAnimation("Idle", "SpriteSheetTest", 2, 5, 1, true));

            foreach (SpriteAnimation anim in Animations.Values)
            {
                this.spriteAnimationAdapter.AddAnimation(anim);
            }

        }

        public void PlayAnimation(SpriteAnimation animation)
        {
            this.spriteAnimationAdapter.ResetAnimation(animation);
        }
        #endregion

        // W E A P O N
        #region 'Weapon Actions'
        public void Use(IDamageable target)
        {
            this.Weapon.Use(target);
        }
        public virtual void PerformAttack(IDamageable target, bool isReduced)
        {
            this.Weapon.PerformAttack(target, isReduced);
        }

        public virtual void PerformHeavyAttack(IDamageable target, bool isReduced)
        {
            this.Weapon.PerformHeavyAttack(target, isReduced);  
        }
        public virtual void PerformDashAttack(IDamageable target, bool isReduced)
        {
            this.Weapon.PerformDashAttack(target, isReduced);   

        }
        public virtual void PerformComboAttack(IDamageable target, bool isReduced)
        {
            this.Weapon.PerformComboAttack(target, isReduced);
        }
        public virtual void PerformFinisherAttack(IDamageable target, bool isReduced)
        {
            this.Weapon.PerformFinisherAttack(target, isReduced);
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
            Bounds = new Rectangle(this.Rectangle.X, this.Rectangle.Y,
                this.Rectangle.Height, this.Rectangle.Height);

            int increaseAmount = this.Weapon.AttackRadius;
        }
    }
}
