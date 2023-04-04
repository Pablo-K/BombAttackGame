using BombAttackGame.Enums;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Interfaces
{
    internal interface IBullet
    {
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public List<Vector2> Trajectory { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public Point Point { get; set; }

    }
}
