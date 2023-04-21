using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

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
                foreach (Rectangle r in Collision)
                {
                    if (InHitBox(new Vector2(Location.X, Location.Y), Texture, r))
                        return new Vector2();
                    return Location;
                }
            }
        }
        private static bool InHitBox(Vector2 Location, Texture2D Texture, Rectangle Rectangle)
        {
            Vector2 positionDifference = Location - new Vector2(Rectangle.X, Rectangle.Y);
            if (positionDifference.X < 0 || positionDifference.Y < 0 ||
    positionDifference.X + 20 > Texture.Width ||
    positionDifference.Y + 20 > Texture.Height)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
