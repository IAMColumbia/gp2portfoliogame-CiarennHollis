using BurnoutBuster.Physics;
using BurnoutBuster.Input;
using BurnoutBuster.Items;
using BurnoutBuster.UI;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using ICollidable = BurnoutBuster.Physics.ICollidable;

namespace BurnoutBuster.Character
{
    public class MonogameCreature : DrawableSprite, IDamageable, IHasHitBox, IFlashableTexture
    {
        // P R O P E R T I E S

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

        //COLLISION AND TAG
        public Rectangle Bounds { get; set; }
        public bool IsCollisionOn { get; set; }
        public Tags Tag { get; }
        public GameComponent GameObject { get; private set; }
        protected Vector2 moveVector;

        //HIT BOX
        public Rectangle HitBox {  get; set; }

        //IFLASHABLE
        public Color flashColor { get; set; }
        public bool canStartFlashing { get; set; }
        public FlashingState flashingState { get; set; }
        public Timer flashingTimer { get; set; }
        public Timer individualFlashTimer { get; set; }

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
        protected override void LoadContent()
        {
            this.SpriteTexture = this.Game.Content.Load<Texture2D>("CharacterSprites/Creature");
            this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
            this.Location = new Microsoft.Xna.Framework.Vector2(300, 450);

            this.ShowMarkers = true;

            this.Speed = 150;

            base.LoadContent();
        }

        public void Reset()
        {
            //stat & state reset 
            this.CreatureState = CreatureState.Normal;
            this.Speed = originalSpeed;
            this.Location = new Microsoft.Xna.Framework.Vector2(300, 450);
            this.HitPoints = originalHitPoints;

            //flashing reset
            this.flashingState = FlashingState.NotFlashing;
            flashingTimer.ResetTimer();
            individualFlashTimer.ResetTimer();
            canStartFlashing = false;
        }

        // U P D A T E
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

        // D R A W 
        #region 'Draw'
        public override void Draw(GameTime gameTime)
        {
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
        public virtual void OnCollisionEnter(Physics.Collision collision)
        {
            if (collision != null)
            {
                // basic collision stuff
            }
        }
        private void UpdateBounds()
        {
            Bounds = this.Rectangle;

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

        // S T A T E
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
            if (HitPoints <= 0)
                this.CreatureState = CreatureState.Shutdown;

            if (DidHitPointsDecreaseByLeastHalf())
                this.CreatureState = CreatureState.Overwhelmed;
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

        // I D A M A G A B L E
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
