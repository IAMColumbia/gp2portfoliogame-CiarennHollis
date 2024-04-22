using BurnoutBuster.Input;
using BurnoutBuster.Items;
using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System.Collections.Generic;

namespace BurnoutBuster.Character
{
    public class MonogameCreature : DrawableAnimatableSprite, IDamageable, IInteract, IHitBox, ICreatureSubject, IFlashableTexture
    {
        // P R O P E R T I E S
        #region 'Properties'
        //REFERENCES
        protected GameConsole console;
        TimedPlayerController controller { get; set; }
        internal GameConsoleCreature creature
        {
            get;
            private set;
        }

        //STATE
        protected CreatureState creatureState;
        public CreatureState CreatureState
        {
            get { return creatureState; }
            set
            {
                if (this.creatureState != value)
                {
                    this.creatureState = this.creature.State = value; // also updates the state of the encapsulated creature
                    OnCreatureStateChanged();
                }
            }
        }

        //STATS
        public int HitPoints
        {
            get { return this.creature.HitPoints; }
            set 
            {
                if (this.HitPoints != value)
                    this.previousHitPoints = this.HitPoints;
                this.creature.HitPoints = value; 
            }
        }
        private int previousHitPoints;
        private int originalHitPoints;
        private float originalSpeed;
        private float reducedSpeed { get => this.Speed * (2 / 3); }

        //ATTACKING
        public IWeapon Weapon
        {
            get { return this.creature.MyWeapon; }
        }
        protected MonogameWeapon MGWeapon;

        //COLLISION AND TAG
        public Rectangle Bounds { get; set; }
        public bool IsCollisionOn { get; set; }
        public Tags Tag { get; }
        public GameComponent GameObject { get; private set; }
        protected Vector2 moveVector;

        //OBSERVER
        public List<ICreatureObserver> observers { get; set; }

        //HIT BOX
        public Rectangle HitBox {  get; set; }

        //IFLASHABLE
        public Color flashColor { get; set; }
        public bool canStartFlashing { get; set; }
        public FlashingState flashingState { get; set; }
        public Timer flashingTimer { get; set; }
        public Timer individualFlashTimer { get; set; }
        #endregion

        // C O N S T R U C T O R
        public MonogameCreature(Game game) : base(game)
        {
            //setting refs
            this.controller = new TimedPlayerController(game);
            this.console = (GameConsole)game.Services.GetService<IGameConsole>();
            if (this.console == null)
            {
                this.console = new GameConsole(game);
                this.Game.Components.Add(this.console);
            }
            creature = new GameConsoleCreature((GameConsole)game.Services.GetService<IGameConsole>());

            //hitpoints
            previousHitPoints = originalHitPoints = HitPoints;

            //observers 
            observers = new List<ICreatureObserver>();

            //attacking
            MGWeapon = new MonogameSimpleSword(game);
            this.Attach(MGWeapon);
            this.creature.MyWeapon = MGWeapon.GetWeapon();

            //collision
            IsCollisionOn = true;
            GameObject = this;
            this.Tag = Tags.Player;

            //damage flashing
            canStartFlashing = false;
            flashingState = FlashingState.NotFlashing;
            flashingTimer = new Timer();
            individualFlashTimer = new Timer();
        }

        // I N I T
        #region 'Init'
        protected override void LoadContent()
        {
            this.SpriteTexture = this.Game.Content.Load<Texture2D>("CharacterSprites/Creature");
            this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
            this.Location = new Microsoft.Xna.Framework.Vector2(450, 300);

            this.ShowMarkers = true;

            this.Speed = originalSpeed = 150;

            //this.MGWeapon.Load();
            this.Notify();
            this.Game.Components.Add(MGWeapon);

            SetUpAnimations();

            base.LoadContent();
        }
        private void SetUpAnimations()
        {
            SpriteAnimation testAnim = new SpriteAnimation("test", "SpriteSheetTest", 2, 5, 1);
            this.spriteAnimationAdapter.AddAnimation(testAnim);

            //this.spriteAnimationAdapter.CurrentAnimation = testAnim;
        }

