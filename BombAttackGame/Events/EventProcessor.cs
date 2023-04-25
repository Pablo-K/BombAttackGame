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
        private readonly List<IGameObject> _gameObjects;
        private readonly List<Rectangle> _mapCollisions;

        public EventProcessor(List<IGameObject> gameObjects, List<Rectangle> mapCollisions)
        {
            _gameObjects = gameObjects;
            _mapCollisions = mapCollisions;
        }

        public void Move(Player player)
        {
            Vector2 newLocation;
            Rectangle rectangle;
            switch (player.Direction)
            {
                case Direction.Left:
                    newLocation = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y);
                    rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(rectangle)) return;
                    player.Location = newLocation;
                    player.Direction = Direction.Left;
                    break;
                case Direction.Right:
                    newLocation = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                    rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(rectangle)) return;
                    player.Location = newLocation;
                    player.Direction = Direction.Right;
                    break;
                case Direction.Up:
                    newLocation = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                    rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(rectangle)) return;
                    player.Location = newLocation;
                    player.Direction = Direction.Up;
                    break;
                case Direction.Down:
                    newLocation = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                    rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(rectangle)) return;
                    player.Location = newLocation;
                    player.Direction = Direction.Down;
                    break;
                case Direction.UpLeft:
                    newLocation = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y - (int)player.Speed);
                    rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(rectangle))
                    {
                        newLocation = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                        rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                        if (InRectangle(rectangle))
                        {
                            newLocation = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y);
                            rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                            if (InRectangle(rectangle)) return;
                            player.Location = newLocation;
                            player.Direction = Direction.Left;
                        }
                        player.Location = newLocation;
                        player.Direction = Direction.Up;
                    }
                    player.Location = newLocation;
                    player.Direction = Direction.UpLeft;
                    break;
                case Direction.DownLeft:
                    newLocation = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y + (int)player.Speed);
                    rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(rectangle))
                    {
                        newLocation = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                        rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                        if (InRectangle(rectangle))
                        {
                            newLocation = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y);
                            rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                            if (InRectangle(rectangle)) return;
                            player.Location = newLocation;
                            player.Direction = Direction.Left;
                        }
                        player.Location = newLocation;
                        player.Direction = Direction.Down;
                    }
                    player.Location = newLocation;
                    player.Direction = Direction.DownLeft;
                    break;
                case Direction.DownRight:
                    newLocation = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y + (int)player.Speed);
                    rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(rectangle))
                    {
                        newLocation = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                        rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                        if (InRectangle(rectangle))
                        {
                            newLocation = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                            rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                            if (InRectangle(rectangle)) return;
                            player.Location = newLocation;
                            player.Direction = Direction.Right;
                        }
                        player.Location = newLocation;
                        player.Direction = Direction.Down;
                    }
                    player.Location = newLocation;
                    player.Direction = Direction.DownRight;
                    break;
                case Direction.UpRight:
                    newLocation = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y - (int)player.Speed);
                    rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                    if (InRectangle(rectangle))
                    {
                        newLocation = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                        rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                        if (InRectangle(rectangle))
                        {
                            newLocation = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                            rectangle = new Rectangle((int)newLocation.X, (int)newLocation.Y, player.Texture.Width, player.Texture.Height);
                            if (InRectangle(rectangle)) return;
                            player.Location = newLocation;
                            player.Direction = Direction.Right;
                        }
                        player.Location = newLocation;
                        player.Direction = Direction.Up;
                    }
                    player.Location = newLocation;
                    player.Direction = Direction.UpRight;
                    break;
            }
        }

        public void Move(Bullet bullet)
        {
            float speed = bullet.Speed * (1.0f / bullet.Distance);
            float length = 0;
            bullet.Location = Vector2.Lerp(bullet.Location, bullet.Point, speed);
            length += Vector2.Distance(bullet.Location, bullet.StartLocation);
            bullet.DistanceTravelled += length;
            foreach (var rec in _mapCollisions) {
                if (bullet.Rectangle.Intersects(rec)) bullet.Event.Enqueue(Event.ObjectHitted);
            }
        }

        public void TryShoot(GameTime gameTime, Player player)
        {
            Bullet bullet = null;
            if (player == null) return;
            var shootLoc = VectorTool.ExtendVector(player.ShootLocation, player.Location, 100000);
            bullet = Shoot.PlayerShoot(player, gameTime, shootLoc);
            if (bullet == null) { return; }
            bullet.Direction = shootLoc - player.Location;
            bullet.Direction.Normalize();
            _gameObjects.Add(bullet);
        }

        private bool InRectangle(Rectangle rect)
        {
            foreach (var rec in _mapCollisions)
            {
                if (rect.Intersects(rec)) return true;
            }
            return false;
        }

    }
}
