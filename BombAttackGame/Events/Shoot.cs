using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace BombAttackGame.Events
{
    internal class Shoot
    {
        public static Bullet PlayerShoot(Player player, GameTime gameTime, Vector2 point)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - player.ShotTime >= player.ShotLatency)
            { 
                var gun = player.HoldingObject as IGun;
                var bullet = new Bullet(player.Location, player, point, gun.Damage);
                bullet.Texture = content.Load<Texture2D>("bullet");
                bullet.Distance = Vector2.Distance(bullet.Point, bullet.Location);
                player.ShotTime = gameTime.TotalGameTime.TotalMilliseconds;
                return bullet;
            }
            return null;
        }
    }
}