        public void Reset()
        {
            //stat & state reset 
            this.CreatureState = CreatureState.Normal;
            this.Speed = originalSpeed; 
            this.Location = new Microsoft.Xna.Framework.Vector2(450, 300);
            this.HitPoints = previousHitPoints = originalHitPoints;

            //weapon reset
            MGWeapon = new MonogameSimpleSword(this.Game);
            this.Attach(MGWeapon);
            this.Notify();
            this.creature.MyWeapon = MGWeapon.GetWeapon();

            //flashing reset
            this.flashingState = FlashingState.NotFlashing;
            flashingTimer.ResetTimer();
            individualFlashTimer.ResetTimer();
            canStartFlashing = false;
        }
        #endregion

        // U P D A T E
        #region 'Update
        public override void Update(GameTime gameTime)
        {
            float timeElapsed = (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            float timeTotal = (float)gameTime.TotalGameTime.TotalMilliseconds;
            KeepCreatureOnScreen();

            //collision
            UpdateBounds();

            //state
            UpdateStateBasedOnHP();
            UpdateBasedOnState();


            //flashing
            HandleFlash(flashColor, timeTotal);

            base.Update(gameTime);


        }

        protected virtual void UpdateCreatureWithController(GameTime gameTime, float time)
        {
            this.controller.Update(gameTime);

            this.Location += ((this.controller.Direction * (time / 1000)) * Speed); // Simple move

        }
        private void UpdateWeapon()
        {
            this.creature.MyWeapon = MGWeapon.GetWeapon();
        }
        #endregion


        // D R A W 
        #region 'Draw'
        public override void Draw(GameTime gameTime)
        {
            //MGWeapon.Draw(gameTime);
            base.Draw(gameTime);
        }
        private void KeepCreatureOnScreen()
        {
            // HORIZONTAL
            if (this.Location.X > Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width)) 
            {
                this.Location.X = Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width);  // / 2
            }
            if (this.Location.X < 0) 
                this.Location.X = 0; //(this.spriteTexture.Width / 2)


