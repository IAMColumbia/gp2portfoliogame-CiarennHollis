using BurnoutBuster.Physics;
using BurnoutBuster.Utility;
using Microsoft.Xna.Framework;
using MonoGameLibrary.Util;
using System;
using System.Collections.Generic;

namespace BurnoutBuster.Character
{
    public enum WaveState { Stopped, Approaching, Besieging, Cleared }
    public class EnemyManager : DrawableGameComponent, ISubject
    {
        // P R O P E R T I E S
        #region 'Properties'
        //object pool management
        public List<MonogameEnemy> AllEnemies;
        public List<MonogameEnemy> ActiveEnemies;
        private List<MonogameEnemy> tempEnemies;

        //spawn management
        Vector2 spawnLocation;
        Timer spawnDelayTimer;
        float delayAmount;
        bool canRestartSpawnDelayTimer;
        public int NumberOfEnemiesToSpawn;

        //wave management
        private int totalEnemiesSpawnedDuringWave;
        public int NumberOfEnemiesPerWave;
        public int EnemiesLeftInWave 
        { 
            get
            {
                if (NumberOfEnemiesPerWave - totalEnemiesSpawnedDuringWave < 0)
                    return 0;

                return NumberOfEnemiesPerWave - totalEnemiesSpawnedDuringWave;
            }
        }
        private int waveCounter;
        public int WaveCounter
        {
            get { return waveCounter; }
            set
            {
                if ((waveCounter >= 2))
                    this.Notify();
                waveCounter = value;
            }
        }
        public WaveState WaveState;
        private Timer waveDelayTimer;
        private float waveDelayDuration;

        //random
        Random rand;

        //references
        MonogameCreature creature;
        GameConsole console;

        //isubject
        public List<IObserver> creatureObservers { get; set; }
        #endregion

        // C O N S T R U C T O R
        public EnemyManager(Game game, Random rand, MonogameCreature creature) : base(game)
        {
            //enemy lists
            AllEnemies = new List<MonogameEnemy>();
            ActiveEnemies = new List<MonogameEnemy>();
            tempEnemies = new List<MonogameEnemy>();

            //ref
            this.creature = creature; 

            this.console = (GameConsole)game.Services.GetService<IGameConsole>();
            if (this.console == null)
            {
                this.console = new GameConsole(game);
                this.Game.Components.Add(this.console);
            }

            //spawning
            this.rand = rand;
            spawnLocation = new Vector2(5, 12);
            NumberOfEnemiesToSpawn = 2;

            canRestartSpawnDelayTimer = false;
            delayAmount = 15000;

            //waves
            WaveCounter = 1;
            NumberOfEnemiesPerWave = 5;
            totalEnemiesSpawnedDuringWave = 0;
            WaveState = WaveState.Stopped;
            waveDelayTimer = new Timer();
            waveDelayDuration = 5000;

            //isubject
            creatureObservers = new List<IObserver>();
        }

