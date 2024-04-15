using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using ICollidable = BurnoutBuster.Physics.ICollidable;

namespace BurnoutBuster.Character
{

    public enum EnemyMovementMode { FollowPlayer, ChargePlayer, SlowApproach }
    public abstract class MonogameEnemy : DrawableSprite, IDamageable, ICollidable, IPoolable, IFlashableTexture
    {
        // P R O P E R T I E S
        #region 'Properties
        //REFERENCES
        /// <summary>
        /// Reference to the player
        /// </summary> 
        protected MonogameCreature creature;
        
        protected GameConsole console;
        /// <summary>
        /// Encapsulated enemy
        /// </summary>
        internal GameConsoleEnemy enemy;

        //STATE
        protected EnemyState enemyState;
        public EnemyState EnemyState
        {
            get { return enemyState; }
            set
            {
                if (this.enemyState != value)
                {
                    this.enemyState = this.enemy.State = value;
                    OnEnemyStateChanged();
                }
            }
        }

        //STATS
        protected EnemyType enemyType;
        public EnemyType EnemyType
        {
            get { return this.enemyType; }
            set
            {
                this.enemyType = this.enemy.Type = value;
            }
        }
        private int originalHitPoints;
        public int HitPoints
        {
            get { return this.enemy.HitPoints; }
            set 
            { this.enemy.HitPoints = value;  }
        }
        public int Damage
        {
            get { return this.enemy.Damage; }
            set
            {
                this.enemy.Damage = value;
            }
        }


        //MOVEMENT 
        protected Vector2 moveVector;
        protected float movementSpeed;
        protected EnemyMovementMode movementMode;
        //for charging movement behavior so it doesn't charge right away
        private int numOfUpdateCyclesToWaitBeforeMoving;
        private int numOfUpdateCyclesPassed;

        //COLLISION AND TAG BITS
        public Rectangle Bounds { get; set; }
        public bool IsCollisionOn { get; set; }
        public Tags Tag { get; }
        public GameComponent GameObject { get; private set; }

        //ATTACKING
        Timer attackDelayTimer;
        float attackDelayAmount;
        bool canRestartDelayTimer;

        //IFLASHABLE
        public Color flashColor { get => Color.Black; }
        public bool canStartFlashing { get; set; }
        public FlashingState flashingState { get; set; }
        public Timer flashingTimer {  get; set; }  
        public Timer individualFlashTimer {  get; set; }  
        #endregion

        // C O N S T R U C T O R 
        #region 'Constructor'
        public MonogameEnemy (Game game, MonogameCreature creature) : base (game)
        {
            //setting refs
            this.console = (GameConsole) game.Services.GetService<IGameConsole>();
            if (this.console == null)
            {
                this.console = new GameConsole(game);
                this.Game.Components.Add(this.console);
            }
            enemy = new GameConsoleEnemy(console);
            this.creature = creature;

            //stats 
            this.originalHitPoints = HitPoints;

            //movement
            numOfUpdateCyclesToWaitBeforeMoving = 5;
            numOfUpdateCyclesPassed = 0;

            //collision bits
            GameObject = this;
            this.Tag = Tags.Enemy;

            //attacking
            attackDelayTimer = new Timer();
            attackDelayTimer.State = TimerState.Off;
            attackDelayAmount = 1500; // in milliseconds
            canRestartDelayTimer = false;

            //damage flashing
            canStartFlashing = false;
            flashingState = FlashingState.NotFlashing;
            flashingTimer = new Timer();    
            individualFlashTimer = new Timer(); 
        }
        #endregion

        // I N I T
        #region 'Init'
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            //stats
            this.movementSpeed = 1;

            //texture set up
            this.SpriteTexture = this.Game.Content.Load<Texture2D>("CharacterSprites/BasicEnemy");
            this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
            this.ShowMarkers = true;

            base.LoadContent();
        }
        #endregion

        // U P D A T E
        #region 'Update'
        public override void Update(GameTime gameTime)
        {
            float time = (float)gameTime.TotalGameTime.TotalMilliseconds;
            KeepEnemyOnScreen();

            //collision
            UpdateBounds();

            //state
            ManageState();

            //attacking
            HandleAttackDelayTimer(time);

            //reset speed
            this.movementSpeed = 1;

            //flashing
            HandleFlash(flashColor, time);

            base.Update(gameTime);
        }
        void HandleAttackDelayTimer(float time)
        {
            if (canRestartDelayTimer)
            {
                attackDelayTimer.StartTimer(time, attackDelayAmount);
                canRestartDelayTimer= false;
            }

            attackDelayTimer.UpdateTimer(time);
        }
        #endregion

        // D R A W 
        #region 'Draw'
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        private void KeepEnemyOnScreen()
        {
            if (this.Location.X > Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2))
            {
                this.Location.X = Game.GraphicsDevice.Viewport.Width - (this.spriteTexture.Width / 2);
            }
            if (this.Location.X < (this.spriteTexture.Width / 2))
                this.Location.X = (this.spriteTexture.Width / 2);

