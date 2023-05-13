using BombAttackGame.Abstracts;
using BombAttackGame.Enums;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using BombAttackGame.Map;
using BombAttackGame.Models.HoldableObjects;
using BombAttackGame.Models.HoldableObjects.ThrowableObjects;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended.Collections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace BombAttackGame.Models
{
    public class Player : IGameObject
    {
        public Vector2 Location { get; set; }
        public Vector2 DLocation { get; set; }
        public Vector2 OldLocation { get; set; }
        public Point Position { get; set; }
        public Point OldPosition { get; set; }
        public List<IInventoryItem> Equipment { get; set; }
        public Vector2 ShootLocation { get; set; }
        public Direction Direction { get; set; }
        public Team Team { get; set; }
        public int Health { get; private set; }
        public double Speed { get; set; }
        public bool CanUseHoldableItem { get; private set; }
        public bool IsDead { get; set; }
        public bool IsFlashed { get; set; }
        public double MovingTime { get; set; }
        private double MovingEndTime { get; set; }
        private double UseHoldableItemBlockTime { get; set; }
        private double UseHoldableItemBlockLatency { get; set; }
        public Color Color { get; set; }
        public Queue<Enums.Events> Event { get; set; }
        public Rectangle Rectangle { get; set; }
        public bool IsHuman { get; set; }
        public int Ammo { get; set; }
        public int FlashTime { get; set; }
        public List<IGameObject> VisibleObjects { get; set; }
        public Inventory Inventory { get; set; }
        public Texture2D Texture { get; set; }
        public double Time { get; private set; }
        private double ChangeInventoryTime { get; set; }
        private double ChangeInventoryLatency { get; set; }
        private double WalkTextureTime { get; set; }
        private double WalkTextureTimeLatency { get; set; }
        public int BotGunChance { get; set; }
        public int BotNothingChance { get; set; }
        public int BotGrenadeChance { get; set; }
        public List<Direction> MoveList { get; set; }
        public Direction ErrorDirection { get; set; }
        public Direction PreviousDirection { get; set; }
        public bool ReadyToGo { get; set; }
        private bool isBotMove { get; set; }
        private List<Tile> MovingTiles { get; set; }
        private Tile PreviousTile { get; set; }

        public Player(Team team)
        {
            this.Team = team;
            this.Direction = Direction.Right;
            this.Speed = GameManager.PlayerSpeed;
            this.Health = 100;
            this.MovingTime = GameManager.BotMovingTime;
            this.Event = new Queue<Enums.Events>();
            this.IsHuman = false;
            this.VisibleObjects = new List<IGameObject>();
            this.Inventory = new Inventory();
            this.UseHoldableItemBlockLatency = 500;
            this.Texture = ContentContainer.PlayerTexture(this.Team);
            this.WalkTextureTimeLatency = 100;
            this.ChangeInventoryLatency = 300;
            this.BotNothingChance = GameManager.BotNothingChange;
            this.BotGunChance = GameManager.BotGunChance;
            this.BotGrenadeChance = GameManager.BotGrenadeChance;
            this.MoveList = new List<Direction>();
            this.MovingTiles = new List<Tile>();
        }
        public void ChangeTexture()
        {
            if (Time <= WalkTextureTime + WalkTextureTimeLatency) return;
            if (this.Texture.Name.ElementAt(this.Texture.Name.Length - 1) == '1') this.Texture = ContentContainer.PlayerTextureMove(this.Team, this.Direction, 1);
            else this.Texture = ContentContainer.PlayerTextureMove(this.Team, this.Direction, 0);
            this.WalkTextureTime = Time;
        }
        public void ChangeInventorySlot(int slot)
        {
            if (this.Time < this.ChangeInventoryTime + this.ChangeInventoryLatency) return;
            switch (slot)
            {
                case 1:
                    if (this.Inventory.Slot1 != null)
                    {
                        if (this.Inventory.SelectedSlot == 1)
                        {
                            this.Inventory.SelectNext(slot);
                        }
                        else
                        {
                            this.Inventory.Select(slot);
                        }
                        BlockUseHoldableItem();
                    }
                    break;
                case 2:
                    if (this.Inventory.Slot2 != null)
                    {
                        if (this.Inventory.SelectedSlot == 2)
                        {
                            this.Inventory.SelectNext(slot);
                        }
                        else
                        {
                            this.Inventory.Select(slot);
                        }
                        BlockUseHoldableItem();
                    }
                    break;
                case 3:
                    if (this.Inventory.Slot3 != null)
                    {
                        if (this.Inventory.SelectedSlot == 3)
                        {
                            this.Inventory.SelectNext(slot);
                        }
                        else
                        {
                            this.Inventory.Select(slot);
                        }
                        BlockUseHoldableItem();
                    }
                    break;
            }
            this.ChangeInventoryTime = this.Time;
        }
        public void BlockUseHoldableItem()
        {
            this.CanUseHoldableItem = false;
            this.UseHoldableItemBlockTime = this.Time;
        }
        private void UnblockUseHoldableItem()
        {
            if (!this.CanUseHoldableItem && this.Time >= this.UseHoldableItemBlockTime + this.UseHoldableItemBlockLatency)
            {
                this.CanUseHoldableItem = true;
            }
        }
        public void PlayerMove(Direction direction)
        {
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
            if (this.Inventory.SelectedItem is Grenade)
            {
                this.ShootLocation = shootLocation;
                this.Event.Enqueue(Enums.Events.Throw);
            }
        }

        public void Tick(GameTime gameTime, List<IGameObject> gameObjects, List<Rectangle> mapRectangle)
        {
            this.Time = gameTime.TotalGameTime.TotalMilliseconds;
            this.DLocation = new Vector2(this.Location.X, this.Location.Y - this.Texture.Height);
            this.OldPosition = this.Position;
            if (this.Direction == Direction.Left)
            { this.Position = new Point((int)(Location.X + this.Texture.Width) / 20, (int)this.Location.Y / 20); }
            if (this.Direction == Direction.Right)
            { this.Position = new Point((int)(Location.X - 1) / 20, (int)this.Location.Y / 20); }
            else if (this.Direction == Direction.Up)
            { this.Position = new Point((int)Location.X / 20, ((int)this.Location.Y + this.Texture.Height ) / 20); }
            else
            { this.Position = new Point((int)Location.X / 20, (int)this.Location.Y / 20); }
            UpdateRectangle();
            CheckIfDead();
            CheckInventory();
            CheckFlash();
            BotUseHoldable(gameTime);
            BotMove(gameTime);
            UnblockUseHoldableItem();
            UpdateObjectsVisibilityAsync(gameObjects, mapRectangle);
        }

        private void BotUseHoldable(GameTime gameTime)
        {
            if (this.IsHuman) return;
            if (this.IsFlashed) return;
            Random random = new Random();
            var x = random.Next(0, 100);
            if (x < BotNothingChance) return;
            if (this.Team == Team.Enemy)
            {
                var visible = VisibleObjects.OfType<Player>().Where(x => x.Team == Team.TeamMate);
                if (visible.Count() == 0) return;
                if (x < BotNothingChance + BotGunChance)
                {
                    var rand = visible.ElementAt(random.Next(0, visible.Count()));
                    UseSelectedItem(rand.Location);
                    if (this.Inventory.SelectedItem is Gun gun)
                    {
                        if (gun.Magazine == 0) gun.AddReloadEvent();
                    }
                }
                else if (x < BotNothingChance + BotGunChance + BotGrenadeChance)
                {
                    if (this.Inventory.Slot2 != null)
                    {
                        ChangeInventorySlot(2);
                        var rand = visible.ElementAt(random.Next(0, visible.Count()));
                        UseSelectedItem(rand.Location);
                    }
                }
            }
            if (this.Team == Team.TeamMate)
            {
                var visible = VisibleObjects.OfType<Player>().Where(x => x.Team == Team.Enemy);
                if (visible.Count() == 0) return;
                if (x < BotNothingChance + BotGunChance)
                {
                    var rand = visible.ElementAt(random.Next(0, visible.Count()));
                    UseSelectedItem(rand.Location);
                    if (this.Inventory.SelectedItem is Gun gun)
                    {
                        if (gun.Magazine == 0) gun.AddReloadEvent();
                    }
                }
                else if (x < BotNothingChance + BotGunChance + BotGrenadeChance)
                {
                    if (this.Inventory.Slot2 != null)
                    {
                        ChangeInventorySlot(2);
                        var rand = visible.ElementAt(random.Next(0, visible.Count()));
                        UseSelectedItem(rand.Location);
                    }
                }
            }
        }

        private void CheckFlash()
        {
            if (this.FlashTime <= this.Time)
            {
                this.IsFlashed = false;
            }
        }
        public void RemoveFromInventory(IInventoryItem inventoryItem)
        {
            switch (inventoryItem.InventorySlot)
            {
                case 1: this.Inventory.Slot1 = null; break;
                case 2: this.Inventory.Slot2 = null; break;
                case 3: this.Inventory.Slot3 = null; break;
            }
        }
        private void CheckInventory()
        {
            if (this.Inventory?.SelectedSlot == 2)
            {
                if (this.Inventory.Slot2 == null)
                {
                    {
                        this.Inventory.SelectNext(2);
                        ChangeInventorySlot(1);
                    }
                }
            }
            if (this.Inventory?.SelectedSlot == 3)
            {
                if (this.Inventory.Slot3 == null)
                {
                    ChangeInventorySlot(1);
                }
            }
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
            foreach (var obj in this.VisibleObjects)
            {
                if (obj.IsDead) this.VisibleObjects.Remove(obj);
            }
        }

        private bool CheckIfDead()
        {
            if (this.Health <= 0)
            {
                this.Event.Enqueue(Enums.Events.Dead);
                return true;
            }
            return false;
        }

        public void UpdateRectangle()
        {
            this.Rectangle = new Rectangle((int)Location.X, (int)Location.Y, this.Texture.Width, this.Texture.Height);
        }
        private void BotMove(GameTime gameTime)
        {
            if (this.MoveList.Count == 0) { ReadyToGo = false; }
            if (this.IsHuman) return;
            if (this.ReadyToGo)
            {
                if(this.Location == this.OldLocation)
                {
                    this.ReadyToGo = false;
                }
                if (this.MoveList.Count > 0)
                {
                    Point pos = new Point(this.MovingTiles.First().X, this.MovingTiles.First().Y);
                    if ((this.MoveList.FirstOrDefault() == Direction.Left || this.MoveList.FirstOrDefault() == Direction.Right)&& this.Position.X == pos.X) {
                        this.MoveList.RemoveAt(0);
                        this.MovingTiles.RemoveAt(0); }
                    if ((this.MoveList.FirstOrDefault() == Direction.Up || this.MoveList.FirstOrDefault() == Direction.Down) && this.Position.Y == pos.Y) {
                        this.MoveList.RemoveAt(0);
                        this.MovingTiles.RemoveAt(0); }
                    if (this.MoveList.Count == 0) return;
                    PlayerMove(this.MoveList.First());
                    OldLocation = this.Location;
                    return;
                }
                if(this.MoveList.Count < 5)
                {
                    Random rand = new();
                    if(rand.Next(0, 100) > 99) { this.ReadyToGo = false; }
                }
            }
            else
            {
                MoveList.Clear();
                MovingTiles.Clear();
                Random rand = new Random();
                Point point = new Point();
                if(this.Team == Team.TeamMate)
                { point = BotPoints.CTPoints[rand.Next(0, BotPoints.CTPoints.Count)]; }
                else
                { point = BotPoints.TTPoints[rand.Next(0, BotPoints.CTPoints.Count)]; }
                PathFinder.findPath(this.MoveList, this.MovingTiles, MapManager.MapString, new Point(this.Position.X, this.Position.Y - 1), point);
                if (this.MoveList.Count > 0) this.ReadyToGo = true;
            }
        }
    }
}
