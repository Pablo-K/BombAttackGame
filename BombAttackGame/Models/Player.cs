using BombAttackGame.Bonuses;
using BombAttackGame.Collisions;
using BombAttackGame.Enums;
using BombAttackGame.Interfaces;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BombAttackGame.Models
{
    public enum Team
    {
        None,
        Player,
        TeamMate,
        Enemy
    }
    internal class Player : IGameObject
    {
        GameServiceContainer _container = new GameServiceContainer();
        public Vector2 Location { get; set; }
        public List<Vector2> OldLocation { get; set; }
        public Vector2 ShootLocation { get; set; }
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
        public List<Event> Event { get; set; }
        public Rectangle Rectangle { get; set; }

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
            this.OldLocation = new List<Vector2>();
            this.Event = new List<Event>();
        }
        public void PlayerMove(Direction direction)
        {
            OldLocation.Add(Location);
            this.Direction = direction;
            if(!this.Event.Contains(Enums.Event.Move)) { Event.Add(Enums.Event.Move); }
        }
        public void Hit(int Damage)
        {
            Health -= Damage;
            if (Health <= 0)
            {
                this.IsDead = true;
            }
        }

        public void TryShoot(Vector2 ShootLocation)
        {
            this.ShootLocation = ShootLocation;
            if(!this.Event.Contains(Enums.Event.TryShoot)) { this.Event.Add(Enums.Event.TryShoot); }
        }
        public void Tick(GameTime GameTime, List<IGameObject> GameObjects)
        {
            UpdateRectangle();
            UpdateColor(Color);
            CheckIfDead();
            this.Event.Clear();
            //if (VectorTool.IsOnObject(player.Collision, MainSpeed.Collision))
            //{ MainSpeed.PickedBonus(player, MainSpeed, GameTime); }
            //if (player.OnMainSpeed) MainSpeedTime(player, GameTime, MainSpeed);
            //PlayerShoot(player, GameTime, Content, Players, Bullets);
            //PlayerMove();
        }
        private bool CheckIfDead()
        {
            if (this.Health <= 0) return true;
            return false;
        }
        public void UpdateRectangle()
        {
            this.Rectangle = new Rectangle((int)Location.X, (int)Location.Y, Texture.Width, Texture.Height);
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
        public void BotMove(Player Player, Collision Collision, GameTime GameTime)
        {
            if (Player.Team == Team.Player) return;
            if (Player.Direction == Direction.None) return;
            if (GameTime.TotalGameTime.TotalMilliseconds >= Player.MovingEndTime)
            {
                Random random = new Random();
                Player.Direction = (Direction)random.Next(-1, 9);
                Player.MovingEndTime = GameTime.TotalGameTime.TotalMilliseconds + Player.MovingTime;
                PlayerMove(Player.Direction);
                return;
            }
            PlayerMove(Player.Direction);
        }
    }
}
