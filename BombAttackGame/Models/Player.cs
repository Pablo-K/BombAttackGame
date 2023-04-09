using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Interfaces;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Models
{
    internal class Player : IPlayer
    {
        public Vector2 Location { get; set; }
        public Direction Direction { get; set; }
        public int Health { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsDead { get; set; }
        public bool IsAttacked { get; set; }
        public int Speed { get; set; }
        public double ShotTime { get; set; }
        public double ShotLatency { get; set; }

        public Player() { 
        
            this.Direction = Direction.Right;
            this.Speed = 2;
            this.Health = 100;
            this.ShotLatency = 100;
        }
        public void Hit(int Damage)
        {
            Health -= Damage;
        }

        public static void TryShoot(Player Player, GameTime GameTime, Microsoft.Xna.Framework.Content.ContentManager Content, Vector2 ShootLoc, List<Bullet> Bullets)
        {
            Bullet Bullet = null;
            ShootLoc = VectorTool.ExtendVector(ShootLoc, Player.Location, 100000); 
            Bullet = Shoot.PlayerShoot(Player, GameTime, Content, ShootLoc);
            if (Bullet == null) { return; }
            Bullets.Add(Bullet);
            Bullet.Direction = ShootLoc - Player.Location;
            Bullet.Direction.Normalize();
        }
    }
}
