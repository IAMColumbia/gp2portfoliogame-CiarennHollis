using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
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
        Timer spawnDelayTimer;
        float delayAmount;
        bool canRestartSpawnDelayTimer;

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
            spawnLocation = new Vector2(100, 200);
            NumberOfEnemiesToSpawn = 2;

            canRestartSpawnDelayTimer = false;
            delayAmount = 15000;
        }

        // I N I T
        public override void Initialize()
        {
            PopulateAllEnemiesList(NumberOfEnemiesToSpawn);
            InitializeEnemies();

            this.spawnDelayTimer = new Timer();

            base.Initialize();
        }
        void PopulateAllEnemiesList(int numberOfEachEnemyToCreate)
        {
            for (int i = 0; i <= numberOfEachEnemyToCreate; i++)
            {
                MonogameEnemy enemy = new BasicEnemy(this.Game, creature);
                enemy.Reset();
                AllEnemies.Add(enemy);
                
                enemy = new HeavyEnemy(this.Game, creature);
                enemy.Reset();
                AllEnemies.Add(enemy);

                enemy = new KamikaziEnemy(this.Game, creature);
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
        public void AddEnemiesToCollisionManager(CollisionManager collisionManager)
        {
            foreach (MonogameEnemy enemy in AllEnemies)
                collisionManager.AddObject(enemy);
        }


        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            float totalTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            CheckEnemies();
            UpdateEnemies(gameTime);


            HandleSpawnDelayTimer(totalTime);
            HandleSpawningEnemies();

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

        // S P A W N I N G   L O G I C
        void CheckEnemies()
        {
            foreach(MonogameEnemy enemy in ActiveEnemies)
            {
                if (enemy.EnemyState == EnemyState.Dead
                    || enemy.EnemyState == EnemyState.Inactive)
                    this.tempEnemies.Add(enemy);
            }

            foreach(MonogameEnemy enemy in tempEnemies)
            {
                this.ActiveEnemies.Remove(enemy);
                enemy.Reset();
            }
            tempEnemies.Clear();
        }

        void SpawnAnEnemy()
        {
            int i;
            i = rand.Next(0, AllEnemies.Count);
            if (AllEnemies[i].EnemyState != EnemyState.Inactive)
                SpawnAnEnemy();
            AllEnemies[i].Activate(spawnLocation * i);
            ActiveEnemies.Add(AllEnemies[i]);
        }
        public void SpawnMultipleEnemies(int numOfEnemiesToSpawn)
        {
            for (int i = 0; i < numOfEnemiesToSpawn; i++)
                SpawnAnEnemy();
        }

        void SpawnMoreIfNoneActive()
        {
            if (ActiveEnemies.Count == 0)
                SpawnMultipleEnemies(NumberOfEnemiesToSpawn);
        }
        bool AreThereActiveEnemies()
        {
            if (ActiveEnemies.Count == 0)
                return false;

            return true;
        }

        // T I M E R   M A N A G E M E N T
        void HandleSpawningEnemies()
        {
            if (spawnDelayTimer.State == TimerState.Off
                || spawnDelayTimer.State == TimerState.Ended
                || !AreThereActiveEnemies())
            {
                SpawnMultipleEnemies(NumberOfEnemiesToSpawn);
                canRestartSpawnDelayTimer = true;
                NumberOfEnemiesToSpawn++;
            }
        }
        void HandleSpawnDelayTimer(float currentTime)
        {
            if (canRestartSpawnDelayTimer)
            {
                spawnDelayTimer.StartTimer(currentTime, delayAmount);
                canRestartSpawnDelayTimer = false;
            }

            spawnDelayTimer.UpdateTimer(currentTime);
        }

        // R E S E T
        public void Reset()
        {
            this.NumberOfEnemiesToSpawn = 0;
            this.ActiveEnemies.Clear();
            this.tempEnemies.Clear();
            spawnDelayTimer.ResetTimer();
        }
    }
}
