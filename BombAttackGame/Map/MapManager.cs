using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Map
{
    internal class MapManager
    {
        public static SpriteBatch Map1(SpriteBatch SpriteBatch, Texture2D Texture)
        {
            MapManager.DrawAroundMap(SpriteBatch, Texture);
            return SpriteBatch;
        }
        public static SpriteBatch DrawWall(SpriteBatch SpriteBatch, Vector2 Position, Texture2D Texture)
        {
            SpriteBatch.Draw(
                texture: Texture,
                position: Position,
                sourceRectangle: null,
                color: Color.BlanchedAlmond,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: new Vector2(1, 1),
                effects: SpriteEffects.None,
                layerDepth: 0f);
            return SpriteBatch;
        }
        public static void DrawYLine(SpriteBatch SpriteBatch, Texture2D Texture, int Y)
        {
            int[] MapSize = new int[2];
            MapSize[0] = 1000;
            MapSize[1] = 1000;
            Y = Y * Texture.Height;
            for (int i = 0; i < MapSize[0]; i+= 20)
            {
                MapManager.DrawWall(SpriteBatch, new Vector2(Y, i), Texture);
            }
        }
        public static void DrawXLine(SpriteBatch SpriteBatch, Texture2D Texture, int X)
        {
            int[] MapSize = new int[2];
            MapSize[0] = 1000;
            MapSize[1] = 1000;
            X = X * Texture.Width;
            for (int i = 0; i < MapSize[0]; i+= Texture.Width)
            {
                DrawWall(SpriteBatch, new Vector2(i, X), Texture);
            }
        }
        public static void DrawYLineFromTo(SpriteBatch SpriteBatch, Texture2D Texture, int Y, int From, int To)
        {
            int[] MapSize = new int[2];
            MapSize[0] = 1000;
            MapSize[1] = 1000;
            Y *= Texture.Height;
            From *= Texture.Height;
            To *= Texture.Height;
            for (int i = From; i < To; i+= Texture.Height)
            {
                DrawWall(SpriteBatch, new Vector2(Y, i), Texture);
            }
        }
        public static void DrawXLineFromTo(SpriteBatch SpriteBatch, Texture2D Texture, int X, int From, int To)
        {
            int[] MapSize = new int[2];
            MapSize[0] = 1000;
            MapSize[1] = 1000;
            X *= Texture.Width;
            From *= Texture.Width;
            To *= Texture.Width;
            for (int i = From; i < To; i+= Texture.Width)
            {
                DrawWall(SpriteBatch, new Vector2(i, X), Texture);
            }
        }
        public static void DrawYLineFromToWithout(SpriteBatch SpriteBatch, Texture2D Texture, int Y, int From, int To, int[] Without)
        {
            int[] MapSize = new int[2];
            MapSize[0] = 1000;
            MapSize[1] = 1000;
            Y *= Texture.Height;
            From *= Texture.Height;
            To *= Texture.Height;
            for (int i = 0; i < Without.Length; i++)
            {
                Without.SetValue(Without.ElementAt(i) * Texture.Height, i);
            }
            for (int i = From; i < To; i+= Texture.Height)
            {
                if(!Without.Contains(i)) DrawWall(SpriteBatch, new Vector2(Y, i), Texture);
            }
        }
        public static void DrawXLineFromToWithout(SpriteBatch SpriteBatch, Texture2D Texture, int X, int From, int To, int[] Without)
        {
            int[] MapSize = new int[2];
            MapSize[0] = 1000;
            MapSize[1] = 1000;
            X *= Texture.Width;
            From *= Texture.Width;
            To *= Texture.Width;
            for (int i = 0; i < Without.Length; i++)
            {
                Without.SetValue(Without.ElementAt(i) * Texture.Width, i);
            }
            for (int i = From; i < To; i+= Texture.Width)
            {
                if(!Without.Contains(i)) DrawWall(SpriteBatch, new Vector2(i, X), Texture);
            }
        }
        public static void DrawAroundMap(SpriteBatch SpriteBatch, Texture2D Texture)
        {
            int[] MapSize = new int[2];
            MapSize[0] = 1000;
            MapSize[1] = 1000;

            for (int i = 0; i < MapSize[0]; i += 20)
            {
                MapManager.DrawWall(SpriteBatch, new Vector2(0, i), Texture);
                MapManager.DrawWall(SpriteBatch, new Vector2(MapSize[0]-20, i), Texture);
                MapManager.DrawWall(SpriteBatch, new Vector2(i, 0), Texture);
                MapManager.DrawWall(SpriteBatch, new Vector2(i, MapSize[0]-20), Texture);
            }
        }
    }
}
