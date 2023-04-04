using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Events
{
    internal class Spawn
    {
        public static Vector2 GenerateRandomSpawnPoint(int[] mapSize, Texture2D texture)
        {
            Random random = new Random();
            return new Vector2(random.Next(mapSize[0] - texture.Width), random.Next(mapSize[1] - texture.Height));
        }
    }
}