            //VERTICAL
            if (this.Location.Y > Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height))
                this.Location.Y = Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height);  // / dddaaa2

            if (this.Location.Y < 0)
                this.Location.Y = 0; //(this.spriteTexture.Height / 2)
        }
        #endregion

        // C O L L I S I O N
        #region 'Collision & Interaction'
        public virtual void OnCollisionEnter(Physics.Collision collision)
        {
            // check to make sure the collision info isn't null
            if (collision != null)
            {
                // basic collision stuff

                if (TagManager.CompareTag(collision.OtherObject, Tags.Weapon))
                {
                    console.GameConsoleWrite("Touched sword");
                    IInteractable item = collision.OtherObject as IInteractable;
                    if (item != null)
                    {
                        this.Interact(item);
                        item.OnInteraction(this);
                    }
                }
            }
        }
        public void Interact(IInteractable item)
        {
            //another check to be safe
            if (TagManager.CompareTag(item, Tags.Weapon))
            {
                MonogameWeapon tempWeapon = item as MonogameWeapon;
                if (tempWeapon != null)
                {
                    this.Detach(MGWeapon); // detaching old weapon
                    MGWeapon.isHeld = false;
                    MGWeapon.Enabled = false;

                    tempWeapon.OnInteraction(this);
                    this.MGWeapon = tempWeapon; //assigning the new weapon
                    UpdateWeapon();
                    MGWeapon.isHeld = true;
                    MGWeapon.Enabled = true;
                    this.Attach(MGWeapon); //reattaching with the new weapon instance
                    Notify();
                }
            }
        }
        private void UpdateBounds()
        {
            Bounds = new Rectangle(this.Rectangle.X, this.Rectangle.Y, 
                this.Rectangle.Height, this.Rectangle.Height);

            int increaseAmount = this.Weapon.AttackRadius;
            HitBox = new Rectangle(Bounds.X - increaseAmount, Bounds.Y - increaseAmount, 
                Bounds.Width + (increaseAmount * 2), Bounds.Height + (increaseAmount * 2));
        }

        public virtual void OnHitBoxEnter(Collision collision)
        {
            if (collision != null)
            {
                if (TagManager.CompareTag(collision.OtherObject, Tags.Enemy))
                {
                    // hitbox collision stuff
                }
            }
        }
        #endregion

        // O B S E R V E R
        #region 'Observer pattern stuff'
        public void Attach(IObserver observer)
        {
            observers.Add((ICreatureObserver)observer);
        }
        public void Detach(IObserver observer)
        {
            observers.Remove((ICreatureObserver)observer); //idk if this will work good :P
        }
        public void Notify()
        {
            foreach (ICreatureObserver observer in observers)
            {
                observer.UpdateObserver(this);
            }
        }
        #endregion

        // S T A T E
        #region 'State management'
        public bool CheckCreatureState(CreatureState state)
        {
            if (CreatureState == state)
                return true;

            return false;
        }
        private bool DidHitPointsDecreaseByLeastHalf()
        {
            if (HitPoints <= previousHitPoints / 2) return true;
            return false;
        }
        private void UpdateStateBasedOnHP()
        {
            if (DidHitPointsDecreaseByLeastHalf())
                this.CreatureState = CreatureState.Overwhelmed;

            if (HitPoints <= 0)
                this.CreatureState = CreatureState.Shutdown;
        }
        private void UpdateBasedOnState()
        {
            switch (CreatureState)
            {
                case CreatureState.Normal:
                    flashColor = Color.PaleVioletRed;
                    break;
                case CreatureState.Overwhelmed:
                    flashColor = Color.Red;
                    break;

                case CreatureState.Shutdown:

                    break;
            }
        }
        #endregion

        // I D A M A G A B L E
        #region 'IDamageable'
        public void Attack(IDamageable target)
        {
            creature.Attack(target);
        }
        protected virtual void OnCreatureStateChanged()
        {
            // logic for what happens when the creature state changes
        }
        public virtual void KnockBack(Vector2 knockbackVector)
        {
            this.Location -= knockbackVector;
        }

        public void Hit(int damageAmount)
        {
            this.creature.Hit(damageAmount);
            console.GameConsoleWrite($"PL HP = {HitPoints}");
            canStartFlashing = true;
            //this.KnockBack();
            //this.creature.KnockBack();
        }
        public void Heal(int healAmount)
        {
            this.HitPoints += healAmount;
        }
        #endregion

        // T E X T U R E   E F F E C T S
        #region 'Texture effects'
        public void HandleFlash(Color color, float time)
        {
            // CAN WE START FLASHING 
            if (canStartFlashing)
            {
                this.flashingState = FlashingState.FlashingColor;
                this.flashingTimer.StartTimer(time, 1500); // TD hard coded time length
                canStartFlashing = false;
            }

            // ARE WE STILL FLASHING?
            this.flashingTimer.UpdateTimer(time);

            if (flashingTimer.State == TimerState.Off
                || flashingTimer.State == TimerState.Ended)
            {
                flashingState = FlashingState.NotFlashing;
                this.DrawColor = Color.White;
            }


            // FLASHING BEHAVIOR
            if (flashingState != FlashingState.NotFlashing)
            {
                //what color are we supposed to be if we are flashing?
                this.individualFlashTimer.UpdateTimer(time);

                if (individualFlashTimer.State == TimerState.Off
                    || individualFlashTimer.State == TimerState.Ended) // if timer has ended, switch colors
                {
                    //update the flashing state 
                    if (flashingState == FlashingState.NormalColor)
                    {
                        flashingState = FlashingState.FlashingColor;
                    }
                    else if (flashingState == FlashingState.FlashingColor)
                    {
                        flashingState = FlashingState.NormalColor;
                    }

                    // update the draw color based on the flashing 
                    switch (flashingState)
                    {
                        case FlashingState.NormalColor:
                            this.DrawColor = Color.White;
                            break;

                        case FlashingState.FlashingColor:
                            this.DrawColor = color;
                            break;
                    }

                    individualFlashTimer.StartTimer(time, 100); // in ms
                }

            }
        }
        #endregion


    }
}
