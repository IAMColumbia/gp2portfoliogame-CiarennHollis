using BurnoutBuster.Character;
using BurnoutBuster.CommandPat;
using BurnoutBuster.Items;
using BurnoutBuster.Physics;
using BurnoutBuster.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Sprite.Extensions;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using SpriteBatch = Microsoft.Xna.Framework.Graphics.SpriteBatch;

namespace BurnoutBuster
{
    public class Game1 : Game
    {
        // P R O P E R T I E S 
        enum GameState { Title, Instructions, Playing, Win, Lose }
        private GameState gameState;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        public Random rand;

        // screen
        const int mapWidth = 900;
        const int mapHeight = 600;
        public HUD HUD;
        Dictionary<string, Screen> Screens;

        //console
        GameConsole console;

        //collision
        private CollisionManager _collisionManager;

        //characters
        MonogameCreature creature;
        EnemyManager enemyManager;
        //MonogameEnemy enemy;

        // command pattern
        CommandProcessor commandProcessor;

        //levels
        Sprite background;

        //items
        ItemManager itemManager;

        // C O N S T R U C T O R
        public Game1()
        {
            gameState = GameState.Title;

            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            rand = new Random();

            background = new Sprite(this)
            {
                Location = Vector2.Zero
            };
            this.Components.Add(background);

            //levelManager = new LevelManager(this);
            //this.Components.Add(levelManager);

            _collisionManager = new CollisionManager(this);
            this.Components.Add(_collisionManager);

            this.HUD = new HUD(this);
            this.Components.Add(HUD);

            this.Screens = new Dictionary<string, Screen>();

            creature = new CommandCreature(this); //player ref
            this.Components.Add(creature); 

            enemyManager = new EnemyManager(this, rand, creature);
            this.Components.Add(enemyManager);

            itemManager = new ItemManager(this);
            this.Components.Add(itemManager);

            commandProcessor = new CommandProcessor(this, creature);
            this.Components.Add(commandProcessor);

        }

        // I N I T 
        protected override void Initialize()
        {
            //background.Initialize();

            base.Initialize();
            SetScreenDimensions();

            enemyManager.Attach(itemManager);
        }

        protected override void LoadContent()
        {
            console = (GameConsole)this.Services.GetService<IGameConsole>();
            
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            background.spriteTexture = this.Content.Load<Texture2D>("Environment/Background");

            SetUpCollisionActors();
            SetUpHUDvalues();
            SetUpScreens();
            //enemyManager.SpawnMultipleEnemies(2);
        }
        private void SetScreenDimensions()
        {
            _graphics.PreferredBackBufferHeight = mapHeight;
            _graphics.PreferredBackBufferWidth = mapWidth;
            _graphics.ApplyChanges();
        }
        /// <summary>
        /// Feeds collidable objects to the collision manager
        /// </summary>
        void SetUpCollisionActors()
        {
            this._collisionManager.AddObject(creature);
            this.enemyManager.AddEnemiesToCollisionManager(_collisionManager);

            foreach (MonogameWeapon weapon in itemManager.GetAllWeapons())
            {
                _collisionManager.AddObject(weapon);
            }
        }
        /// <summary>
        /// Initializes game screens
        /// </summary>
        void SetUpScreens()
        {
            Screens.Add("Title", new Screen()
            {
                primaryText = "BURNOUT BUSTER!",
                secondaryText = "Press [SPACE] to play",
                tertiaryText = "Press [SHIFT] for Instructions"
            });
            Screens["Title"].LoadContent(this, "Environment/TitleScreen");


            Screens.Add("Instructions", new Screen()
            {
                primaryText = "Instructions",
                secondaryText = @"
Movement controls: WASD
Attack: Left Arrow
Heavy Attack: Up Arrow
Dash: Right Arrow

Dash Attack: Dash + Attack 
    -> (Right Arrow + Left Arrow)
Combo Attack: Attack + Heavy Attack 
    -> (Right Arrow + Up Arrow)
Finisher Attack: Attack + Heavy Attack + Attack
    -> (Right Arrow + Up Arrow + Right Arrow)
",
                tertiaryText = "Press [SHIFT] for to go back",

                primaryTextPosition = new Vector2(250, 50),
                secondaryTextPosition = new Vector2(250, 100),
                tertiaryTextPosition = new Vector2(250, 450)
                
            });
            Screens["Instructions"].LoadContent(this, "Environment/InstructionsScreen");

            Screens.Add("Win", new Screen()
            {
                primaryText = "You Win!",
                secondaryText = "Press [SPACE] to play again",
                tertiaryText = "Press [ESC] to quit",

            });
            Screens["Win"].LoadContent(this);

            Screens.Add("Lose", new Screen()
            {
                primaryText = "You Lost!",
                secondaryText = "Press [SPACE] to play again",
                tertiaryText = "Press [ESC] to quit",

            });
            Screens["Lose"].LoadContent(this);
        }

