using BombAttackGame.Abstracts;
using BombAttackGame.Bonuses;
using BombAttackGame.Enums;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using BombAttackGame.Models.HoldableObjects;
using BombAttackGame.Models.HoldableObjects.ThrowableObjects;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace BombAttackGame.Models
{
    internal class Player : IGameObject
    {
        public Vector2 Location { get; set; }
        public List<Vector2> OldLocation { get; set; }
        public List<IInventoryItem> Equipment { get; set; }
        public Vector2 ShootLocation { get; set; }
        public Direction Direction { get; set; }
        public Team Team { get; set; }
        public int Health { get; private set; }
        public double Speed { get; set; }
        public bool CanUseHoldableItem { get; private set; }
        public bool IsDead { get; set; }
        public bool IsAttacked { get; set; }
        public bool OnMainSpeed { get; private set; }
        public double ShotTime { get; set; }
        public double MainSpeedStartTime { get; private set; }
        public double MainSpeedEndTime { get; private set; }
        private double MovingTime { get; set; }
        private double MovingEndTime { get; set; }
        private double UseHoldableItemBlockTime { get; set; }
        private double UseHoldableItemBlockLatency { get; set; }
        public Color Color { get; set; }
        public Queue<Enums.Events> Event { get; set; }
        public Rectangle Rectangle { get; set; }
        public bool IsHuman { get; set; }
        public int Ammo { get; set; }
        public List<IGameObject> VisibleObjects { get; set; }
        public Inventory Inventory { get; set; }
        public Texture2D Texture { get => ContentContainer.PlayerTexture(this.Team); set { } }
        public double Time { get; private set; }

        public Player()
        {
            this.Direction = Direction.Right;
            this.Speed = 2;
            this.Health = 100;
            this.MovingTime = 1000;
            this.OnMainSpeed = false;
            this.OldLocation = new List<Vector2>();
            this.Event = new Queue<Enums.Events>();
            this.IsHuman = false;
            this.VisibleObjects = new List<IGameObject>();
            this.Inventory = new Inventory();
            this.UseHoldableItemBlockLatency = 500;
        }
        public void ChangeInventorySlot(int slot)
        {
            switch (slot)
            {
                case 1:
                    if (this.Inventory.Slot1 != null) { this.Inventory.SelectedSlot = slot; BlockUseHoldableItem(); } break;
                case 2:
                    if (this.Inventory.Slot2 != null) { this.Inventory.SelectedSlot = slot; BlockUseHoldableItem(); } break;
                case 3:
                    if (this.Inventory.Slot3 != null) { this.Inventory.SelectedSlot = slot; BlockUseHoldableItem(); } break;
            }
        }
        public void BlockUseHoldableItem()
        {
            this.CanUseHoldableItem = false;
            this.UseHoldableItemBlockTime = this.Time;
        }
        private void UnblockUseHoldableItem()
        {
            if(!this.CanUseHoldableItem && this.Time >= this.UseHoldableItemBlockTime + this.UseHoldableItemBlockLatency)
            {
                this.CanUseHoldableItem = true;
            }
        }
        public void PlayerMove(Direction direction)
        {
            OldLocation.Add(Location);
            this.Direction = direction;
            if (!this.Event.Contains(Enums.Events.Move)) { Event.Enqueue(Enums.Events.Move); }
        }

        public void Hit(int damage)
        {
            Health -= damage;
            if (Health <= 0)
            {
                this.IsDead = true;
            }
        }

        public void UseSelectedItem(Vector2 shootLocation)
        {
            if (this.Inventory.SelectedItem == null) return;
            if (this.Inventory.SelectedItem.GetType() == typeof(Sheriff))
            {
                this.ShootLocation = shootLocation;
                if (!this.Event.Contains(Enums.Events.TryShoot)) { this.Event.Enqueue(Enums.Events.TryShoot); }
            }
            if (this.Inventory.SelectedItem.GetType() == typeof(Grenade))
            {
                this.ShootLocation = shootLocation;
                this.Event.Enqueue(Enums.Events.Throw);
            }
        }

        public void Tick(GameTime gameTime, List<IGameObject> gameObjects, List<Rectangle> mapRectangle)
        {
            this.Time = gameTime.TotalGameTime.TotalMilliseconds;
            UpdateRectangle();
            UpdateColor(Color);
            CheckIfDead();
            //BotMove(gameTime);
            UnblockUseHoldableItem();
            UpdateObjectsVisibilityAsync(gameObjects, mapRectangle);
        }
        private async Task UpdateObjectsVisibilityAsync(List<IGameObject> gameObjects, List<Rectangle> mapRectangle)
        {
            foreach (var obj in gameObjects)
            {
                bool intersects = VectorTool.CheckLineIntersection(this.Location, obj.Location, mapRectangle);
                if (!intersects)
                {
                    if (!this.VisibleObjects.Contains(obj)) this.VisibleObjects.Add(obj);
                }
                else
                {
                    if (this.VisibleObjects.Contains(obj)) this.VisibleObjects.Remove(obj);
                }
            }
        }
       
        private bool CheckIfDead()
        {
            if (this.Health <= 0) return true;
            return false;
        }

        public void UpdateRectangle()
        {
            this.Rectangle = new Rectangle((int)Location.X, (int)Location.Y, this.Texture.Width, this.Texture.Height);
        }

        public static void MainSpeedTime(Player Player, GameTime GameTime, MainSpeed MainSpeed)
        {
            if (Player.MainSpeedEndTime <= GameTime.TotalGameTime.TotalMilliseconds && Player.OnMainSpeed)
            {
                Player.Speed /= MainSpeed.WalkSpeed;
                Player.OnMainSpeed = false;
            }
        }

        public void UpdateColor(Color color)
        {
            if (OnMainSpeed)
            {
                this.Color = Color.GreenYellow;
            }
            else
            {
                this.Color = color;
            }

        }

        public void BotMove(GameTime gameTime)
        {
            if (this.IsHuman) return;
            if (gameTime.TotalGameTime.TotalMilliseconds >= MovingEndTime)
            {
                Random random = new();
                int x = random.Next(0, 8);
                var Dir = (Direction)x;
                MovingEndTime = gameTime.TotalGameTime.TotalMilliseconds + MovingTime;
                PlayerMove(Dir);
                return;
            }
            PlayerMove(Direction);
        }

    }
}
