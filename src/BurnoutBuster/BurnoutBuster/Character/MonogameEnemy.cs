using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Character
{
    public abstract class MonogameEnemy : DrawableSprite
    {
        // P R O P E R T I E S
        protected GameConsole console;
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

        // C O N S T R U C T O R 
        public MonogameEnemy (Game game) : base (game)
        {
            this.console = (GameConsole) game.Services.GetService<IGameConsole>();
            if (this.console == null)
            {
                this.console = new GameConsole(game);
                this.Game.Components.Add(this.console);
            }
            enemy = new GameConsoleEnemy(console);
        }

        // I N I T
        public override void Initialize()
        {
            base.Initialize();
        }
        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            
            KeepEnemyOnScreen();
            base.Update(gameTime);
        }
        // D R A W 
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        // M I S C  M E T H O D S
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
        void OnEnemyStateChanged ()
        {
            // logic for what happens when the enemy state changes
        }
        public virtual void Move()
        {
            // implement move behavior [TD]
            enemy.Move();
            //this.Location = Move amount;
        }
    }
}
