using BombAttackGame.Enums;
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
        public List<Rectangle> MapCollision { get; set; }
        public GameTime GameTime { get; set; }
        public List<IGameObject> GameObjects { get; set; }
        public ContentManager Content { get; set; }
        public EventProcessor(int[] MapSize)
        {
            this.MapSize = MapSize;
        }
        public void Move(Player player)
        {
            Vector2 Location;
            Rectangle Rectangle;
            switch (player.Direction)
            {
                case Direction.Left:
                    Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision) { if (Rectangle.Intersects(rec)) return; }
                    player.Location = Location;
                    player.Direction = Direction.Left;
                    break;
                case Direction.Right:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision) { if (Rectangle.Intersects(rec)) return; }
                    player.Location = Location;
                    player.Direction = Direction.Right;
                    break;
                case Direction.Up:
                    Location = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision) { if (Rectangle.Intersects(rec)) return; }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.Down:
                    Location = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision) { if (Rectangle.Intersects(rec)) return; }
                    player.Location = Location;
                    player.Direction = Direction.Down;
                    break;
                case Direction.UpLeft:
                    Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision) { if (Rectangle.Intersects(rec)) return; }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.DownLeft:
                    Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision) { if (Rectangle.Intersects(rec)) return; }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.DownRight:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision) { if (Rectangle.Intersects(rec)) return; }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.UpRight:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision) { if (Rectangle.Intersects(rec)) return; }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
            }
        }
        public void Move(Bullet Bullet)
        {
            var speed = Bullet.Speed * (1 / Bullet.Distance);
            float Length = 0;
            Bullet.Location = Vector2.Lerp(Bullet.Location, Bullet.Point, speed);
            Length += Vector2.Distance(Bullet.Location, Bullet.StartLocation);
            Bullet.DistanceTravelled += Length;
            foreach (var rec in MapCollision) { if (Bullet.Rectangle.Intersects(rec)) Bullet.Event.Enqueue(Event.ObjectHitted); }
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
        public void Update(List<Rectangle> MapCollision, GameTime GameTime, List<IGameObject> GameObjects, ContentManager Content)
        {
            this.MapCollision = MapCollision;
            this.GameTime = GameTime;
            this.GameObjects = GameObjects;
            this.Content = Content;
        }
    }
}
