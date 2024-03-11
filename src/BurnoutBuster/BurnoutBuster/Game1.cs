using BurnoutBuster.Character;
using BurnoutBuster.CommandPat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Util;
using System;

namespace BurnoutBuster
{
    public class Game1 : Game
    {
        // P R O P E R T I E S 
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        //console
        GameConsole console;

        //characters
        MonogameCreature creature;
        MonogameEnemy enemy;

        // command pattern
        CommandProcessor commandProcessor;

        // C O N S T R U C T O R
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;


            creature = new CommandCreature(this, enemy); //DEPENDENCY FOR POC
            this.Components.Add(creature); 

            enemy = new BasicEnemy(this, creature); //DEPENDENCY FOR POC
            this.Components.Add(enemy);

            commandProcessor = new CommandProcessor(this, creature);
            this.Components.Add(commandProcessor);
        }

        // I N I T 
        protected override void Initialize()
        {
            creature.enemy = this.enemy;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            console = (GameConsole)this.Services.GetService<IGameConsole>();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        // U P D A T E 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            WriteConsoleInfo();

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

            console.Log("Enemy", enemy.HitPoints.ToString());
        }

    }
}
