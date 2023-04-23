using BombAttackGame.Bonuses;
using BombAttackGame.Collisions;
using BombAttackGame.Enums;
using BombAttackGame.Interfaces;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Models
{
    internal class Player : IGameObject
    {
        public Vector2 Location { get; set; }
        public List<Vector2> OldLocation { get; set; }
        public Vector2 ShootLocation { get; set; }
        public Direction Direction { get; set; }
        public Team Team { get; set; }
        public int Health { get; private set; }
        public double Speed { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsDead { get; set; }
        public bool IsAttacked { get; set; }
        public bool OnMainSpeed { get; private set; }
        public double ShotTime { get; set; }
        public double ShotLatency { get; set; }
        public double EnemyShotLatency { get; private set; }
        public double TeamMateShotLatency { get; private set; }
        public double MainSpeedStartTime { get; private set; }
        public double MainSpeedEndTime { get; private set; }
        private double MovingTime { get; set; }
        private double MovingEndTime { get; set; }
        public Color Color { get; set; }
        public Queue<Event> Event { get; private set; }
        public Rectangle Rectangle { get; set; }
        public bool Human { get; set; }
        public List<IGameObject> VisibleObjects { get; set; }

        public Player(Texture2D Texture)
        {
            this.Texture = Texture;
            this.Direction = Direction.Right;
            this.Speed = 2;
            this.ShotLatency = 100;
            this.EnemyShotLatency = 1000;
            this.TeamMateShotLatency = 400;
            this.Health = 100;
            this.MovingTime = 1000;
            this.OnMainSpeed = false;
            this.OldLocation = new List<Vector2>();
            this.Event = new Queue<Event>();
            this.Human = false;
            this.VisibleObjects = new List<IGameObject>();
        }
        public void PlayerMove(Direction direction)
        {
            OldLocation.Add(Location);
            this.Direction = direction;
            if (!this.Event.Contains(Enums.Event.Move)) { Event.Enqueue(Enums.Event.Move); }
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
            if (!this.Event.Contains(Enums.Event.TryShoot)) { this.Event.Enqueue(Enums.Event.TryShoot); }
        }
        public void Tick(GameTime GameTime, List<IGameObject> GameObjects, List<Rectangle> MapRectangle)
        {
            UpdateRectangle();
            UpdateColor(Color);
            CheckIfDead();
            BotMove(GameTime);
            UpdateObjectsVisibility(GameObjects, MapRectangle);
            //if (VectorTool.IsOnObject(player.Collision, MainSpeed.Collision))
            //{ MainSpeed.PickedBonus(player, MainSpeed, GameTime); }
            //if (player.OnMainSpeed) MainSpeedTime(player, GameTime, MainSpeed);
            //PlayerShoot(player, GameTime, Content, Players, Bullets);
            //PlayerMove();
        }
        private void UpdateObjectsVisibility(List<IGameObject> GameObjects, List<Rectangle> MapRectangle)
        {
            foreach (var obj in GameObjects)
            {
                bool intersects = VectorTool.CheckLineIntersection(this.Location, obj.Location, MapRectangle);
                if (!intersects)
                {
                    if (!this.VisibleObjects.Contains(obj)) this.VisibleObjects.Add(obj);
                }
                else
                {
                    if (this.VisibleObjects.Contains(obj)) this.VisibleObjects.Remove(obj);
                }
            }
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
        public void BotMove(GameTime GameTime)
        {
            if (this.Human) return;
            if (GameTime.TotalGameTime.TotalMilliseconds >= MovingEndTime)
            {
                Random random = new Random();
                int x = random.Next(0,8);
                var Dir = (Direction)x;
                MovingEndTime = GameTime.TotalGameTime.TotalMilliseconds + MovingTime;
                //if (Dir == Direction.Up || Dir == Direction.Down) losuj();
                PlayerMove(Dir);
                return;
            }
            PlayerMove(Direction);
        }
     }

}
