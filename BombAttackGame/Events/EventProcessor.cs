﻿using BombAttackGame.Abstracts;
using BombAttackGame.Draw;
using BombAttackGame.Enums;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using BombAttackGame.Models.HoldableObjects.ThrowableObjects;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace BombAttackGame.Events
{
    internal class EventProcessor
    {
        private readonly List<IGameObject> _gameObjects;
        private readonly List<IGameSprite> _sprites;
        private readonly List<Rectangle> _mapCollisions;
        private readonly List<IHoldableObject> _holdableObjects;
        private List<Animation> _animations;
        private readonly GameTime _gameTime;

        public EventProcessor(List<IGameObject> gameObjects, List<Rectangle> mapCollisions, GameTime gameTime, List<IGameSprite> sprites, List<IHoldableObject> holdableObjects, List<Animation> animations)
        {
            _gameObjects = gameObjects;
            _mapCollisions = mapCollisions;
            _gameTime = gameTime;
            _sprites = sprites;
            _holdableObjects = holdableObjects;
            _animations = animations;
        }

        public void ProcessEvents()
        {
            foreach (var gameObject in _gameObjects.ToList())
            {
                if (gameObject.Event is null) continue;
                while (gameObject.Event.TryDequeue(out Enums.Events e))
                {
                    ProcessEvent(gameObject, e);
                }
            }
            foreach (var holdableObject in _holdableObjects.ToList())
            {
                if (holdableObject.Event is null) continue;
                while (holdableObject.Event.TryDequeue(out Enums.Events e))
                {
                    ProvessEvent(holdableObject, e);
                }
            }
        }

        private void ProcessEvent(IGameObject gameObject, Enums.Events e)
        {
            switch (e)
            {
                case Enums.Events.Move:
                    Move(gameObject);
                    break;
                case Enums.Events.TryShoot:
                    TryShoot(gameObject);
                    break;
                case Enums.Events.Throw:
                    ThrowItem(gameObject);
                    break;
                case Enums.Events.ObjectHitted:
                    ObjectHitted(gameObject);
                    break;
                case Enums.Events.Shoot:
                    break;
                case Enums.Events.Delete:
                    Delete(gameObject);
                    break;
                case Enums.Events.Explode:
                    Explode(gameObject);
                    break;
            }
        }

        private void ThrowItem(IGameObject gameObject)
        {
            Explosive item = Throw.PlayerThrow((Player)gameObject);
            if (item is HandGrenade)
            {
                HandGrenade handGrenade = (HandGrenade)item;
                handGrenade.StartTime = _gameTime.TotalGameTime.TotalMilliseconds;
                _gameObjects.Add(handGrenade);
            }
        }
        private void Explode(IGameObject gameObject)
        {
            switch (gameObject)
            {
                case HandGrenade handGrenade:
                    DealDamageAround(handGrenade);
                    _animations.Add(new Animation(AnimationsContainer.HandGrenadeBoom, handGrenade.Location));
                    break;
            }
        }
        private void DealDamageAround(HandGrenade handGrenade)
        {
            foreach (var obj in _gameObjects.OfType<Player>())
            {
                int damage = handGrenade.CalculateDamage(obj);
                if (damage > 0)
                {
                    DealDamage.DealDamageToPlayer(obj, damage);
                    CreateDamage(handGrenade, obj, _gameTime);
                }
            }
        }

        private void ProvessEvent(IHoldableObject obj, Enums.Events e)
        {
            switch (e)
            {
                case Enums.Events.Reload:
                    var o = obj as Gun;
                    o.Reload();
                    break;
            }
        }

        private void Move(IGameObject gameObject)
        {
            switch (gameObject)
            {
                case Player player: Move(player); break;
                case Bullet bullet: Move(bullet); break;
                case HandGrenade handgrenade: Move(handgrenade); break;
            }
        }

        private void Move(Player player)
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
                player.ChangeTexture();
        }

        private void Move(Bullet bullet)
        {
            float speed = bullet.Speed * (1.0f / bullet.Distance);
            float length = 0;
            bullet.Location = Vector2.Lerp(bullet.Location, bullet.Point, speed);
            length += Vector2.Distance(bullet.Location, bullet.StartLocation);
            bullet.DistanceTravelled += length;
            foreach (var rec in _mapCollisions)
            {
                if (bullet.Rectangle.Intersects(rec)) bullet.Event.Enqueue(Enums.Events.ObjectHitted);
            }
        }
        private void Move(HandGrenade handGrenade)
        {
            float speed = handGrenade.Speed * (1.0f / handGrenade.Distance);
            float length = 0;
            handGrenade.Location = Vector2.Lerp(handGrenade.Location, handGrenade.Point, speed);
            length += Vector2.Distance(handGrenade.Location, handGrenade.StartLocation);
            handGrenade.DistanceTravelled += length;
            foreach (var rec in _mapCollisions)
            {
                if (handGrenade.Rectangle.Intersects(rec))
                    handGrenade.Event.Enqueue(Enums.Events.ObjectHitted);
            }
        }

        private void TryShoot(IGameObject gameObject)
        {
            switch (gameObject)
            {
                case Player player: TryShoot(player); break;
            }
        }

        private void TryShoot(Player player)
        {
            Bullet bullet = null;
            if (player == null) return;
            var shootLoc = VectorTool.ExtendVector(player.ShootLocation, player.Location, 100000);
            bullet = Shoot.PlayerShoot(player, _gameTime, shootLoc);
            if (bullet == null) { return; }
            bullet.Direction = shootLoc - player.Location;
            bullet.Direction.Normalize();
            _gameObjects.Add(bullet);
        }

        private void ObjectHitted(IGameObject gameObject)
        {
            if (gameObject is Bullet bullet) ObjectHitted(bullet);
            if (gameObject is HandGrenade hgrenade) ObjectHitted(hgrenade);
        }

        private void ObjectHitted(Bullet bullet)
        {
            if (bullet.ObjectHitted?.GetType() == typeof(Player))
            {
                DealDamage.DealDamageToPlayer(bullet.ObjectHitted as Player, bullet.DamageDealt);
                CreateDamage(bullet, _gameTime);
            }
            DeleteObject.FromGameObjects(bullet, _gameObjects);
        }

        private void ObjectHitted(HandGrenade hGrenade)
        {

        }

        private void Delete(IGameObject gameObject)
        {
            DeleteObject.FromGameObjects(gameObject, _gameObjects);
        }

        private void CreateDamage(Bullet bullet, GameTime gameTime)
        {
            Damage Damage = new Damage(bullet.DamageDealt, bullet.Location);
            Damage.Font = ContentContainer.DamageFont;
            Damage.Location = bullet.Location;
            Damage.ShowTime = gameTime.TotalGameTime.TotalMilliseconds;
            _sprites.Add(Damage);
        }
        private void CreateDamage(HandGrenade handGrenade, Player player, GameTime gameTime)
        {
            Damage Damage = new Damage(handGrenade.DamageDealt, player.Location);
            Damage.Font = ContentContainer.DamageFont;
            Damage.Location = player.Location;
            Damage.ShowTime = gameTime.TotalGameTime.TotalMilliseconds;
            _sprites.Add(Damage);
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
