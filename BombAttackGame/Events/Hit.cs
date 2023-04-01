using BombAttackGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Events
{
    internal class Hit
    {
        public static bool BulletHit(Bullet bullet, List<Player> players, out Player player) 
        {
            player = null;
            foreach (Player p in players)
            {

                if (
                    (Math.Abs(bullet.Location.X - p.Location.X) < p.Texture.Width) && 
                    (bullet.Location.Y - p.Location.Y <= p.Texture.Height) && 
                    (bullet.Location.Y >= p.Location.Y) &&
                    bullet.Owner != p)
                {
                    player = p;
                    return true;
                }
            }
            return false;
        }
    }
}
