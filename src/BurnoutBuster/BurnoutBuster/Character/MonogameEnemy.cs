using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using ICollidable = BurnoutBuster.Collision.ICollidable;

namespace BurnoutBuster.Character
{

    public enum EnemyMovementMode { FollowPlayer }
    public abstract class MonogameEnemy : DrawableSprite, IDamageable, ICollidable, IPoolable
    {
        // P R O P E R T I E S

        //DEPENDENCY 
        protected MonogameCreature creature;
        
        protected GameConsole console;
        /// <summary>
        /// Encapsulated enemy
        /// </summary>
        internal GameConsoleEnemy enemy;
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

        protected EnemyMovementMode movementMode;

        // collision and tag bits
        public Rectangle Bounds { get; set; }

        public Tags Tag { get; }

        public GameComponent GameObject { get; private set; }

        // movement
        protected Vector2 moveVector;
        protected float movementSpeed;

        //attacking
        Timer attackDelayTimer;
        float attackDelayAmount;
        bool canRestartTimer;

        // C O N S T R U C T O R 
        //DEPENDENCY FOR POC: creature ref
        public MonogameEnemy (Game game, MonogameCreature creature) : base (game)
        {
            this.console = (GameConsole) game.Services.GetService<IGameConsole>();
            if (this.console == null)
            {
                this.console = new GameConsole(game);
                this.Game.Components.Add(this.console);
            }
            enemy = new GameConsoleEnemy(console);
            this.creature = creature;

            GameObject = this;
            this.Tag = Tags.Enemy;

            attackDelayTimer = new Timer();
            attackDelayTimer.State = TimerState.Off;
            attackDelayAmount = 1500; // in milliseconds
            canRestartTimer = false;
        }

        // I N I T
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            this.HitPoints = 10;
            this.originalHitPoints = HitPoints;
            this.Damage = 1;

            this.SpriteTexture = this.Game.Content.Load<Texture2D>("CharacterSprites/BasicEnemy");
            this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
            
            //this.Location = new Vector2(200, 200);
            this.movementSpeed = 1;

            //this.Bounds.Position = this.Location;
            
            
            this.ShowMarkers = true;

            base.LoadContent();
        }
        // U P D A T E
        public override void Update(GameTime gameTime)
        {

            float time = (float)gameTime.TotalGameTime.TotalMilliseconds;
            //CorrectColor();
            KeepEnemyOnScreen();

            UpdateBounds();
            ManageState();

            HandleAttackDelayTimer(time);
            this.movementSpeed = 1;

            base.Update(gameTime);
        }
        void HandleAttackDelayTimer(float time)
        {
            if (canRestartTimer)
            {
                attackDelayTimer.StartTimer(time, attackDelayAmount);
                canRestartTimer= false;
            }

            attackDelayTimer.UpdateTimer(time);
        }
        // D R A W 
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

        // C O L L I S I O N
        public void OnCollisionEnter(Collision.Collision collision)
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
                 
                if (TagManager.CompareTag(collision.OtherObject, Tags.Enemy))
                {
                    //this.Location -= collision.PenetrationVector;
                }
            }
        }
        private void UpdateBounds()
        {
            this.Bounds = this.Rectangle;
        }

        // S T A T E    M A N A G E M E N T 
        void OnEnemyStateChanged()
        {
            // logic for what happens when the enemy state changes
        }
        private void ManageState()
        {
            if (HitPoints <= 0)
                this.Die();
        }

        // M O V E M E N T
        public virtual void Move(GameTime gameTime)
        {
            if (EnemyState == EnemyState.Normal)
            {
                switch (movementMode)
                {
                    case EnemyMovementMode.FollowPlayer:
                        // follows the player
                        this.moveVector = this.creature.Location - this.Location;
                        moveVector *= (float)gameTime.ElapsedGameTime.TotalSeconds * movementSpeed;

                        this.Location = Vector2.Lerp(this.Location, this.Location + moveVector, 0.3f);
                        break;
                }
                enemy.Move();
            }
            
            //this.Bounds.Position = this.Location;
            //this.Location = Move amount;
        }

        // IDAMAGABLE
        public virtual void Hit(int damageAmount)
        {
            // play hit animation 
            this.enemy.Hit(damageAmount);
            this.console.GameConsoleWrite($"Enemy Health: {HitPoints}");
            //FlashColor(Color.Red);
        }

        public virtual void Attack(IDamageable target)
        {
            //console.GameConsoleWrite($"attack delay timer state: {attackDelayTimer.State.ToString()}");
            if (attackDelayTimer.State == TimerState.Off 
                || attackDelayTimer.State == TimerState.Ended)
            {
                enemy.Attack(target);
                canRestartTimer = true;
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

        // IPOOLABLE
        public void Reset()
        {
            this.HitPoints = originalHitPoints;
            this.enemyState = EnemyState.Inactive;
            this.Enabled = false;
        }
        public void Activate(Vector2 spawnLocation)
        {
            this.enemyState = EnemyState.Normal;
            this.Location = spawnLocation;
            this.Enabled = true;
        }

        // TEXTURE EFFECTS
        private void FlashColor(Color color)
        {
            this.DrawColor = color;
        }
        private void CorrectColor()
        {
            this.DrawColor = Color.White;
        }

    }
}
