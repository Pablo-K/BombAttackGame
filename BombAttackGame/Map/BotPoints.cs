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
            BotPoints.CTPoints.AddRange(GetPointsFromChar('a'));
            BotPoints.CTPoints.AddRange(GetPointsFromChar('b'));
            BotPoints.CTPoints.AddRange(GetPointsFromChar('t'));
            BotPoints.CTPoints.AddRange(GetPointsFromChar('c'));
            BotPoints.TTPoints.AddRange(GetPointsFromChar('c'));
            BotPoints.TTPoints.AddRange(GetPointsFromChar('t'));
            BotPoints.TTPoints.AddRange(GetPointsFromChar('a'));
            BotPoints.TTPoints.AddRange(GetPointsFromChar('b'));
        }

        private List<Point> GetPointsFromChar(char c)
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

    }
}
