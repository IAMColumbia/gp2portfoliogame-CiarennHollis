using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Sprite;
using System.Collections.Generic;

namespace BurnoutBuster.Items
{
    class ItemManager : DrawableGameComponent, IObserver
    {
        // P R O P E R T I E S
        List<MonogameWeapon> Weapons;
        List<MonogameItem> Items;
        Vector2 spawnLocation;

        // C O N S T R U C T O R
        public ItemManager(Game game) : base(game)
        {
            Weapons = new List<MonogameWeapon>();
            Items = new List<MonogameItem>();
            spawnLocation = new Vector2(750, 200);
        }

        // I N I T 
        public override void Initialize()
        {
            LoadWeapons();
            LoadItems(5);
            base.Initialize();
        }
        private void LoadWeapons()
        {
            MonogameWeapon newWeapon = new MonogameGoldSword(this.Game);
            Weapons.Add(newWeapon);
            this.Game.Components.Add(newWeapon);
            newWeapon.Enabled = false;
        }
        private void LoadItems(int amount)
        {
            for (int i = 0; i < amount; i++)
            {
                this.Items.Add(new MonogameHealthPU(this.Game, 5 + 1));
            }
        }
        public List<MonogameWeapon> GetAllWeapons()
        {
            return Weapons;
        }
        public List<MonogameItem> GetAllItems()
        {
            return Items;
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            //UpdateItems(gameTime);
            base.Update(gameTime);
        }

        public void UpdateObserver()
        {
            SpawnItem(Weapons[0]);
        }

        private void UpdateItems(GameTime gameTime)
        {
            foreach (MonogameItem item in Items)
            {
                if (item.Enabled)
                    item.Update(gameTime);
            }
        }

        // D R A W
        public override void Draw(GameTime gameTime)
        {
            //foreach (MonogameItem item in Items)
            //{
            //    if (item.Enabled)
            //        item.Draw(gameTime);
            //}

            base.Draw(gameTime);
        }

        // S P A W N I N G
        private void SpawnItem(DrawableSprite item)
        {
            item.Location = spawnLocation;
            item.Enabled = true;
        }
    }
}
