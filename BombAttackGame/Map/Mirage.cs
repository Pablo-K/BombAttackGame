using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.IO;

namespace BombAttackGame.Map
{
    internal class Mirage
    {
        public char[][] Structure { get; set; }
        public List<Vector2> WallVector { get; set; }
        public List<Rectangle> Rectangle { get; set; }
        public Mirage()
        {
            this.Structure = MapManager.JsonToChar(File.ReadAllText("Map/Mirage.json"));
            this.WallVector = MapManager.MapConverter(this.Structure, "w");
            this.Rectangle = new List<Rectangle>();
            this.Rectangle.AddRange(MapManager.WallRectangle(this.WallVector));
        }
    }
}
