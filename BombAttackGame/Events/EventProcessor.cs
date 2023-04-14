using BombAttackGame.Collisions;
using BombAttackGame.Interfaces;
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
        public List<IGameObject> GameObjects { get; set; }
        public ContentManager Content { get; set; }
        public EventProcessor(int[] MapSize)
        {
            this.MapSize = MapSize;
        }
        public void Move(Player player)
        {
            if(player == null) return;
        }
        public void TryShoot(Player player, out Bullet Bullet)
        {
            Bullet = null;
            if (player == null) return;
            var ShootLoc = VectorTool.ExtendVector(player.ShootLocation, player.Location, 100000);
            Bullet = Shoot.PlayerShoot(player, GameTime, Content, ShootLoc);
            if (Bullet == null) { return; }
            Bullet.Direction = ShootLoc - player.Location;
            Bullet.Direction.Normalize();
        }
        public void Update(Collision Collision, GameTime GameTime, List<IGameObject> GameObjects, ContentManager Content)
        {
            this.Collision = Collision;
            this.GameTime = GameTime;
            this.GameObjects = GameObjects;
            this.Content = Content;
        }
    }
}
