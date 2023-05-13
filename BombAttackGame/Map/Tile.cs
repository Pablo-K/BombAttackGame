using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Map
{
    internal class Tile
    {
        public int X;
        public int Y;
        public int F;
        public int G;
        public int H;
        public Tile Parent;
    }
}
