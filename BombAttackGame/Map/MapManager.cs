﻿using BombAttackGame.Global;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Text.Json;

namespace BombAttackGame.Map
{
    internal class MapManager
    {
        public static Texture2D Wall => ContentContainer.WallTexture;
        public Mirage Mirage { get; set; }

        public MapManager()
        {
        }
        public void GenerateMirage()
        {
            Mirage = new Mirage();
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
                    if(Model.Contains("w") && (MapStructure[i][j] == 'w')) Map.Add(new Vector2(j * MapManager.Wall.Height, i * MapManager.Wall.Width));
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
