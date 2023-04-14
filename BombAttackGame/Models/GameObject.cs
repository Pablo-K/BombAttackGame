using BombAttackGame.Bonuses;
using BombAttackGame.Collisions;
using BombAttackGame.Events;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Models
{
    internal class GameObject
    {
        public static Player AddPlayer(Team Team, ContentManager Content, int[] MapSize, Collision Collision)
        {
            Texture2D Texture = Content.Load<Texture2D>(Team.ToString());
            Player Player = new Player(Texture);
            Player.Team = Team;
            Player.Location = Spawn.GenerateRandomSpawnPoint(MapSize, Player.Texture, Collision);
            if (Team == Team.Enemy) Player.ShotLatency = Player.EnemyShotLatency;
            if (Team == Team.TeamMate) Player.ShotLatency = Player.TeamMateShotLatency;
            return Player;
        }

        public static List<Player> AddPlayers(Team Team, ContentManager Content, int Amount, int[] MapSize, Collision Collision)
        {
            List<Player> Players = new List<Player>();
            Texture2D Texture = Content.Load<Texture2D>(Team.ToString());
            for (int i = 0; i < Amount; i++)
            {
                Player Player = new Player(Texture);
                Player.Team = Team;
                Player.Location = Spawn.GenerateRandomSpawnPoint(MapSize, Player.Texture, Collision);
                if (Team == Team.Enemy) Player.ShotLatency = Player.EnemyShotLatency;
                if (Team == Team.TeamMate) Player.ShotLatency = Player.TeamMateShotLatency;
                Players.Add(Player);
            }
            return Players;
        }
        public static MainSpeed AddMainSpeed(int[] MapSize, ContentManager Content, Collision Collision)
        {
            MainSpeed MainSpeed = new MainSpeed();
            MainSpeed.Texture = Content.Load<Texture2D>("mainSpeed");
            MainSpeed.Location = Spawn.GenerateRandomSpawnPoint(MapSize, MainSpeed.Texture, Collision);
            //MainSpeed.Rectangle = VectorTool.Collision(MainSpeed.Location, MainSpeed.Texture);
            MainSpeed.IsDead = false;
            return MainSpeed;
        }
    }
}
