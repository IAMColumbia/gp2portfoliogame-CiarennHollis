using BurnoutBuster.Character;
using BurnoutBuster.Collision;
using BurnoutBuster.CommandPat;
using BurnoutBuster.Levels;
using BurnoutBuster.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGameLibrary.Util;
using System;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace BurnoutBuster
{
    public class Game1 : Game
    {
        // P R O P E R T I E S 
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Random rand;

        // screen
        const int mapWidth = 900;
        const int mapHeight = 600;
        public HUD HUD;

        //console
        GameConsole console;

        //collision
        private CollisionComponent _collision;
        private CollisionManager _collisionManager;

        //characters
        MonogameCreature creature;
        EnemyManager enemyManager;
        //MonogameEnemy enemy;

        // command pattern
        CommandProcessor commandProcessor;

        //levels
        //LevelManager levelManager;

        // C O N S T R U C T O R
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            rand = new Random();

            //levelManager = new LevelManager(this);
            //this.Components.Add(levelManager);

            _collision = new CollisionComponent(new RectangleF(0, 0, mapWidth, mapHeight));
            this.Components.Add(_collision);

            _collisionManager = new CollisionManager(this);
            this.Components.Add(_collisionManager);

            this.HUD = new HUD(this);
            this.Components.Add(HUD);

            creature = new CommandCreature(this); //player ref
            this.Components.Add(creature); 

            enemyManager = new EnemyManager(this, rand, creature);
            this.Components.Add(enemyManager);

            commandProcessor = new CommandProcessor(this, creature);
            this.Components.Add(commandProcessor);
        }

        // I N I T 
        protected override void Initialize()
        {
            _collision.Initialize();
            base.Initialize();
            SetScreenDimensions();
        }

        protected override void LoadContent()
        {
            console = (GameConsole)this.Services.GetService<IGameConsole>();
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            SetUpCollisionActors();
            SetUpHUDvalues();

            enemyManager.SpawnLevelEnemies(2);
        }
        private void SetScreenDimensions()
        {
            _graphics.PreferredBackBufferHeight = mapHeight;
            _graphics.PreferredBackBufferWidth = mapWidth;
            _graphics.ApplyChanges();
        }
        void SetUpCollisionActors()
        {
            this._collisionManager.AddObject(creature);
            this.enemyManager.AddEnemiesToCollisionManager(_collisionManager);
        }

        /// <summary>
        /// Initializes the values for the HUD
        /// </summary>
        void SetUpHUDvalues()
        {
            this.HUD.AddItem("Creature HP", creature.HitPoints);
        }

        // U P D A T E 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            WriteConsoleInfo();

            _collision.Update(gameTime);
            UpdateHUDvalues();

            //TD dirty game exit for VS
            if (creature.CheckCreatureState(CreatureState.Shutdown))
                Exit();

            base.Update(gameTime);
        }

        /// <summary>
        /// updates the values for the HUD
        /// </summary>
        void UpdateHUDvalues()
        {
            this.HUD.UpdateHUDSlot("Creature HP", creature.HitPoints);
        }

        // D R A W
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            //levelManager.Draw(gameTime, _spriteBatch);

            base.Draw(gameTime);
        }

        // M I S C 
        void WriteConsoleInfo()
        {
                

            console.Log("Movement controls", "WASD");
            console.Log("Attack:", "Left Arrow");
            console.Log("Heavy Attack:", "Up Arrow");
            console.Log("Dash:", "Right Arrow");

            console.Log("Dash Attack:", "Right + Left");
            console.Log("Combo Attack:", "Right + Up");
            console.Log("Finisher Attack:", "Left + Up + Left");

            //console.Log("Enemy", enemy.HitPoints.ToString());
        }


    }
}
