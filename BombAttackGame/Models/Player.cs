using BombAttackGame.Enums;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public Player(Vector2 location) { 
        
            this.Location = new Vector2(location.X, location.Y);
            this.Direction = Direction.Right;
            this.Speed = 2;
            this.Health = 100;
            this.ShotLatency = 100;
        }
        public void Hit(int Damage)
        {
            Health -= Damage;
        }
    }
}
