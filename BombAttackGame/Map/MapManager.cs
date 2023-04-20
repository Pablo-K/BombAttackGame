using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BombAttackGame.Map
{
    internal class MapManager
    {
        public char[][] MapMirageStructure { get; set; }
        public List<Vector2> MapMirageVector { get; set; }
        public Texture2D Wall { get; set; }
        public MapManager(SpriteBatch SpriteBatch, Texture2D Wall)
        {
            this.Wall = Wall;
            ReadMap("Mirage");
        }
        public SpriteBatch DrawMap(SpriteBatch SpriteBatch, List<Vector2> MapVector)
        {
            for (int i = 0; i < MapVector.Count; i++)
            {
                DrawWall(SpriteBatch, MapVector[i], Wall);
            }
            return SpriteBatch;
        }
        private void ReadMap(string Map)
        {
            switch (Map)
            {
                case "Mirage":
                    MapMirageStructure = JsonToChar(File.ReadAllText("Map/Mirage.json"));
                    MapMirageVector = MapConverter(MapMirageStructure);
                    break;
            }
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
        private List<Vector2> MapConverter(char[][] MapStructure)
        {
            List<Vector2> Map = new List<Vector2>();
            for (int i = 0; i < MapStructure.Length; i++)
            {
                for (int j = 0; j < MapStructure[i].Length; j++)
                {
                    if (MapStructure[i][j] == 'w') Map.Add(new Vector2(j * Wall.Height, i * Wall.Width));
                }
            }
            return Map;
        }
        private char[][] JsonToChar(string Json)
        {
            return JsonSerializer.Deserialize<char[][]>(Json);
        }
    }
}