        /// <summary>
        /// Initializes the values for the HUD
        /// </summary>
        void SetUpHUDvalues()
        {
            this.HUD.AddItem("Creature HP", creature.HitPoints);
            this.HUD.AddItem("Enemies Left", enemyManager.EnemiesLeftInWave);
            this.HUD.AddItem("Wave Number", enemyManager.WaveCounter);
            this.HUD.AddItem("Wave Status", enemyManager.WaveState);
        }

        // U P D A T E 
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

#if DEBUG
            if (Keyboard.GetState().IsKeyDown(Keys.OemTilde))
                _collisionManager.ToggleDebugVisuals();
#endif
            WriteConsoleInfo();

            UpdateBasedOnState(gameTime);

        }
        /// <summary>
        /// Manages the game state -> showing the different screens
        /// </summary>
        void UpdateBasedOnState(GameTime gameTime)
        {
            switch(gameState)
            {
                case GameState.Instructions:
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)
                        || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                        this.gameState = GameState.Title;
                    break;

                case GameState.Playing:
                    UpdateHUDvalues();
                    
                    // WIN/LOSE CONDITION !!
                    if (creature.CheckCreatureState(CreatureState.Shutdown))
                        this.gameState = GameState.Lose;
                    if (enemyManager.WaveCounter > 9)
                        this.gameState = GameState.Win;

                    base.Update(gameTime);
                    break;

            case GameState.Title:
            case GameState.Win:
                case GameState.Lose:
                    ResetGame();
                    if (Keyboard.GetState().IsKeyDown(Keys.Space))
                        this.gameState = GameState.Playing;
                    if (Keyboard.GetState().IsKeyDown(Keys.LeftShift)
                        || Keyboard.GetState().IsKeyDown(Keys.RightShift))
                        this.gameState = GameState.Instructions;
                    break;
            }
        }

        /// <summary>
        /// updates the values for the HUD
        /// </summary>
        void UpdateHUDvalues()
        {
            this.HUD.UpdateHUDSlot("Creature HP", creature.HitPoints);
            this.HUD.UpdateHUDSlot("Enemies Left", enemyManager.EnemiesLeftInWave);
            this.HUD.UpdateHUDSlot("Wave Number", enemyManager.WaveCounter);
            this.HUD.UpdateHUDSlot("Wave Status", enemyManager.WaveState);
        }

        // D R A W
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.DarkBlue);
            string screenToDraw = "";

            switch(gameState)
            {
                case GameState.Playing:
                    screenToDraw = "";
                    break;

                case GameState.Title:
                    screenToDraw = "Title";
                    break;

                case GameState.Instructions:
                    screenToDraw = "Instructions";
                    break;

                case GameState.Win:
                    screenToDraw = "Win";
                    break;

                case GameState.Lose:
                    screenToDraw = "Lose";
                    break;
            }

            _spriteBatch.Begin();   
            _spriteBatch.DrawSprite(background);

            //if (gameState != GameState.Playing || screenToDraw != string.Empty)
            //    Screens[screenToDraw].DrawScreen(_spriteBatch);
            if (gameState != GameState.Playing || screenToDraw != string.Empty)
            {
                _spriteBatch.Draw(Screens[screenToDraw].visual, Screens[screenToDraw].visualPosition, Color.White);
                Screens[screenToDraw].DrawScreen(_spriteBatch);
            }

#if DEBUG
            _collisionManager.DrawCollisionRectangles(_spriteBatch);
#endif
            _spriteBatch.End(); 

            //TD idk if base.Draw() can be called before spriteBatch.End() so this if statement is here
            if (gameState == GameState.Playing || screenToDraw == string.Empty)
                base.Draw(gameTime);
        }

        // M I S C 
        void WriteConsoleInfo()
        {
            //console.Log("Game State", gameState.ToString());
        }

        void ResetGame()
        {
            enemyManager.ResetForNewGame();
            creature.Reset();
        }

    }
}
