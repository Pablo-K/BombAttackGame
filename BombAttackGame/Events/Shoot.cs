using BombAttackGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Events
{
    internal class Shoot
    {
        public static Bullet PlayerShoot(Player player, GameTime gameTime, ContentManager content, Point point)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - player.ShotTime >= player.ShotLatency)
            {
                var bullet = new Bullet(player.Location, player, point);
                bullet.Texture = content.Load<Texture2D>("bullet");
                player.ShotTime = gameTime.TotalGameTime.TotalMilliseconds;
                return bullet;
            }
            return null;
        }
    }
}
