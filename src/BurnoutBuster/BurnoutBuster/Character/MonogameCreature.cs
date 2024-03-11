using BurnoutBuster.Items;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;

namespace BurnoutBuster.Character
{
    public class MonogameCreature : DrawableSprite
    {
        // P R O P E R T I E S

        //DEPENDENCY FOR POC
        public MonogameEnemy enemy;

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
        
        public IWeapon Weapon
        {
            get { return this.creature.MyWeapon; }
        }

        // C O N S T R U C T O R
        //DEPENDENCY FOR POC: enemy
        public MonogameCreature(Game game, MonogameEnemy enemy) : base(game)
        {
            this.controller = new TimedPlayerController(game);
            this.console = (GameConsole)game.Services.GetService<IGameConsole>();
            if (this.console == null)
            {
                this.console = new GameConsole(game);
                this.Game.Components.Add(this.console);
            }
            creature = new GameConsoleCreature((GameConsole)game.Services.GetService<IGameConsole>());
            this.enemy = enemy;
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

        // C O N T R O L L E R S   A N D   M O V E M E N T
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

        // M I S C   M E T H O D S
        public void Attack(IDamageable target)
        {
            creature.Attack(target);
        }
        protected virtual void OnCreatureStateChanged()
        {
            // logic for what happens when the creature state changes
        }

        protected bool CollisionCheck()
        {
            if (Intersects(enemy)) // DEPENDENCY FOR POC: enemy -> need to do collision a better way
            {
                return true;
            }
            return false;
        }
    }
}
