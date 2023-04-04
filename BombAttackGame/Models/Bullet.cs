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
        public int Damage { get; set; }
        public int Speed { get; set; }
        public List<Vector2> Trajectory { get; set; }
        public Player Owner { get; set; }
        public Point Point { get; set; }
        public int TrajectoryIndex { get; set; }
        public Bullet(Vector2 location, Player owner, Point point)
        {
            this.Location = new Vector2(location.X, location.Y);
            this.Speed = 5;
            this.Damage = 20;
            this.Owner = owner;
            this.Point = point;
            this.TrajectoryIndex = 0;
        }
    }
}
