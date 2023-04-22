using BombAttackGame.Enums;
using BombAttackGame.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Events
{
    internal class Spawn
    {
        public static Vector2 ChooseRandomSpawnPoint(int[] MapSize, MapManager Map, Team Team)
        {
            Random random = new Random();
            if(Team == Team.TeamMate)
            {
                return Map.Mirage.SpawnPoints.ElementAt(random.Next(0, Map.Mirage.SpawnPoints.Count/2));
            }
            if(Team == Team.Enemy)
            {
                return Map.Mirage.SpawnPoints.ElementAt(random.Next(Map.Mirage.SpawnPoints.Count/2+1, Map.Mirage.SpawnPoints.Count));
            }
            else
            {
                return Map.Mirage.SpawnPoints.ElementAt(random.Next(-1, 10));
            }
        }
        public static Vector2 ChooseBonusRandomSpawnPoint(int[] MapSize, MapManager Map, Team Team)
        {
            Random random = new Random();
            return Map.Mirage.BonusSpawnPoints.ElementAt(random.Next(0, Map.Mirage.BonusSpawnPoints.Count));
        }
    }
}