            if (this.Location.Y > Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2))
                this.Location.Y = Game.GraphicsDevice.Viewport.Height - (this.spriteTexture.Height / 2);

            if (this.Location.Y < (this.spriteTexture.Height / 2))
                this.Location.Y = (this.spriteTexture.Height / 2);
        }
        #endregion

        // C O L L I S I O N
        #region 'Collision'
        public virtual void OnCollisionEnter(Physics.Collision collision)
        {
            if (collision != null)
            {
                
                if (TagManager.CompareTag(collision.OtherObject, Tags.Player))
                {
                    this.Location -= collision.PenetrationVector * 3.5f; //TD hard coded var
                    //console.GameConsoleWrite("touching player");
                    movementSpeed = 0;
                    this.Attack((IDamageable)collision.OtherObject);
                }
                 
                if (TagManager.CompareTag(collision.OtherObject, Tags.Enemy) ) //TD only works if the enemies don't spawn on top of each other :P
                {
                    this.Location -= collision.PenetrationVector;
                }
            }
        }
        private void UpdateBounds()
        {
            this.Bounds = this.Rectangle;
        }
        #endregion

        // S T A T E    M A N A G E M E N T 
        #region 'State management'
        void OnEnemyStateChanged()
        {
            // logic for what happens when the enemy state changes
        }
        private void ManageState()
        {
            if (HitPoints <= 0)
                this.Die();
        }
        #endregion

        // M O V E M E N T
        #region 'Movement'
        public virtual void Move(GameTime gameTime)
        {
            this.moveVector = this.creature.Location - this.Location;

            if (EnemyState == EnemyState.Normal)
            {
                switch (movementMode)
                {
                    case EnemyMovementMode.FollowPlayer:

                        moveVector *= (float)gameTime.ElapsedGameTime.TotalSeconds * movementSpeed;
                        this.Location = Vector2.Lerp(this.Location, this.Location + moveVector, 0.3f); //TD hard coded "amount"
                        break;

                    case EnemyMovementMode.ChargePlayer:
                        numOfUpdateCyclesPassed++;
                        if (numOfUpdateCyclesPassed == numOfUpdateCyclesToWaitBeforeMoving)
                        {
                            numOfUpdateCyclesPassed = 0;
                            moveVector *= (float)gameTime.ElapsedGameTime.TotalSeconds * (movementSpeed * 7); //TD hard coded speed modifier
                            this.Location = Vector2.Lerp(this.Location, this.Location + moveVector, 0.3f); //TD hard coded "amount"
                        }

                        break;

                    case EnemyMovementMode.SlowApproach:
                        moveVector *= (float)gameTime.ElapsedGameTime.TotalSeconds * (movementSpeed / 2.5f); //TD hard coded speed modifier
                        this.Location = Vector2.Lerp(this.Location, this.Location + moveVector, 0.3f); //TD hard coded "amount"
                        break;
                }
                enemy.Move();
            }
            
        }
        #endregion

        // I D A M A G A B L E
        #region 'IDamageable implementation'
        public virtual void Hit(int damageAmount)
        {
            // play hit animation 
            this.enemy.Hit(damageAmount);
            this.console.GameConsoleWrite($"Enemy Health: {HitPoints}");
            canStartFlashing = true;
        }

        public virtual void Attack(IDamageable target)
        {
            //console.GameConsoleWrite($"attack delay timer state: {attackDelayTimer.State.ToString()}");
            if (attackDelayTimer.State == TimerState.Off 
                || attackDelayTimer.State == TimerState.Ended)
            {
                enemy.Attack(target);
                canRestartDelayTimer = true;
            }
        }
        public virtual void KnockBack(Vector2 knockbackVector)
        {
            // knock back logic
            this.Location -= knockbackVector;
            this.enemy.KnockBack(knockbackVector);
        }
        public virtual void Die()
        {
            this.enemy.Die();
            this.Reset();
        }
        #endregion

        // I P O O L A B L E
        #region 'IPoolable implementation'
        public void Reset()
        {
            this.HitPoints = originalHitPoints;
            this.enemyState = EnemyState.Inactive;
            this.flashingState = FlashingState.NotFlashing;
            this.flashingTimer.ResetTimer();
            this.individualFlashTimer.ResetTimer();
            this.canStartFlashing = false;
            this.Enabled = false;
            this.IsCollisionOn = false;
        }
        public void Activate(Vector2 spawnLocation)
        {
            this.enemyState = EnemyState.Normal;
            this.Location = spawnLocation;
            this.Enabled = true;
            this.IsCollisionOn = true;
        }
        #endregion

        // T E X T U R E   E F F E C T S
        #region 'Texture effects'
        public void HandleFlash(Color color, float time)
        {
            // CAN WE START FLASHING?
            if (canStartFlashing)
            {
                this.flashingState = FlashingState.FlashingColor;
                this.flashingTimer.StartTimer(time, 1000); // TD hard coded time length
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
                    || individualFlashTimer.State == TimerState.Ended)
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
