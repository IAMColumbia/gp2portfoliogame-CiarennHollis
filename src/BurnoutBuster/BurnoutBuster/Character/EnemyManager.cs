using BurnoutBuster.Collision;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace BurnoutBuster.Character
{
    public class EnemyManager : DrawableGameComponent
    {
        // P R O P E R T I E S
        //object pool management
        public List<MonogameEnemy> AllEnemies;
        public List<MonogameEnemy> ActiveEnemies;
        private List<MonogameEnemy> tempEnemies;

        //spawn management
        Vector2 spawnLocation;

        //random
        Random rand;
        public int NumberOfEnemiesToSpawn;

        //references
        MonogameCreature creature;

        // C O N S T R U C T O R
        public EnemyManager(Game game, Random rand, MonogameCreature creature) : base(game)
        {
            AllEnemies = new List<MonogameEnemy>();
            ActiveEnemies = new List<MonogameEnemy>();
            tempEnemies = new List<MonogameEnemy>();

            this.creature = creature;

            this.rand = rand;
            spawnLocation = new Vector2(400, 400);
            NumberOfEnemiesToSpawn = 2;
        }

        // I N I T
        public override void Initialize()
        {
            PopulateAllEnemiesList(NumberOfEnemiesToSpawn);
            InitializeEnemies();

            base.Initialize();
        }
        void PopulateAllEnemiesList(int numberOfEachEnemyToCreate)
        {
            for (int i = 0; i <= numberOfEachEnemyToCreate; i++)
            {
                MonogameEnemy enemy = new BasicEnemy(this.Game, creature);
                enemy.Reset();
                AllEnemies.Add(enemy);
            }
        }
        void InitializeEnemies()
        {
            foreach(MonogameEnemy enemy in AllEnemies) // initialize all the enemies
            {
                enemy.Initialize();
            }
        }

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            CheckEnemies();
            UpdateEnemies(gameTime);
            base.Update(gameTime);
        }
        void UpdateEnemies(GameTime gameTime)
        {
            foreach (MonogameEnemy enemy in ActiveEnemies) // only update the active enemies
            {
                enemy.Update(gameTime);
            }
        }

        // D R A W
        public override void Draw(GameTime gameTime)
        {
            DrawEnemies(gameTime);
            base.Draw(gameTime);
        }
        void DrawEnemies(GameTime gameTime) // only draw the active enemies
        {
            foreach (MonogameEnemy enemy in ActiveEnemies)
            {
                enemy.Draw(gameTime);
            }
        }

        // M I S C   M E T H O D S
        public void AddEnemiesToCollisionManager(CollisionManager collisionManager)
        {
            foreach (MonogameEnemy enemy in AllEnemies)
                collisionManager.AddObject(enemy);
        }
        void CheckEnemies()
        {
            foreach(MonogameEnemy enemy in ActiveEnemies)
            {
                if (enemy.EnemyState == EnemyState.Dead)
                    this.tempEnemies.Add(enemy);
            }

            foreach(MonogameEnemy enemy in tempEnemies)
            {
                this.ActiveEnemies.Remove(enemy);
                enemy.Reset();
            }
            tempEnemies.Clear();
        }

        int i = 0;
        void SpawnAnEnemy()
        {
            i = rand.Next(0, AllEnemies.Count);
            if (AllEnemies[i].EnemyState != EnemyState.InActive)
                SpawnAnEnemy();
            AllEnemies[i].Activate(spawnLocation);
            ActiveEnemies.Add(AllEnemies[i]);
        }
        public void SpawnLevelEnemies()
        {
            for (int i = 0; i < NumberOfEnemiesToSpawn; i++)
                SpawnAnEnemy();
        }
    }
}
