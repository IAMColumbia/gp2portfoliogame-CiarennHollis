using BurnoutBuster.Character;
using BurnoutBuster.CommandPat.Commands;
using BurnoutBuster.Input;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System.Collections.Generic;

namespace BurnoutBuster.CommandPat
{
    class CommandProcessor : GameComponent
    {
        // P R O P E R T I E S

        //references
        TimedInputHandler input;
        GameConsole console;
        CommandCreature creatureReceiver;
        ChordAnalyzer chordAnalyzer;

        //class vars
        Stack<ICommand> Commands;
        KeyMap keyMap;
        ButtonMap buttonMap;
        float listenTime; //in milliseconds


        //enum
        enum ListeningMode { ForAnything, ForDashAttack, ForComboAttack, ForFinisherAttack }
        ListeningMode listeningMode;

        // C O N S T R U C T O R 
        public CommandProcessor(Game game, GameComponent creature) : base(game)
        {
            Commands = new Stack<ICommand>();
            keyMap = new KeyMap();
            buttonMap = new ButtonMap();
            listenTime = 350;
            #region 'Setting Refs'
            input = (TimedInputHandler)game.Services.GetService<IInputHandler>();
            if (input == null)
            {
                input = new TimedInputHandler(game);
                game.Components.Add(input);
            }

            chordAnalyzer = new ChordAnalyzer();

            console = (GameConsole)game.Services.GetService<IGameConsole>();
            if (console == null)
            {
                console = new GameConsole(game);
                game.Components.Add(console);
            }

            this.creatureReceiver = (CommandCreature)creature;  
            #endregion


        }

        // I N I T
        public override void Initialize()
        {
            chordAnalyzer.Initialize();
            base.Initialize();
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            float _time = (float)gameTime.TotalGameTime.TotalMilliseconds;

            chordAnalyzer.Update(_time);

            HandleKeyBoard(_time);
            //HandleGamePad();
            
            base.Update(gameTime);
        }

        // B U F F E R   A N D   A N A L Y Z E R
        #region 'Chord Buffer and Analyzer'
        private void UseChordAnalyzer(string buttenRef, float _time, out Command command)
        {
            chordAnalyzer.AddNote(CreateNoteBasedOnInput(buttenRef, _time));
            chordAnalyzer.CheckQueue();
            command = CreateCommandBasedOnOutputFromChordAnalyzer();
        }
        private Note CreateNoteBasedOnInput(string actionPerformed, float time) //TD RESPONSIBILITY: creating a note from the input probably should happen in the chord analyzer class, but this is the way it makes the most sense to my brain rn so it's going to stay here :P
        {
            ActionCommands actionCommand = ActionCommands.Null;
            switch (actionPerformed)
            {
                case "Heavy":
                    actionCommand = ActionCommands.HeavyAttack;
                    break;
                case "Action 2":
                    actionCommand = ActionCommands.Null;
                    break;
                case "Attack":
                    actionCommand = ActionCommands.Attack;
                    break;
                case "Dash":
                    actionCommand = ActionCommands.Dash;
                    break;
            }
            return new Note(actionCommand, time, listenTime);
        }
        private Command CreateCommandBasedOnOutputFromChordAnalyzer()
        {
            Command command = new NullCommand(this.Game);

            switch (chordAnalyzer.GetCurrentCommandOutput())
            {
                case ActionCommands.Null:
                    // do nothing
                    break;

                case ActionCommands.Attack:
                    command = new AttackCommand(this.Game);
                    break;
                case ActionCommands.HeavyAttack:
                    command = new HeavyAttackCommand(this.Game);
                    break;
                case ActionCommands.ComboAttack:
                    command = new ComboAttackCommand(this.Game);
                    break;
                case ActionCommands.FinisherAttack:
                    command = new FinisherAttackCommand(this.Game);
                    break;
                case ActionCommands.Dash:
                    command = new DashCommand(this.Game);
                    break;
                case ActionCommands.DashAttack:
                    command = new DashAttackCommand(this.Game);
                    break;
            }

            return command;
        }
        #endregion

        // K E Y B O A R D   M E T H O D S
        #region 'Keyboard Methods'
        void HandleKeyBoard(float _time)
        {
            Command command = null;

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
                    //console.GameConsoleWrite($"onKeyDownMap key held {item.Value.ToString()}");

                    switch (item.Value)
                    {
                        //movement
                        case "Up":
                            command = new MoveUpCommand(this.Game);
                            break;
                        case "Down":
                            command = new MoveDownCommand(this.Game);
                            break;
                        case "Left":
                            command = new MoveLeftCommand(this.Game);
                            break;
                        case "Right":
                            command = new MoveRightCommand(this.Game);
                            break;

                        
                    }

                    if (command != null)
                        command.Execute(creatureReceiver);
                }

