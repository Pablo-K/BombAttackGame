using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BombAttackGame.Map
{
    internal class MapManager
    {
        public static Texture2D Wall { get; set; }
        public Mirage Mirage { get; set; }
        public MapManager(SpriteBatch SpriteBatch, Texture2D Wall)
        {
            MapManager.Wall = Wall;
            this.Mirage = new Mirage();
        }
        public SpriteBatch DrawMap(SpriteBatch SpriteBatch, List<Vector2> MapVector)
        {
            for (int i = 0; i < MapVector.Count; i++)
            {
                DrawWall(SpriteBatch, MapVector[i], Wall);
            }
            return SpriteBatch;
        }
        private SpriteBatch DrawWall(SpriteBatch SpriteBatch, Vector2 Position, Texture2D Texture)
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
        public static Rectangle WallRectangle(Vector2 Location)
        {
            return new Rectangle((int)Location.X, (int)Location.Y, MapManager.Wall.Width, MapManager.Wall.Height);
        }
        public static List<Rectangle> WallRectangle(List<Vector2> Location)
        {
            List<Rectangle> Rectangles = new List<Rectangle>();
            foreach (Vector2 Loc in Location)
            {
                Rectangles.Add(new Rectangle((int)Loc.X, (int)Loc.Y, MapManager.Wall.Width, MapManager.Wall.Height));
            }
            return Rectangles;
        }
        public static List<Vector2> MapConverter(char[][] MapStructure, string Model)
        {
            List<Vector2> Map = new List<Vector2>();
            for (int i = 0; i < MapStructure.Length; i++)
            {
                for (int j = 0; j < MapStructure[i].Length; j++)
                {
                    if(Model.Contains("w")) if (MapStructure[i][j] == 'w') Map.Add(new Vector2(j * MapManager.Wall.Height, i * MapManager.Wall.Width));
                }
            }
            return Map;
        }
        public static char[][] JsonToChar(string Json)
        {
            return JsonSerializer.Deserialize<char[][]>(Json);
        }
    }
}
