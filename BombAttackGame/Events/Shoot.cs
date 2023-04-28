using BombAttackGame.Abstracts;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;

namespace BombAttackGame.Events
{
    internal class Shoot
    {
        public static Bullet PlayerShoot(Player player, GameTime gameTime, Vector2 point)
        {
            Gun gun = (Gun)player.Inventory.SelectedItem;
            if (!CanShoot(gameTime,gun)) return null;
            var bullet = new Bullet(player.Location, player, point, gun.Damage);
            bullet.Texture = ContentContainer.BulletTexture;
            bullet.Distance = Vector2.Distance(bullet.Point, bullet.Location);
            gun.ShotTime = gameTime.TotalGameTime.TotalMilliseconds;
            gun.Magazine -= 1;
            return bullet;

        }

        private static bool CanShoot(GameTime gameTime, Gun gun) {
            if (gun is null) return false;
            double shotTimeMs = gun.ShotTime;
            double gameTimeMs = gameTime.TotalGameTime.TotalMilliseconds;
            bool isOnCooldown = gameTimeMs - shotTimeMs < gun.Latency;
            bool hasAmmo = gun.Magazine > 0;
            bool canShoot = hasAmmo && isOnCooldown == false;
            return canShoot;
        }
    }
}
