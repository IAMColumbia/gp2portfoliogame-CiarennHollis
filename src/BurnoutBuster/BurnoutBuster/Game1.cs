using BurnoutBuster.Character;
using BurnoutBuster.Collision;
using BurnoutBuster.CommandPat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;

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
        const int mapHeight = 500;

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

        // C O N S T R U C T O R
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            rand = new Random();

            _collision = new CollisionComponent(new RectangleF(0, 0, mapWidth, mapHeight));
            this.Components.Add(_collision);

            _collisionManager = new CollisionManager(this);
            this.Components.Add(_collisionManager);

            creature = new CommandCreature(this); //DEPENDENCY FOR POC
            this.Components.Add(creature); 

            //enemy = new BasicEnemy(this, creature); //DEPENDENCY FOR POC
            //this.Components.Add(enemy);

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

            enemyManager.SpawnLevelEnemies();
        }
        private void SetScreenDimensions()
        {
            _graphics.PreferredBackBufferHeight = mapHeight;
            _graphics.PreferredBackBufferWidth = mapWidth;
            _graphics.ApplyChanges();
        }
        void SetUpCollisionActors()
        {
            //foreach (ITaggedCollidable taggedCollidable in _collidableObjects)
            //{
            //    _collision.Insert(taggedCollidable);
            //}

            this._collisionManager.AddObject(creature);
            this.enemyManager.AddEnemiesToCollisionManager(_collisionManager);
            //this._collisionManager.AddObject(enemy);
        }
        // U P D A T E 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            WriteConsoleInfo();

            _collision.Update(gameTime);
            base.Update(gameTime);
        }

        // D R A W
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


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
