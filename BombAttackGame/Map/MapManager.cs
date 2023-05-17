using BombAttackGame.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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
        public static string[] MapString { get; set; }

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
            MapManager.MapString = CharToStringConverter(this.CharMap);
        }
        public static List<Point> GetPointsFromChar(char c)
        {
            List<Point> points = new List<Point>();
            for (int i = 0; i < MapManager.MapString.Length; i++)
            {
                for (int j = 0; j < MapManager.MapString.Length; j++)
                {
                    if (MapManager.MapString[i][j] == c)
                        points.Add(new Point(j, i));
                }
            }
            return points;
        }
        public static bool IsOnTile(Vector2 loc, Texture2D texture, Point tile)
        {
            bool b = true;
            List<Point> pos = new List<Point>() {
                new Point((int)loc.X / 20, (int)loc.Y / 20),
                new Point(((int)loc.X + texture.Width) / 20, (int)loc.Y / 20),
                new Point((int)loc.X / 20, ((int)loc.Y + texture.Height) / 20),
                new Point(((int)loc.X + texture.Width) / 20, ((int)loc.Y + texture.Height) / 20),
            };
            foreach (Point p in pos)
            {
                if (p != tile)
                {
                    b = false;
                    return b;
                }
            }
            return b;
        }

        private char[][] Reverse(char[][] map)
        {
            char[][] newchar = map;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map.Length; j++)
                {
                    newchar[i][j] = map[j][i];
                }
            }
            return newchar;
        }
        private string[] CharToStringConverter(char[][] map)
        {
            string[] s = new string[map.Length];
            for (int i = 0; i < map.Length; i++)
            {
                string str = "";
                for (int j = 0; j < map.Length; j++)
                {
                    str += map[i][j];
                }
                s[i] = str;
            }
            return s;
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
