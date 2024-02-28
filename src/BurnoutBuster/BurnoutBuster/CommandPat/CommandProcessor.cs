using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.CommandPat
{
    class CommandProcessor : GameComponent
    {
        // P R O P E R T I E S

        //references
        TimedInputHandler input;
        GameConsole console;
        // ref to the player


        //class vars
        Stack<ICommand> Commands;
        KeyMap keyMap;
        ButtonMap buttonMap;

        // C O N S T R U C T O R 
        public CommandProcessor(Game game) : base(game)
        {
            Commands = new Stack<ICommand>();
            keyMap = new KeyMap();
            buttonMap = new ButtonMap();   
            #region 'Setting Refs'
            input = (TimedInputHandler)game.Services.GetService<IInputHandler>();
            if (input == null)
            {
                input = new TimedInputHandler(game);
                game.Components.Add(input);
            }

            console = (GameConsole)game.Services.GetService<IGameConsole>();
            if (console == null)
            {
                console = new GameConsole(game);
                game.Components.Add(console);
            }
            #endregion

            // set the player references
        }

        // M E T H O D S
        public override void Update(GameTime gameTime)
        {
            HandleKeyBoard();
            HandleGamePad();
            
            base.Update(gameTime);
        }


        void HandleKeyBoard()
        {
            foreach (var item in keyMap.OnReleasedKeyMap)
            {
                if (input.KeyboardState.HasReleasedKey(item.Key))
                {
                    switch (item.Value)
                    {
                        // stuff on released key
                    }
                }
            }

            foreach (var item in keyMap.OnKeyDownMap)
            {
                if (input.KeyboardState.IsHoldingKey(item.Key))
                {
                    console.GameConsoleWrite($"onKeyDownMap key held {item.Value.ToString()}");


                    Command command = null;

                    switch (item.Value)
                    {
                        case "Action 1":
                            //trigger action 1 command
                            break;
                        case "Action 2":
                            // trigger action 2 command
                            break;
                        case "Action 3":
                            // trigger action 3 command
                            break;
                        case "Action 4":
                            // trigger action 4 command
                            break;
                    }

                    //if (command != null)
                    //command.Execute([PLAYER REF]);
                }

                if (input.KeyboardState.HasReleasedKey(item.Key))
                {
                    console.GameConsoleWrite(string.Format("onKeyDownMap Key released {0}", item.Value.ToString())); //Log key to console
                    /*switch (item.Value)
                    {
                       //nothing 
                    }*/
                }
            }
        }


        void HandleGamePad()
        {
            
            foreach (var item in buttonMap.OnReleasedButtonMap)
            {
                if (input.gamePadHandler.WasButtonPressed(0, (InputHandler.ButtonType)item.Key))
                {
                    switch (item.Value)
                    {
                        // stuff on released key
                    }
                }
            }

            foreach (var item in keyMap.OnKeyDownMap)
            {
                if (input.gamePadHandler.WasButtonPressed(0, (InputHandler.ButtonType)item.Key))
                {
                    console.GameConsoleWrite($"onKeyDownMap key held {item.Value.ToString()}");


                    Command command = null;

                    switch (item.Value)
                    {
                        case "Action 1":
                            //trigger action 1 command
                            break;
                        case "Action 2":
                            // trigger action 2 command
                            break;
                        case "Action 3":
                            // trigger action 3 command
                            break;
                        case "Action 4":
                            // trigger action 4 command
                            break;
                    }

                    //if (command != null)
                    //command.Execute([PLAYER REF]);
                }

            }
        }
    }
}
