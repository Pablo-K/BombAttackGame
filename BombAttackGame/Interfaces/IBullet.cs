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
        public Direction Direction { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsDead { get; set; }
        public int Speed { get; set; }

    }
}
