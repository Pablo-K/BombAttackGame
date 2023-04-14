using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Runtime.CompilerServices;

namespace BombAttackGame.Models
{
    internal class Bullet : IGameObject 
    {
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public bool IsDead { get; set; }
        public int Speed { get; set; }
        public float Distance { get; set; }
        public float DistanceTravelled { get; set; }
        public float MaxDistance { get; set; }
        public double Damage { get; set; }
        public double TeamDamage { get; set; }
        public double EnemyDamage { get; set; }
        public double OtherDamage { get; set; }
        public Player Owner { get; set; }
        public Vector2 StartLocation { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Point { get; set; }
        public Rectangle Rectangle { get; set; }
        public Event Event { get; set; }
        public IGameObject ObjectHitted { get; set; }
        public int DamageDealt { get; set; }
        public Bullet(Vector2 location, Player owner, Vector2 point)
        {
            this.Location = new Vector2(location.X, location.Y);
            this.StartLocation = new Vector2(location.X, location.Y);
            this.Speed = 5;
            this.Damage = 25;
            this.Owner = owner;
            this.Point = point;
            this.MaxDistance = 120000;
            this.TeamDamage = 0.5;
            this.EnemyDamage = 1;
            this.OtherDamage = 1;
            this.IsDead = false;
            this.Color = Color.AliceBlue;
            this.Event = Event.None;
        }
        public int CalculateDamage()
        {
            switch (this.DistanceTravelled)
            {
                case > 80000:
                    Convert.ToInt32(Damage = Damage * 0.1); break;
                case > 55000:
                    Convert.ToInt32(Damage = Damage * 0.2); break;
                case > 40000:
                    Convert.ToInt32(Damage = Damage * 0.3); break;
                case > 30000:
                    Convert.ToInt32(Damage = Damage * 0.4); break;
                case > 20000:
                    Convert.ToInt32(Damage = Damage * 0.5); break;
                case > 15000:
                    Convert.ToInt32(Damage = Damage * 0.6); break;
                case > 10000:
                    Convert.ToInt32(Damage = Damage * 0.7); break;
                case > 8000:
                    Convert.ToInt32(Damage = Damage * 0.8); break;
                case > 5000:
                    Convert.ToInt32(Damage = Damage * 0.9); break;
            }
            return (int)Damage;
        }
        public void UpdateRectangle()
        {
            this.Rectangle = new Rectangle((int)Location.X, (int)Location.Y, Texture.Width, Texture.Height);
        }

        //private static void BulletsHit(GameTime GameTime, List<IGameObject> GameObjects, List<IGameSprite> GameSprites, ContentManager Content)
        //{
        //    foreach (Bullet bullet in GameObjects.OfType<Bullet>().ToList())
        //    {
        //        if (Hit.BulletHit(bullet, GameObjects, out Player PlayerHitted))
        //        {
        //            int Damage = (int)bullet.CalculateDamage(PlayerHitted);
        //            PlayerHitted.Hit(Damage);
        //            GameObjects.Remove(bullet);

        //            Damage damage = new Damage((int)Damage, PlayerHitted.Location);
        //            damage.Font = Content.Load<SpriteFont>("damage");
        //            damage.ShowTime = GameTime.TotalGameTime.TotalMilliseconds;
        //            GameSprites.Add(damage);
        //        }
        //    }
        //}
        public void Tick(GameTime GameTime, List<IGameObject> GameObjects)
        {
            Move();
            UpdateRectangle();
            BulletHitted(GameObjects);
        }
        private void Move()
        {
            var speed = this.Speed * (1 / this.Distance);
            float Length = 0;
            this.Location = Vector2.Lerp(this.Location, this.Point, speed);
            Length += Vector2.Distance(this.Location, this.StartLocation);
            this.DistanceTravelled += Length;
        }
        private void BulletHitted(List<IGameObject> GameObjects)
        {
            foreach (IGameObject obj in GameObjects.OfType<Player>().Where(x => x.IsDead == false))
            {
                if (Hit.InHitbox(this,obj as Player))
                {
                    this.Event = Event.ObjectHitted;
                    this.ObjectHitted = obj;
                    this.DamageDealt = CalculateDamage();
                }
            }
        }
    }
}