                if (input.KeyboardState.HasReleasedKey(item.Key))
                {
                    //console.GameConsoleWrite(string.Format("onKeyDownMap Key released {0}", item.Value.ToString())); //Log key to console
                    switch (item.Value)
                    {
                       

                        case "Heavy":
                        case "Action 2":
                        case "Attack":
                        case "Dash":
                            //command = CreateActionCommandBasedOnListeningMode(item.Value, _time);
                            UseChordAnalyzer(item.Value, _time, out command);
                            break;
                    }
                    if (command != null)
                        command.Execute(creatureReceiver);
                }
            }

            
        }
        
        private Command CreateActionCommandBasedOnListeningMode(string buttonRef, float time)
        {
            Command command = null;
            switch (listeningMode)
            {
                case ListeningMode.ForAnything:
                    command = ForAnythingBehavior(buttonRef, time);
                    break;

                case ListeningMode.ForDashAttack:
                    command = ForDashAttackBehavior(buttonRef, time); 
                    break;

                case ListeningMode.ForComboAttack:
                    command = ForAttackComboBehavior(buttonRef, time);
                    break;

                case ListeningMode.ForFinisherAttack:
                    command = ForFinisherBehavior(buttonRef, time);
                    break;
            }
            return command;
        }
        private Command ForAnythingBehavior(string buttonRef, float time)
        {
            this.input.timer.ResetTimer(); //reset the timer
            Command command = null;
            switch(buttonRef) // instatiating command
            {
                case "Heavy":
                    command = new Commands.HeavyAttackCommand(this.Game);
                    listeningMode = ListeningMode.ForAnything;
                    break;
                case "Attack":
                    command = new Commands.AttackCommand(this.Game);
                    listeningMode = ListeningMode.ForComboAttack;
                    break;
                case "Dash":
                    command = new Commands.DashCommand(this.Game);
                    listeningMode = ListeningMode.ForDashAttack;
                    break;
            }


            this.input.timer.StartTimer(time, listenTime); // starting input timer
            return command;
        }
        private Command ForDashAttackBehavior(string buttonRef, float time)
        {
            this.input.timer.UpdateTimer(time); 
            //TD implement timer function
            Command command = null;
            switch (buttonRef) // instatiating command
            {
                case "Heavy":
                    command = new Commands.HeavyAttackCommand(this.Game);
                    listeningMode = ListeningMode.ForAnything;
                    this.input.timer.ResetTimer();
                    break;

                case "Attack":
                    if (this.input.timer.IsRunning())
                    {
                        command = new Commands.DashAttackCommand(this.Game);
                        this.input.timer.StartTimer(time, listenTime);
                    }
                    else { command = new Commands.DashCommand(this.Game); this.input.timer.ResetTimer(); }
                    listeningMode = ListeningMode.ForAnything;
                    break;

                case "Dash":
                    command = new Commands.DashCommand(this.Game);
                    listeningMode = ListeningMode.ForAnything;
                    this.input.timer.ResetTimer();
                    break;
            }
            return command;
        }
        private Command ForAttackComboBehavior(string buttonRef, float time)
        {
            this.input.timer.UpdateTimer(time);
            //TD implement timer function
            Command command = null;

            switch (buttonRef) // instatiating command
            {
                case "Heavy":
                    if (this.input.timer.IsRunning())
                    { 
                        command = new Commands.ComboAttackCommand(this.Game);
                        this.input.timer.StartTimer(time, listenTime);
                        listeningMode = ListeningMode.ForFinisherAttack; 
                    }
                    else 
                    { 
                        command = new Commands.HeavyAttackCommand(this.Game);
                        listeningMode = ListeningMode.ForAnything;
                        this.input.timer.ResetTimer();
                    }
                    break;
                case "Attack":
                    command = new Commands.AttackCommand(this.Game);
                    listeningMode = ListeningMode.ForAnything;
                    this.input.timer.ResetTimer();
                    break;
                case "Dash":
                    command = new Commands.DashCommand(this.Game);
                    listeningMode = ListeningMode.ForAnything;
                    this.input.timer.ResetTimer();
                    break;
            }
            return command;
        }
        private Command ForFinisherBehavior(string buttonRef, float time)
        {
            this.input.timer.UpdateTimer(time);
            //TD implement timer function
            Command command = null;

            switch (buttonRef) // instatiating command
            {
                case "Heavy":
                    command = new Commands.HeavyAttackCommand(this.Game);
                    listeningMode = ListeningMode.ForAnything;
                    this.input.timer.ResetTimer();
                    break;

                case "Attack":
                    if (this.input.timer.IsRunning())
                    {
                        command = new Commands.FinisherAttackCommand(this.Game);
                    }
                    else { command = new Commands.AttackCommand(this.Game); this.input.timer.ResetTimer(); }
                    listeningMode = ListeningMode.ForAnything;
                    break;

                case "Dash":
                    command = new Commands.DashCommand(this.Game);
                    listeningMode = ListeningMode.ForAnything;
                    this.input.timer.ResetTimer();
                    break;
            }
            return command;
        }
        #endregion

        // G A M E P A D   M E T H O D S  -> [TD] don't work
        #region 'Gamepad stuff'
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
                        //movement
                        case "Up":
                            // trigger move up
                            break;
                        case "Down":
                            // trigger move down
                            break;
                        case "Left":
                            // trigger move left
                            break;
                        case "Right":
                            // trigger move right
                            break;

                        // actions
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
        #endregion
    }
}
