using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGameLibrary.Sprite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BurnoutBuster.Levels
{
    public class Level : Sprite
    {
        // P R O P E R T I E S
        // art - texture2D
        private int NumOfEnemiesToClear;
        private int NumOfEnemiesKilled;
        private string textureFilePath;

        // C O N S T R U C T O R
        public Level(Game game, int numOfEnemies, string levelArtPath) : base(game)
        {
            textureFilePath = levelArtPath;
            NumOfEnemiesToClear = numOfEnemies;
        }

        // M E T H O D S
        public string GetFilePath()
        {
            return textureFilePath;
        }
        public void IncreaseEnemiesKilled()
        {
            NumOfEnemiesKilled++;
        }

        public bool HasRoundEnded()
        {
            if (NumOfEnemiesKilled >= NumOfEnemiesToClear)
                return true;

            return false;
        }

    }
}
