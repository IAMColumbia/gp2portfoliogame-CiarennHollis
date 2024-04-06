﻿using BurnoutBuster.Collision;
using BurnoutBuster.Input;
using BurnoutBuster.Items;
using BurnoutBuster.UI;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;
using MonoGame.Extended.Collisions;
using MonoGameLibrary.Sprite;
using MonoGameLibrary.Util;
using ICollidable = BurnoutBuster.Collision.ICollidable;

namespace BurnoutBuster.Character
{
    public class MonogameCreature : DrawableSprite, IDamageable, ICollidable, IFlashableTexture
    {
        // P R O P E R T I E S

        //REFERENCES
        protected GameConsole console;
        TimedPlayerController controller { get; set; }
        internal GameConsoleCreature creature
        {
            get;
            private set;
        }

        //STATE
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

        //STATS
        public int HitPoints
        {
            get { return this.creature.HitPoints; }
            set 
            {
                this.creature.HitPoints = value; 
            }
        }

        //ATTACKING
        public IWeapon Weapon
        {
            get { return this.creature.MyWeapon; }
        }

        //COLLISION AND TAG
        public Rectangle Bounds { get; set; }
        public Tags Tag { get; }
        public GameComponent GameObject { get; private set; }
        protected Vector2 moveVector;

        //IFLASHABLE
        public Color flashColor { get => Color.Black; }
        public bool canStartFlashing { get; set; }
        public FlashingState flashingState { get; set; }
        public Timer flashingTimer { get; set; }
        public Timer individualFlashTimer { get; set; }

        // C O N S T R U C T O R
        //DEPENDENCY FOR POC: enemy
        public MonogameCreature(Game game) : base(game)
        {
            //setting refs
            this.controller = new TimedPlayerController(game);
            this.console = (GameConsole)game.Services.GetService<IGameConsole>();
            if (this.console == null)
            {
                this.console = new GameConsole(game);
                this.Game.Components.Add(this.console);
            }
            creature = new GameConsoleCreature((GameConsole)game.Services.GetService<IGameConsole>());
            
            //collision
            GameObject = this;
            this.Tag = Tags.Player;

            //damage flashing
            canStartFlashing = false;
            flashingState = FlashingState.NotFlashing;
            flashingTimer = new Timer();
            individualFlashTimer = new Timer();
        }

        // I N I T
        protected override void LoadContent()
        {
            this.SpriteTexture = this.Game.Content.Load<Texture2D>("CharacterSprites/creature");
            this.Origin = new Vector2(this.SpriteTexture.Width / 2, this.SpriteTexture.Height / 2);
            this.Location = new Microsoft.Xna.Framework.Vector2(100, 100);

            this.ShowMarkers = true;

            this.Speed = 150;

            base.LoadContent();
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            float timeElapsed = (float) gameTime.ElapsedGameTime.TotalMilliseconds;
            float timeTotal = (float)gameTime.TotalGameTime.TotalMilliseconds;
            KeepCreatureOnScreen();

            //collision
            UpdateBounds();

            //state
            UpdateStateBasedOnHP();

            //flashing
            HandleFlash(flashColor, timeTotal);

            base.Update(gameTime);
        }

        protected virtual void UpdateCreatureWithController(GameTime gameTime, float time)
        {
            this.controller.Update(gameTime);

            this.Location += ((this.controller.Direction * (time / 1000)) * Speed); // Simple move

        }

        // D R A W 
        #region 'Draw'
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
        #endregion

        // C O L L I S I O N
        public virtual void OnCollisionEnter(Collision.Collision collision)
        {
            if (collision != null)
            {
                if (TagManager.CompareTag(collision.OtherObject, Tags.Enemy))
                {
                    //console.GameConsoleWrite("Collided with an Enemy");
                }
            }
        }
        private void UpdateBounds()
        {
            Bounds = this.Rectangle;
        }


        // S T A T E
        public bool CheckCreatureState(CreatureState state)
        {
            if (CreatureState == state)
                return true;

            return false;
        }
        private void UpdateStateBasedOnHP()
        {
            if (HitPoints <= 0)
                this.CreatureState = CreatureState.Shutdown;
        }

        // I D A M A G A B L E
        public void Attack(IDamageable target)
        {
            creature.Attack(target);
        }
        protected virtual void OnCreatureStateChanged()
        {
            // logic for what happens when the creature state changes
        }
        public virtual void KnockBack(Vector2 knockbackVector)
        {
            this.Location -= knockbackVector;
        }

        public void Hit(int damageAmount)
        {
            this.creature.Hit(damageAmount);
            console.GameConsoleWrite($"PL HP = {HitPoints}");
            canStartFlashing = true;
            //this.KnockBack();
            //this.creature.KnockBack();
        }

        // T E X T U R E   E F F E C T S
        #region 'Texture effects'
        public void HandleFlash(Color color, float time)
        {
            // CAN WE START FLASHING 
            if (canStartFlashing)
            {
                this.flashingState = FlashingState.FlashingColor;
                this.flashingTimer.StartTimer(time, 1000); // TD hard coded time length
                canStartFlashing = false;
            }

            // ARE WE STILL FLASHING?
            this.flashingTimer.UpdateTimer(time);

            if (flashingTimer.State == TimerState.Off
                || flashingTimer.State == TimerState.Ended)
            {
                flashingState = FlashingState.NotFlashing;
                this.DrawColor = Color.White;
            }


            // FLASHING BEHAVIOR
            if (flashingState != FlashingState.NotFlashing)
            {
                //what color are we supposed to be if we are flashing?
                this.individualFlashTimer.UpdateTimer(time);

                if (individualFlashTimer.State == TimerState.Off
                    || individualFlashTimer.State == TimerState.Ended) // if timer has ended, switch colors
                {
                    //update the flashing state 
                    if (flashingState == FlashingState.NormalColor)
                    {
                        flashingState = FlashingState.FlashingColor;
                    }
                    else if (flashingState == FlashingState.FlashingColor)
                    {
                        flashingState = FlashingState.NormalColor;
                    }

                    // update the draw color based on the flashing 
                    switch (flashingState)
                    {
                        case FlashingState.NormalColor:
                            this.DrawColor = Color.White;
                            break;

                        case FlashingState.FlashingColor:
                            this.DrawColor = color;
                            break;
                    }

                    individualFlashTimer.StartTimer(time, 100); // in ms
                }

            }
        }
        #endregion


    }
}
