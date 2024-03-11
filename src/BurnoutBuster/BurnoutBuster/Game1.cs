using BurnoutBuster.Character;
using BurnoutBuster.CommandPat;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace BurnoutBuster
{
    public class Game1 : Game
    {
        // P R O P E R T I E S 
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

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

            creature = new CommandCreature(this);
            this.Components.Add(creature);

            enemy = new BasicEnemy(this);
            this.Components.Add(enemy);

            commandProcessor = new CommandProcessor(this, creature);
            this.Components.Add(commandProcessor);
        }

        // I N I T 
        protected override void Initialize()
        {

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

        }

        // U P D A T E 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            base.Update(gameTime);
        }

        // D R A W
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);


            base.Draw(gameTime);
        }
    }
}
