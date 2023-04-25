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
        private readonly List<IGameObject> _gameObjects;
        private readonly List<Rectangle> _mapCollisions;
        public int[] MapSize { get; set; }
        public ContentManager Content { get; set; }

        public EventProcessor(int[] MapSize, List<IGameObject> gameObjects, List<Rectangle> mapCollisions)
        {
            this.MapSize = MapSize;
            _gameObjects = gameObjects;
            _mapCollisions = mapCollisions;
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
                    if (InRectangle(Rectangle)) return;
                    player.Location = Location;
                    player.Direction = Direction.Left;
                    break;
                case Direction.Right:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(Rectangle)) return;
                    player.Location = Location;
                    player.Direction = Direction.Right;
                    break;
                case Direction.Up:
                    Location = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(Rectangle)) return;
                    player.Location = Location;
                    player.Direction = Direction.Up;
                    break;
                case Direction.Down:
                    Location = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(Rectangle)) return;
                    player.Location = Location;
                    player.Direction = Direction.Down;
                    break;
                case Direction.UpLeft:
                    Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(Rectangle))
                    {
                        Location = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                        Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                        if (InRectangle(Rectangle))
                        {
                            Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y);
                            Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                            if (InRectangle(Rectangle)) return;
                            player.Location = Location;
                            player.Direction = Direction.Left;
                        }
                        player.Location = Location;
                        player.Direction = Direction.Up;
                    }
                    player.Location = Location;
                    player.Direction = Direction.UpLeft;
                    break;
                case Direction.DownLeft:
                    Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(Rectangle))
                    {
                        Location = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                        Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                        if (InRectangle(Rectangle))
                        {
                            Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y);
                            Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                            if (InRectangle(Rectangle)) return;
                            player.Location = Location;
                            player.Direction = Direction.Left;
                        }
                        player.Location = Location;
                        player.Direction = Direction.Down;
                    }
                    player.Location = Location;
                    player.Direction = Direction.DownLeft;
                    break;
                case Direction.DownRight:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y + (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(Rectangle))
                    {
                        Location = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                        Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                        if (InRectangle(Rectangle))
                        {
                            Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                            Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                            if (InRectangle(Rectangle)) return;
                            player.Location = Location;
                            player.Direction = Direction.Right;
                        }
                        player.Location = Location;
                        player.Direction = Direction.Down;
                    }
                    player.Location = Location;
                    player.Direction = Direction.DownRight;
                    break;
                case Direction.UpRight:
                    Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y - (int)player.Speed);
                    Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(Rectangle))
                    {
                        Location = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                        Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                        if (InRectangle(Rectangle))
                        {
                            Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                            Rectangle = new Rectangle((int)Location.X, (int)Location.Y, player.Texture.Width, player.Texture.Height);
                            if (InRectangle(Rectangle)) return;
                            player.Location = Location;
                            player.Direction = Direction.Right;
                        }
                        player.Location = Location;
                        player.Direction = Direction.Up;
                    }
                    player.Location = Location;
                    player.Direction = Direction.UpRight;
                    break;
            }
        }
        private bool InRectangle(Rectangle rect)
        {
            foreach (var rec in _mapCollisions) { if (rect.Intersects(rec)) return true; }
            return false;

        }
        public void Move(Bullet Bullet)
        {
            var speed = Bullet.Speed * (1 / Bullet.Distance);
            float Length = 0;
            Bullet.Location = Vector2.Lerp(Bullet.Location, Bullet.Point, speed);
            Length += Vector2.Distance(Bullet.Location, Bullet.StartLocation);
            Bullet.DistanceTravelled += Length;
            foreach (var rec in _mapCollisions) { if (Bullet.Rectangle.Intersects(rec)) Bullet.Event.Enqueue(Event.ObjectHitted); }
        }
        public void TryShoot(GameTime gameTime, Player player, out Bullet bullet)
        {
            bullet = null;
            if (player == null) return;
            var shootLoc = VectorTool.ExtendVector(player.ShootLocation, player.Location, 100000);
            bullet = Shoot.PlayerShoot(player, gameTime, shootLoc);
            if (bullet == null) { return; }
            bullet.Direction = shootLoc - player.Location;
            bullet.Direction.Normalize();
            _gameObjects.Add(bullet);
        }

    }
}