        // I N I T
        #region 'Init'
        public override void Initialize()
        {

            PopulateAllEnemiesList(20); //TD hard coded number
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
        #endregion

        // U P D A T E
        public override void Update(GameTime gameTime)
        {
            float totalTime = (float)gameTime.TotalGameTime.TotalMilliseconds;

            CheckEnemies();
            UpdateEnemies(gameTime);
            
            UpdateBasedOnState(totalTime);

            base.Update(gameTime);
        }
        private void UpdateEnemies(GameTime gameTime)
        {
            foreach (MonogameEnemy enemy in ActiveEnemies) // only update the active enemies
            {
                enemy.Update(gameTime);
            }
        }

        // D R A W
        #region 'Draw'
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
        #endregion 

        // S P A W N I N G   M A N A G E M E N T
        #region 'SpawningManagement'
        private void CheckEnemies()
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

        private void SpawnAnEnemy()
        {
            int loopRuns = 0;
            int i;
            i = rand.Next(0, AllEnemies.Count);


            if (AllEnemies[i].EnemyState != EnemyState.Inactive
                || loopRuns >= 10) 
                // if the enemy is not inactive (ie, if it's active)
                //spawn another enemy
            {
                loopRuns++;
                SpawnAnEnemy();
            }
            else if (loopRuns >= 10) //breaks the loop if we've been through it several times
            {
                console.GameConsoleWrite("Not Enough enemies in the object pool. Please add more.");
            }

            //for setting the location of enemy
            if (i >= 75) //[TD] hard coded number
                i = rand.Next(1, 75);
            AllEnemies[i].Activate(spawnLocation * i);
            ActiveEnemies.Add(AllEnemies[i]);
        }
        public void SpawnMultipleEnemies(int numOfEnemiesToSpawn)
        {
            for (int i = 0; i < numOfEnemiesToSpawn; i++)
                SpawnAnEnemy();

            totalEnemiesSpawnedDuringWave += numOfEnemiesToSpawn;
        }

        private void SpawnMoreIfNoneActive()
        {
            if (ActiveEnemies.Count == 0)
                SpawnMultipleEnemies(NumberOfEnemiesToSpawn);
        }
        private bool AreThereActiveEnemies()
        {
            if (ActiveEnemies.Count == 0)
                return false;

            return true;
        }
        #endregion

        // W A V E   M A N A G E M E N T
        #region 'Wave Management'
        private void UpdateBasedOnState(float totalTime)
        {
            switch (WaveState)
            {
                case WaveState.Approaching:
                    waveDelayTimer.ResetTimer();  
                    WaveState = WaveState.Besieging;
                    break;

                case WaveState.Besieging:
                    HandleSpawnDelayTimer(totalTime);
                    HandleSpawningEnemies();
                    OnWaveEnd(totalTime);
                    break;

                case WaveState.Cleared:
                    HandleWaveDelay(totalTime);
                    break;

                case WaveState.Stopped:
                    WaveState = WaveState.Approaching;
                    break;
            }
        }
        private bool HasWaveEnded()
        {
            if (totalEnemiesSpawnedDuringWave >= NumberOfEnemiesPerWave)
                return true;

            return false;
        }
        private void OnWaveEnd(float totalTime)
        {
            if (HasWaveEnded())
            {
                ResetForNewWave();
                WaveCounter++;
                NumberOfEnemiesPerWave *= 2;
                waveDelayTimer.StartTimer(totalTime, waveDelayDuration);
                this.WaveState = WaveState.Cleared; 
            }
        }
        #endregion

        // T I M E R   M A N A G E M E N T
        #region 'Timer Management'
        private void HandleSpawningEnemies()
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
        private void HandleSpawnDelayTimer(float currentTime)
        {
            if (canRestartSpawnDelayTimer)
            {
                spawnDelayTimer.StartTimer(currentTime, delayAmount);
                canRestartSpawnDelayTimer = false;
            }

            spawnDelayTimer.UpdateTimer(currentTime);
        }

        private void HandleWaveDelay(float totalTime)
        {
            waveDelayTimer.UpdateTimer(totalTime);

            if (waveDelayTimer.State == TimerState.Ended)
                this.WaveState = WaveState.Approaching;
        }
        #endregion


        // I S U B J E C T
        #region 'ISubject'
        public void Attach(IObserver observer)
        {
            creatureObservers.Add(observer);
        }
        public void Detach(IObserver observer)
        {
            creatureObservers.Remove(observer);
        }
        public void Notify()
        {
            foreach(IObserver observer in  creatureObservers)
            {
                observer.UpdateObserver();
            }
        }
        #endregion

        // R E S E T I N G
        #region 'Resets'
        void ResetForNewWave()
        {
            this.NumberOfEnemiesToSpawn = 2;
            totalEnemiesSpawnedDuringWave = 0;

            ResetAllEnemies();

            spawnLocation = new Vector2(5, 12);

            spawnDelayTimer.ResetTimer();
        }
        private void ResetAllEnemies()
        {
            foreach (MonogameEnemy enemy in ActiveEnemies)
            {
                enemy.Reset();
            }
            foreach (MonogameEnemy enemy in tempEnemies)
            {
                enemy.Reset();
            }
            this.ActiveEnemies.Clear();
            this.tempEnemies.Clear();
        }
        public void ResetForNewGame()
        {
            ResetForNewWave();
            WaveCounter = 1;
            NumberOfEnemiesPerWave = 5;
        }
        #endregion
    }
}
