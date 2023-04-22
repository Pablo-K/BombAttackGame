﻿using BombAttackGame.Bonuses;
using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Map;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Models
{
    internal class GameObject
    {
        public static Player AddPlayer(Team Team, ContentManager Content, int[] MapSize, MapManager Map)
        {
            Texture2D Texture = Content.Load<Texture2D>(Team.ToString());
            Player Player = new Player(Texture);
            Player.Team = Team;
            Player.Location = Spawn.ChooseRandomSpawnPoint(MapSize, Map, Team);
            if (Team == Team.Enemy) Player.ShotLatency = Player.EnemyShotLatency;
            if (Team == Team.TeamMate) Player.ShotLatency = Player.TeamMateShotLatency;
            return Player;
        }

        public static List<Player> AddPlayers(Team Team, ContentManager Content, int Amount, int[] MapSize, MapManager Map)
        {
            List<Player> Players = new List<Player>();
            Texture2D Texture = Content.Load<Texture2D>(Team.ToString());
            for (int i = 0; i < Amount; i++)
            {
                Player Player = new Player(Texture);
                Player.Team = Team;
                Player.Location = Spawn.ChooseRandomSpawnPoint(MapSize, Map, Team);
                if (Team == Team.Enemy) Player.ShotLatency = Player.EnemyShotLatency;
                if (Team == Team.TeamMate) Player.ShotLatency = Player.TeamMateShotLatency;
                Players.Add(Player);
            }
            return Players;
        }
        public static MainSpeed AddMainSpeed(int[] MapSize, ContentManager Content, Team Team, MapManager Map)
        {
            MainSpeed MainSpeed = new MainSpeed();
            MainSpeed.Texture = Content.Load<Texture2D>("mainSpeed");
            MainSpeed.Location = Spawn.ChooseRandomSpawnPoint(MapSize, Map, Team);
            //MainSpeed.Rectangle = VectorTool.Collision(MainSpeed.Location, MainSpeed.Texture);
            MainSpeed.IsDead = false;
            return MainSpeed;
        }
    }
}
