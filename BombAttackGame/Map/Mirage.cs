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
        public List<Vector2> SpawnPoints { get; set; }
        public Mirage()
        {
            this.Structure = MapManager.JsonToChar(File.ReadAllText("Map/Mirage.json"));
            this.WallVector = MapManager.MapConverter(this.Structure, "w");
            this.Rectangle = new List<Rectangle>();
            this.Rectangle.AddRange(MapManager.WallRectangle(this.WallVector));
            this.SpawnPoints = new List<Vector2>
            {
                new Vector2(160, 760), 
                new Vector2(160, 780),
                new Vector2(180, 760),
                new Vector2(200, 760),
                new Vector2(220, 760),
                new Vector2(240, 760),
                new Vector2(180, 800),
                new Vector2(200, 740),
                new Vector2(860, 400),
                new Vector2(860, 420),
                new Vector2(880, 400),
                new Vector2(880, 420),
                new Vector2(880, 440),
                new Vector2(880, 460),
                new Vector2(880, 480),
                new Vector2(840, 400)
            };
        }
    }
}
