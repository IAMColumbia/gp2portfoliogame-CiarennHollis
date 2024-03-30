using BurnoutBuster.Collision;
using BurnoutBuster.Input;
using BurnoutBuster.Items;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using System;
using ICollidable = BurnoutBuster.Collision.ICollidable;

namespace BurnoutBuster.Character
{
    public class MonogameCreature : DrawableSprite, IDamageable, ICollidable
    {
        // P R O P E R T I E S


        protected GameConsole console;
        TimedPlayerController controller { get; set; }

        internal GameConsoleCreature creature
        {
            get;
            private set;
        }
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
        public int HitPoints
        {
            get { return this.creature.HitPoints; }
            set { this.creature.HitPoints = value; }
        }

        public IWeapon Weapon
        {
            get { return this.creature.MyWeapon; }
        }

        // collision and tag bits
        public Rectangle Bounds { get; set; }
        public Tags Tag { get; }
        public GameComponent GameObject { get; private set; }
        protected Vector2 moveVector;

        // C O N S T R U C T O R
        //DEPENDENCY FOR POC: enemy
        public MonogameCreature(Game game) : base(game)
        {
            this.controller = new TimedPlayerController(game);
            this.console = (GameConsole)game.Services.GetService<IGameConsole>();
            if (this.console == null)
            {
                this.console = new GameConsole(game);
                this.Game.Components.Add(this.console);
            }
            creature = new GameConsoleCreature((GameConsole)game.Services.GetService<IGameConsole>());
            
            GameObject = this;
            this.Tag = Tags.Player;
        }

        // I N I T
        protected override void LoadContent()
        {
            base.LoadContent();

            this.SpriteTexture = this.Game.Content.Load<Texture2D>("CharacterSprites/creature");
            this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
            this.Location = new Microsoft.Xna.Framework.Vector2(100, 100);


            this.Speed = 150;
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            float time = (float) gameTime.ElapsedGameTime.TotalMilliseconds;

            UpdateBounds();

            KeepCreatureOnScreen();

            base.Update(gameTime);
        }

        protected virtual void UpdateCreatureWithController(GameTime gameTime, float time)
        {
            this.controller.Update(gameTime);

            this.Location += ((this.controller.Direction * (time / 1000)) * Speed); // Simple move

        }

        // D R A W 
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
        private void KeepCreatureOnScreen()
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
                if (TagManager.CompareTag(collision.OtherObject, Tags.Enemy))
                {
                    console.GameConsoleWrite("Collided with an Enemy");
                }
            }
        }
        private void UpdateBounds()
        {
            Bounds = this.Rectangle;
        }
        // M I S C   M E T H O D S
        public void Attack(IDamageable target)
        {
            creature.Attack(target);
        }
        protected virtual void OnCreatureStateChanged()
        {
            // logic for what happens when the creature state changes
        }

        public void Hit(int damageAmount)
        {
            this.creature.Hit(damageAmount);
        }

        
    }
}
