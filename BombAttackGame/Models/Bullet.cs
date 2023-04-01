using BombAttackGame.Enums;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Models
{
    internal class Bullet : IBullet
    {
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public Direction Direction { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public Player Owner { get; set; }
        public Bullet(Vector2 location, Direction direction, Player owner)
        {
            this.Location = new Vector2(location.X, location.Y);
            this.Direction = direction;
            this.Speed = 4;
            this.Damage = 20;
            this.Owner = owner;
        }
    }
}
