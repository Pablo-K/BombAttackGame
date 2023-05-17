using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BombAttackGame.Map
{
    internal class BotPoints
    {
        public static List<Point> CTPoints { get; private set; }
        public static List<Point> TTPoints { get; private set; }

        public BotPoints()
        {
            BotPoints.CTPoints = new List<Point>();
            BotPoints.TTPoints = new List<Point>();
            BotPoints.CTPoints.AddRange(MapManager.GetPointsFromChar('a'));
            BotPoints.CTPoints.AddRange(MapManager.GetPointsFromChar('b'));
            BotPoints.CTPoints.AddRange(MapManager.GetPointsFromChar('t'));
            BotPoints.CTPoints.AddRange(MapManager.GetPointsFromChar('c'));
            BotPoints.TTPoints.AddRange(MapManager.GetPointsFromChar('c'));
            BotPoints.TTPoints.AddRange(MapManager.GetPointsFromChar('t'));
            BotPoints.TTPoints.AddRange(MapManager.GetPointsFromChar('a'));
            BotPoints.TTPoints.AddRange(MapManager.GetPointsFromChar('b'));
        }
    }
}
