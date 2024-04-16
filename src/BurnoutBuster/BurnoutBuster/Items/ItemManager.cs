using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System.Collections.Generic;

namespace BurnoutBuster.Items
{
    class ItemManager : GameComponent, IObserver
    {
        // P R O P E R T I E S
        List<MonogameWeapon> Weapons;
        Vector2 spawnLocation;

        // C O N S T R U C T O R
        public ItemManager(Game game) : base(game)
        {
            Weapons = new List<MonogameWeapon>();
            spawnLocation = new Vector2(750, 200);
        }

        // I N I T 
        public override void Initialize()
        {
            LoadWeapons();
            base.Initialize();
        }
        private void LoadWeapons()
        {
            MonogameWeapon newWeapon = new MonogameGoldSword(this.Game);
            Weapons.Add(newWeapon);
            this.Game.Components.Add(newWeapon);
            newWeapon.Enabled = false;
        }
        public List<MonogameWeapon> GetAllWeapons()
        {
            return Weapons;
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
        }

        public void UpdateObserver()
        {
            SpawnItem(Weapons[0]);
        }

        // S P A W N I N G
        private void SpawnItem(DrawableSprite item)
        {
            item.Location = spawnLocation;
            item.Enabled = true;
        }
    }
}
