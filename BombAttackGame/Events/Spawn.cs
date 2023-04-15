using BombAttackGame.Collisions;
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
        public static Vector2 GenerateRandomSpawnPoint(int[] MapSize, Texture2D Texture, List<Rectangle> Collision)
        {
            Random random = new Random();
            while (true)
            {
                Vector2 Location = new Vector2(random.Next(0, MapSize[0]), random.Next(0, MapSize[1]));
                Rectangle rectangle = new Rectangle((int)Location.X, (int)Location.Y, Texture.Width, Texture.Height);
                return Location;
            }
        }
    }
}
