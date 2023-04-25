using BombAttackGame.Bonuses;
using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Global;
using BombAttackGame.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Models
{
    internal class GameObject
    {
        public static Player AddPlayer(Team Team, int[] MapSize, MapManager Map)
        {
            Player Player = new Player();
            Player.Team = Team;
            Player.Location = Spawn.ChooseRandomSpawnPoint(MapSize, Map, Team);
            if (Team == Team.Enemy)
            {
                Player.ShotLatency = Player.EnemyShotLatency;
                Player.Color = Color.Red;
            }
            if (Team == Team.TeamMate)
            {
                Player.ShotLatency = Player.TeamMateShotLatency;
                Player.Color = Color.Green;
            }
            return Player;
        }

        public static List<Player> AddPlayers(Team Team, int Amount, int[] MapSize, MapManager Map)
        {
            List<Player> Players = new List<Player>();
            for (int i = 0; i < Amount; i++)
            {
                Player Player = new Player();
                Player.Team = Team;
                Player.Location = Spawn.ChooseRandomSpawnPoint(MapSize, Map, Team);
                if (Team == Team.Enemy)
                {
                    Player.ShotLatency = Player.EnemyShotLatency;
                    Player.Color = Color.Red;
                }
                if (Team == Team.TeamMate)
                {
                    Player.ShotLatency = Player.TeamMateShotLatency;
                    Player.Color = Color.Green;
                }
                Players.Add(Player);
            }
            return Players;
        }
        public static MainSpeed AddMainSpeed(int[] MapSize, Team Team, MapManager Map)
        {
            var mainSpeed = new MainSpeed();
            mainSpeed.Texture = ContentContainer.MainSpeedTexture;
            mainSpeed.Location = Spawn.ChooseBonusRandomSpawnPoint(MapSize, Map, Team);
            mainSpeed.IsDead = false;
            return mainSpeed;
        }
    }
}
