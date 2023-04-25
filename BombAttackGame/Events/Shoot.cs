using BombAttackGame.Global;
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
            bool canShoot = gameTime.TotalGameTime.TotalMilliseconds - player.ShotTime >= player.ShotLatency;

            if (canShoot == false) return null;

            var bullet = new Bullet(player.Location, player, point);
            bullet.Texture = ContentContainer.BulletTexture;
            bullet.Distance = Vector2.Distance(bullet.Point, bullet.Location);
            player.ShotTime = gameTime.TotalGameTime.TotalMilliseconds;
            return bullet;

        }
    }
}
