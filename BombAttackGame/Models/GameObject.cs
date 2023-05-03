using BombAttackGame.Bonuses;
using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Global;
using BombAttackGame.Map;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BombAttackGame.Models
{
    internal class GameObject
    {
        public static Player AddPlayer(Team team, (int Width, int Height) MapSize, MapManager Map)
        {
            Player Player = new Player(team);
            Player.Location = Spawn.ChooseRandomSpawnPoint(Map, team);
            if (team == Team.Enemy)
            {
                Player.Color = Color.Red;
            }
            if (team == Team.TeamMate)
            {
                Player.Color = Color.Green;
            }
            return Player;
        }

        public static List<Player> AddPlayers(Team team, int Amount, MapManager Map)
        {
            List<Player> Players = new List<Player>();
            for (int i = 0; i < Amount; i++)
            {
                Player Player = new Player(team);
                Player.Team = team;
                Player.Location = Spawn.ChooseRandomSpawnPoint(Map, team);
                if (team == Team.Enemy)
                {
                    Player.Color = Color.Red;
                }
                if (team == Team.TeamMate)
                {
                    Player.Color = Color.Green;
                }
                Players.Add(Player);
            }
            return Players;
        }
        public static MainSpeed AddMainSpeed(Team Team, MapManager Map)
        {
            var mainSpeed = new MainSpeed();
            mainSpeed.Texture = ContentContainer.MainSpeedTexture;
            mainSpeed.Location = Spawn.ChooseBonusRandomSpawnPoint(Map, Team);
            mainSpeed.IsDead = false;
            return mainSpeed;
        }
    }
}
