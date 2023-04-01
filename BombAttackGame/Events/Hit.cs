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
        public static bool BulletHit(Bullet bullet, List<Player> players, out Player hitted) 
        {
            hitted = null;
            foreach (Player p in players)
            {
                if (InHitbox(bullet,p)) { hitted = p; return true; }
            }
            return false;
        }
        private static bool InHitbox(Bullet bullet, Player player)
        {
            if ( (Math.Abs(bullet.Location.X - player.Location.X) < player.Texture.Width) && 
                    (bullet.Location.Y - player.Location.Y <= player.Texture.Height) && 
                    (bullet.Location.Y >= player.Location.Y) &&
                    bullet.Owner != player)
                { return true; }
            return false;

        }
    }
}
