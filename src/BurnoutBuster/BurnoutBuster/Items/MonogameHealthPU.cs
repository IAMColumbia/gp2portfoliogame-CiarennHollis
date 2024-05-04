using BurnoutBuster.Character;
using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Items
{
    class MonogameHealthPU : MonogameItem 
    {
        // P R O P E R T I E S
        protected HealthPickUp healthPickUp { get; set; }

        // C O N S T R U C T O R
        public MonogameHealthPU(Game game, int healthIncrease) : base(game)
        {
            this.Tag = Utility.Tags.Health;
            healthPickUp = new HealthPickUp("Health", healthIncrease);
        }

        // I N I T
        public override void Initialize()
        {
            base.Initialize();
        }
        protected override void LoadContent()
        {
            this.spriteTexture = Game.Content.Load<Texture2D>("Items/GoldSword");
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
        public override void OnCollisionEnter(Collision collision)
        {
            base.OnCollisionEnter(collision);
        }
        public override void OnInteraction(IInteract subject)
        {
            ITaggable tagSubject = subject as ITaggable;
            
            if (tagSubject != null)
            {
                if (TagManager.CompareTag(tagSubject, Tags.Player))
                {
                    IDamageable damageableSubject = subject as IDamageable;
                    if (damageableSubject != null)
                    {
                        this.Use(damageableSubject);
                    }
                    this.Enabled = false;
                }
            }
            base.OnInteraction(subject);
        }

        // I T E M   A C T I O N S
        public override void Use(IDamageable target)
        {
            healthPickUp.Use(target);
            //base.Use(target);
        }
    }
}
