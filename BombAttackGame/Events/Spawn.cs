using BombAttackGame.Enums;
using BombAttackGame.Map;
using Microsoft.Xna.Framework;
using System;
using System.Linq;

namespace BombAttackGame.Events
{
    internal class Spawn
    {
        public static Vector2 ChooseRandomSpawnPoint(MapManager Map, Team Team)
        {
            Random random = new Random();
            if(Team == Team.TeamMate)
            {
                return Map.CTSpawnVectors.ElementAt(random.Next(0, Map.CTSpawnVectors.Count));
            }
            if(Team == Team.Enemy)
            {
                return Map.TTSpawnVectors.ElementAt(random.Next(0, Map.TTSpawnVectors.Count));
            }
            else
            {
                return Map.CTSpawnVectors.ElementAt(random.Next(0, Map.CTSpawnVectors.Count));
            }
        }
    }
}
