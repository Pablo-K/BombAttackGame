using BombAttackGame.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace BombAttackGame.Map
{
    internal class MapManager
    {
        public static Texture2D Wall => ContentContainer.WallTexture;
        public static Texture2D Water => ContentContainer.WaterTexture;
        public static Texture2D Ground => ContentContainer.GroundTexture;
        public char[][] CharMap { get; set; } 
        public List<Rectangle> MapCollisions { get; set; }
        public List<Vector2> WallVectors { get; set; }
        public List<Vector2> GroundVectors { get; set; }
        public List<Vector2> ABombSiteVectors { get; set; }
        public List<Vector2> BBombSiteVectors { get; set; }
        public List<Vector2> CTSpawnVectors { get; set; }
        public List<Vector2> TTSpawnVectors { get; set; }

        public MapManager() { }
        public void LoadMap()
        {
            this.CharMap = MapManager.JsonToChar(File.ReadAllText("Map/mirage.json"));
            this.WallVectors = new List<Vector2>();
            this.MapCollisions = new List<Rectangle>();
            this.GroundVectors = new List<Vector2>();
            this.ABombSiteVectors = new List<Vector2>();
            this.BBombSiteVectors = new List<Vector2>();
            this.CTSpawnVectors = new List<Vector2>();
            this.TTSpawnVectors = new List<Vector2>();
            MapConverter(this.CharMap);
        }

        private Rectangle TextureRectangle(Vector2 Location)
        {
            return new Rectangle((int)Location.X, (int)Location.Y, MapManager.Wall.Width, MapManager.Wall.Height);
        }
        private List<Rectangle> TextureRectangle(List<Vector2> Location)
        {
            List<Rectangle> Rectangles = new List<Rectangle>();
            foreach (Vector2 Loc in Location)
            {
                Rectangles.Add(new Rectangle((int)Loc.X, (int)Loc.Y, MapManager.Wall.Width, MapManager.Wall.Height));
            }
            return Rectangles;
        }
        private void MapConverter(char[][] MapStructure)
        {
            for (int i = 0; i < MapStructure.Length; i++)
            {
                for (int j = 0; j < MapStructure[i].Length; j++)
                {
                    if (MapStructure[i][j] == 'W')
                    {
                        WallVectors.Add(new Vector2(j * MapManager.Wall.Width, i * MapManager.Wall.Height));
                        MapCollisions.Add(TextureRectangle(new Vector2(j * MapManager.Wall.Width, i * MapManager.Wall.Height)));
                    }
                    else if (MapStructure[i][j] == 'g') { GroundVectors.Add(new Vector2(j * MapManager.Wall.Width, i * MapManager.Wall.Height)); }
                    else if (MapStructure[i][j] == 'a') { ABombSiteVectors.Add(new Vector2(j * MapManager.Wall.Width, i * MapManager.Wall.Height)); }
                    else if (MapStructure[i][j] == 'b') { BBombSiteVectors.Add(new Vector2(j * MapManager.Wall.Width, i * MapManager.Wall.Height)); }
                    else if (MapStructure[i][j] == 'c') { CTSpawnVectors.Add(new Vector2(j * MapManager.Wall.Width, i * MapManager.Wall.Height)); }
                    else if (MapStructure[i][j] == 't') { TTSpawnVectors.Add(new Vector2(j * MapManager.Wall.Width, i * MapManager.Wall.Height)); }
                }
            }

        }
        public static char[][] JsonToChar(string Json)
        {
            return JsonSerializer.Deserialize<char[][]>(Json);
        }
    }
}
