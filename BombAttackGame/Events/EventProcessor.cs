using BombAttackGame.Collisions;
using BombAttackGame.Models;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;

namespace BombAttackGame.Events
{
    internal class EventProcessor
    {
        public int[] MapSize { get; set; }
        public Collision Collision { get; set; }
        public GameTime GameTime { get; set; }
        public List<Bullet> Bullets { get; set; }
        public ContentManager Content { get; set; }
        public EventProcessor(int[] MapSize)
        {
            this.MapSize = MapSize;
        }
        public void Move(Player player)
        {
            if(player == null) return;
            if (Collision.CheckCollision(player.Collision)) player.Location = player.OldLocation;
        }
        public void TryShoot(Player player)
        {
            if (player == null) return;
            Bullet Bullet = null;
            var ShootLoc = VectorTool.ExtendVector(player.ShootLocation, player.Location, 100000);
            Bullet = Shoot.PlayerShoot(player, GameTime, Content, ShootLoc);
            if (Bullet == null) { return; }
            Bullets.Add(Bullet);
            Bullet.Direction = ShootLoc - player.Location;
            Bullet.Direction.Normalize();
        }
        public void Update(Collision Collision, GameTime GameTime, List<Bullet> Bullets, ContentManager Content)
        {
            this.Collision = Collision;
            this.GameTime = GameTime;
            this.Bullets = Bullets;
            this.Content = Content;
        }
    }
}
