using BombAttackGame.Enums;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace BombAttackGame.Map
{
    internal class PathFinder
    {
        public static void FindPath(List<Direction> list, List<Tile> movingtiles, string[] map, Point position, Point end)
        {
            Tile current = null;
            var start = new Tile { X = position.X, Y = position.Y };
            var target = new Tile { X = end.X, Y = end.Y };
            var openList = new List<Tile>();
            var closedList = new List<Tile>();
            int g = 0;
            openList.Add(start);

            while (openList.Count > 0)
            {
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);

                closedList.Add(current);

                openList.Remove(current);

                if (closedList.FirstOrDefault(l => l.X == target.X && l.Y == target.Y) != null)
                    break;

                var adjacentSquares = GetWalkableAdjacentSquares(current.X, current.Y, map);
                g++;

                foreach (var adjacentSquare in adjacentSquares)
                {
                    if (closedList.FirstOrDefault(l => l.X == adjacentSquare.X
                            && l.Y == adjacentSquare.Y) != null)
                        continue;

                    if (openList.FirstOrDefault(l => l.X == adjacentSquare.X
                            && l.Y == adjacentSquare.Y) == null)
                    {
                        adjacentSquare.G = g;
                        adjacentSquare.H = ComputeHScore(adjacentSquare.X, adjacentSquare.Y, target.X, target.Y);
                        adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                        adjacentSquare.Parent = current;

                        openList.Insert(0, adjacentSquare);
                    }
                    else
                    {
                        if (g + adjacentSquare.H < adjacentSquare.F)
                        {
                            adjacentSquare.G = g;
                            adjacentSquare.F = adjacentSquare.G + adjacentSquare.H;
                            adjacentSquare.Parent = current;
                        }
                    }
                }
            }
            var newmap = map;

            while (current != null)
            {
                if (current.Parent != null)
                {
                    movingtiles.Add(current);
                    if (current.X > current.Parent.X) list.Add(Direction.Right);
                    if (current.X < current.Parent.X) list.Add(Direction.Left);
                    if (current.Y > current.Parent.Y) list.Add(Direction.Down);
                    if (current.Y < current.Parent.Y) list.Add(Direction.Up);
                }
                current = current.Parent;
            }

            list.Reverse();
            movingtiles.Reverse();
        }

        static List<Tile> GetWalkableAdjacentSquares(int x, int y, string[] map)
        {
            var proposedLocations = new List<Tile>()
            {
                new Tile { X = x, Y = y - 1 },
                new Tile { X = x, Y = y + 1 },
                new Tile { X = x - 1, Y = y },
                new Tile { X = x + 1, Y = y },
            };
            List<Tile> result = new List<Tile>();
            foreach (var item in proposedLocations)
            {
                if (Char.IsLower(map[item.Y][item.X]))
                {
                    result.Add(item);
                }
            }
            return result;
        }

        static int ComputeHScore(int x, int y, int targetX, int targetY)
        {
            return Math.Abs(targetX - x) + Math.Abs(targetY - y);
        }
    }
}
