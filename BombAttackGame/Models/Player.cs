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
        public Texture2D Texture { get; set; }
        public bool IsDead { get; set; }
        public bool IsAttacked { get; set; }
        public int Speed { get; set; }
        public int Damage { get; set; }
        public double ShotTime { get; set; }
        public double ShotLatency { get; set; }

        public Player(int x, int y) { 
        
            this.Location = new Vector2(x,y);
            this.Direction = Direction.Right;
            this.Speed = 2;
            this.Damage = 20;
            this.ShotLatency = 100;
        }
    }
}
