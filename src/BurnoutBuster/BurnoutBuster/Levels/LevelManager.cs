using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Levels
{
    public class LevelManager : DrawableGameComponent
    {
        // P R O P E R T I E S
        private List<Level> _levels;
        public int CurrentLevel;

        // C O N S T R U C T O R
        public LevelManager(Game game) : base(game)
        {

        }

        // I N I T 
        public override void Initialize()
        {
            this._levels = new List<Level>();

            base.Initialize();
        }
        protected override void LoadContent()
        {
            InitializeLevels();

            base.LoadContent();
        }
        void InitializeLevels()
        {
            Level level1 =
                new Level(this.Game, 2, "Environment/PlaceHolderLevelArt");
            _levels.Add(level1);

            Level level2 =
                new Level(this.Game, 5, "Environment/PlaceHolderLevelArt");
            _levels.Add(level2);
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


        // M I S C
        public void LoadLevel(int levelIndex)
        {
            CurrentLevel = levelIndex;
            _levels[CurrentLevel].spriteTexture = 
                this.Game.Content.Load<Texture2D>(_levels[CurrentLevel].GetFilePath());

        }

        public void UnloadLevel(int levelIndex)
        {
            CurrentLevel = -1;
            _levels[levelIndex].spriteTexture = null;
            this.Game.Content.UnloadAsset(_levels[CurrentLevel].GetFilePath());
        }

        void UpdateCurrentLevel()
        {

        }

    }
}
