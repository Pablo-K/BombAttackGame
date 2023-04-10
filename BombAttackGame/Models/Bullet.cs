using BombAttackGame.Events;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Models
{
    internal class Bullet 
    {
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
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
        }
        public int CalculateDamage(Player Player)
        {
            switch (DistanceTravelled)
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
            if (Player.Team == Owner.Team) return (int)(Damage * TeamDamage);
            if (Player.Team == Team.Enemy && (Owner.Team == Team.TeamMate || Owner.Team == Team.Player)) return (int)(Damage * EnemyDamage);
            if (Player.Team == Team.TeamMate && Owner.Team == Team.Player) return (int)(Damage * TeamDamage);
            return (int)Damage;
        }

        private static void BulletsHit(GameTime GameTime, List<Player> AllPlayers, List<Bullet> Bullets, List<Damage> Damages)
        {
            foreach (Bullet bullet in Bullets.ToList())
            {
                if (Hit.BulletHit(bullet, AllPlayers, out Player PlayerHitted))
                {
                    int Damage = (int)bullet.CalculateDamage(PlayerHitted);
                    PlayerHitted.Hit(Damage);
                    Bullets.Remove(bullet);

                    Damage damage = new Damage((int)Damage, PlayerHitted.Location);
                    damage.ShowTime = GameTime.TotalGameTime.TotalMilliseconds;
                    Damages.Add(damage);
                }
            }
        }
        public static void Tick(GameTime GameTime, List<Player> AllPlayers, List<Bullet> Bullets, List<Damage> Damages)
        {
            foreach (Bullet bullet in Bullets.ToList())
            {
                if(bullet.DistanceTravelled >= bullet.MaxDistance)
                { Bullets.Remove(bullet); }
                Move(bullet);
            }
            BulletsHit(GameTime, AllPlayers, Bullets, Damages);
        }
        private static void Move(Bullet Bullet)
        {
            var speed = Bullet.Speed * (1 / Bullet.Distance);
            float Length = 0;
            Bullet.Location = Vector2.Lerp(Bullet.Location, Bullet.Point, speed);
            Length += Vector2.Distance(Bullet.Location, Bullet.StartLocation);
            Bullet.DistanceTravelled += Length;
        }
    }
}
