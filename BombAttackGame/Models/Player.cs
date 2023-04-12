using BombAttackGame.Bonuses;
using BombAttackGame.Collisions;
using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Interfaces;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

namespace BombAttackGame.Models
{
    public enum Team
    {
        None,
        Player,
        TeamMate,
        Enemy
    }
    internal class Player
    {
        GameServiceContainer _container = new GameServiceContainer();
        public Vector2 Location { get; set; }
        public Vector2 OldLocation { get; set; }
        public Vector2 ShootLocation { get; set; }
        public List<Vector2> Collision { get; set; }
        public Direction Direction { get; set; }
        public Team Team { get; set; }
        public int Health { get; set; }
        public double Speed { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsDead { get; set; }
        public bool IsAttacked { get; set; }
        public bool OnMainSpeed { get; set; }
        public double ShotTime { get; set; }
        public double ShotLatency { get; set; }
        public double EnemyShotLatency { get; set; }
        public double TeamMateShotLatency { get; set; }
        public double MainSpeedStartTime { get; set; }
        public double MainSpeedEndTime { get; set; }
        public double MovingTime { get; set; }
        public double MovingEndTime { get; set; }
        public Color Color { get; set; }
        public Event Event { get; set; }

        public Player(Texture2D Texture)
        {
            this.Texture = Texture;
            this.Direction = Direction.Right;
            this.Color = Color.Red;
            this.Speed = 2;
            this.ShotLatency = 100;
            this.EnemyShotLatency = 1000;
            this.TeamMateShotLatency = 1000;
            this.Health = 100;
            this.MovingTime = 1000;
            this.OnMainSpeed = false;
        }
        public void PlayerMove(Direction direction)
        {
            OldLocation = Location;
            if (direction == Direction.Left)
            {
                Location = new Vector2(Location.X - (int)Speed, Location.Y);
                Direction = Direction.Left;
            }
            if (direction == Direction.Right)
            {
                Location = new Vector2(Location.X + (int)Speed, Location.Y);
                Direction = Direction.Right;
            }
            if (direction == Direction.Down)
            {
                Location = new Vector2(Location.X, Location.Y + (int)Speed);
                Direction = Direction.Down;
            }
            if (direction == Direction.Up)
            {
                Location = new Vector2(Location.X, Location.Y - (int)Speed);
                Direction = Direction.Up;
            }
            Event = Event.Move;
        }
        public void Hit(int Damage)
        {
            Health -= Damage;
        }

        public void TryShoot(Vector2 ShootLocation)
        {
            //Bullet Bullet = null;
            //ShootLoc = VectorTool.ExtendVector(ShootLoc, Location, 100000);
            //Bullet = Shoot.PlayerShoot(GameTime, Content, ShootLoc);
            //if (Bullet == null) { return; }
            //Bullets.Add(Bullet);
            //Bullet.Direction = ShootLoc - Player.Location;
            //Bullet.Direction.Normalize();
            this.ShootLocation = ShootLocation;
            this.Event = Event.TryShoot;
        }
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
        public void Tick()
        {
            //if (CheckIfDead(player)) Players.Remove(player);
            UpdateCollision();
            UpdateColor(Color);
            this.Event = Event.None;
            //if (VectorTool.IsOnObject(player.Collision, MainSpeed.Collision))
            //{ MainSpeed.PickedBonus(player, MainSpeed, GameTime); }
            //if (player.OnMainSpeed) MainSpeedTime(player, GameTime, MainSpeed);
            //PlayerShoot(player, GameTime, Content, Players, Bullets);
            //PlayerMove();
        }
        private static bool CheckIfDead(Player Player)
        {
            if (Player.Health <= 0) return true;
            return false;
        }
        public void UpdateCollision()
        {
            Collision = VectorTool.Collision(Location, Texture);
        }
        public static void MainSpeedTime(Player Player, GameTime GameTime, MainSpeed MainSpeed)
        {
            if (Player.MainSpeedEndTime <= GameTime.TotalGameTime.TotalMilliseconds && Player.OnMainSpeed)
            {
                Player.Speed /= MainSpeed.WalkSpeed;
                Player.ShotLatency *= MainSpeed.ShootingSpeed;
                Player.OnMainSpeed = false;
            }
        }
        public void UpdateColor(Color Color)
        {
            if (OnMainSpeed)
            {
                this.Color = Color.GreenYellow;
            }
            else
            {
                this.Color = Color;
            }
        }
        public static void PlayerShoot(Player Player, GameTime GameTime, ContentManager Content, List<Player> Players, List<Bullet> Bullets)
        {
            //List<Player> PlayersRandom = new List<Player>();
            //PlayersRandom.AddRange(Players);
            //int n = PlayersRandom.Count;
            //Random rng = new Random();
            //while (n > 1)
            //{
            //    n--;
            //    int k = rng.Next(n + 1);
            //    Player value = PlayersRandom[k];
            //    PlayersRandom[k] = PlayersRandom[n];
            //    PlayersRandom[n] = value;
            //}
            //{
            //    foreach (Player player in PlayersRandom)
            //    {
            //        if (!(Player.Team == Team.TeamMate && player.Team == Team.TeamMate))
            //        {
            //            if (!(Player.Team == Team.Enemy && player.Team == Team.Enemy))
            //            {
            //                if (Player.Team == Team.Enemy && (player.Team == Team.TeamMate || player.Team == Team.Player)) Player.TryShoot(Player, GameTime, Content, player.Location, Bullets);
            //                if (Player.Team == Team.TeamMate && player.Team == Team.Enemy) Player.TryShoot(Player, GameTime, Content, player.Location, Bullets);
            //            }
            //        }
            //    }
            //}
        }
        public void BotMove(Player Player, Collision Collision, GameTime GameTime)
        {
            //if (Player.Team == Team.Player) return;
            //if (Player.Direction == Direction.None) return;
            //if (GameTime.TotalGameTime.TotalMilliseconds >= Player.MovingEndTime)
            //{
            //    Random random = new Random();
            //    Player.Direction = (Direction)random.Next(-1, 4);
            //    Player.MovingEndTime = GameTime.TotalGameTime.TotalMilliseconds + Player.MovingTime;
            //    PlayerMove(Player, Player.Direction, Collision);
            //    return;
            //}
            //PlayerMove(Player, Player.Direction, Collision);
        }
    }
}
