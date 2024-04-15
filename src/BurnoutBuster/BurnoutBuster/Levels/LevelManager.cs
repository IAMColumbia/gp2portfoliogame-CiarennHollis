using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

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
            //for VS
            CurrentLevel = 0;
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
            level1.Initialize();
            _levels.Add(level1);

            Level level2 =
                new Level(this.Game, 5, "Environment/PlaceHolderLevelArt");
            level2.Initialize();
            _levels.Add(level2);

            //TD not the best solution for this but it'l have ot do for now
            for (int i = 0; i < _levels.Count; i++)
            {
                LoadLevel(i);
            }
        }

        // U P D A T E 
        public override void Update(GameTime gameTime)
        {
            UpdateCurrentLevel(CurrentLevel);
            base.Update(gameTime);
        }

        // D R A W 
        public void Draw(GameTime gameTime, SpriteBatch sb)
        {
            sb.Begin();
            DrawCurrentLevel(CurrentLevel, sb);
            //base.Draw(gameTime);
            sb.End();
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

        void UpdateCurrentLevel(int levelIndex)
        {
            //updates enemies killed

        }

        void DrawCurrentLevel(int levelIndex, SpriteBatch sb)
        {
            _levels[levelIndex].Draw(sb);
        }

    }
}
