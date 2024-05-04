using BurnoutBuster.Character;
using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    class MonogameItem : DrawableSprite, IInteractable, IPoolable
    {

        // P R O P E R T I E S

        protected Item item;

        //collision
        public Rectangle Bounds { get; set; }
        public bool IsCollisionOn { get; set; }

        public GameComponent GameObject { get => this; }

        public Tags Tag{ get; set; }



        // C O N S T R U C T O R
        public MonogameItem(Game game) : base(game)
        {

        }

        // I N I T
        public override void Initialize()
        {
            base.Initialize();
        }

        protected override void LoadContent()
        {
            base.LoadContent();
        }

        // U P D A T E 
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        // D R A W
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }

        // C O L L I S I O N
        public virtual void OnCollisionEnter(Collision collision)
        {
            if (collision != null)
            {

            }
        }

        public virtual void OnInteraction(IInteract subject)
        {
            if (subject != null)
            {

            }
        }

        // I T E M   A C T I O N S
        public virtual void Use()
        {
            this.item.Use();
        }
        public virtual void Use(IDamageable target)
        {
            this.item.Use(target);
        }

        // I P O O L A B L E
        public void Reset()
        {
            this.IsCollisionOn = false;
            this.Enabled = false;
        }

        public void Activate(Vector2 spawnLocation)
        {
            this.IsCollisionOn = true;
            this.Enabled = true;
        }


    }
}
