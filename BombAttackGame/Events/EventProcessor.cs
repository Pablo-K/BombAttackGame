using BombAttackGame.Collisions;
using BombAttackGame.Enums;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Collections.Generic;
using System.Linq;

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
                    foreach (var rec in MapCollision)
                    {
                        if (Rectangle.Intersects(rec)) return;
                    }
                    player.Location = Location;
                    player.Direction = Direction.Left;
                    break;
                case Direction.Right:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision)
                    {
                        if (Rectangle.Intersects(rec)) return;
                    }
                    player.Location = Location;
                    player.Direction = Direction.Right;
                    break;
                case Direction.Up:
                    Location = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision)
                    {
                        if (Rectangle.Intersects(rec)) return;
                    }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.Down:
                    Location = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision)
                    {
                        if (Rectangle.Intersects(rec)) return;
                    }
                    player.Location = Location;
                    player.Direction = Direction.Down;
                    break;
                case Direction.UpLeft:
                    Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision)
                    {
                        if (Rectangle.Intersects(rec)) return;
                    }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.DownLeft:
                    Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision)
                    {
                        if (Rectangle.Intersects(rec)) return;
                    }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.DownRight:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision)
                    {
                        if (Rectangle.Intersects(rec)) return;
                    }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.UpRight:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    foreach (var rec in MapCollision)
                    {
                        if (Rectangle.Intersects(rec)) return;
                    }
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.None:
                    break;
                default:
                    break;
            }
            if (player.Direction == Direction.Left)
            {
            }
            if (player.Direction == Direction.Right)
            {
            }
            if (player.Direction == Direction.Down)
            {
            }
            if (player.Direction == Direction.Up)
            {
            }
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
