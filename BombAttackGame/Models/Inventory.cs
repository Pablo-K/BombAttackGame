using BombAttackGame.Events;
using BombAttackGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Models
{
    public class Inventory
    {
        public IInventoryItem Slot1 { get; set; }
        public IInventoryItem Slot2 { get; set; }
        public IInventoryItem Slot3 { get; set; }
        public int SelectedSlot { get; set; }
        public IInventoryItem SelectedItem { get => GetSelectedItem(); }
        public List<IInventoryItem> InventoryItems { get; set; }

        private IInventoryItem GetSelectedItem()
        {
            switch (SelectedSlot)
            {
                case 1: return Slot1;
                case 2: return Slot2;
                case 3: return Slot3;
                default: throw new Exception();
            }
        }

        public Inventory()
        {
            InventoryItems = new List<IInventoryItem>();
        }

        public void Equip(IInventoryItem inventoryItem)
        {
            var oldItem = inventoryItem.InventorySlot switch
            {
                1 => Slot1,
                2 => Slot2,
                3 => Slot3,
                _ => throw new ArgumentException()
            };

            if (oldItem is not null)
            {
                InventoryItems.Add(oldItem);
            }
            switch (inventoryItem.InventorySlot)
            {
                case 1: Slot1 = inventoryItem; break;
                case 2: Slot2 = inventoryItem; break;
                case 3: Slot3 = inventoryItem; break;
            }
        }
        public void Select(int slot)
        {
            this.SelectedSlot = slot;
        }
        public void SelectNext(int slot)
        {
            var oldItem = slot switch
            {
                1 => Slot1,
                2 => Slot2,
                3 => Slot3,
                _ => throw new ArgumentException()
            };
            if(oldItem is not null)
            {
                InventoryItems.Add(oldItem);
            }
            List<IInventoryItem> list = new List<IInventoryItem>();
            IInventoryItem newItem;
            switch (slot)
            {
                case 1:
                    list = new List<IInventoryItem>() { this.Slot1 };
                    if (!this.InventoryItems.Where(x => x.InventorySlot == 1).Except(list).Any())
                    {
                        this.InventoryItems.Remove(this.Slot1);
                        return;
                    }
                    newItem = this.InventoryItems.Where(x => x.InventorySlot == 1).Except(list).First();
                    this.Slot1 = this.InventoryItems.Where(x => x.InventorySlot == 1).Except(list).First();
                    this.InventoryItems.Remove(this.Slot1);
                    break;
                case 2:
                    list = new List<IInventoryItem>() { this.Slot2 };
                    if (!this.InventoryItems.Where(x => x.InventorySlot == 2).Except(list).Any())
                    {
                        this.InventoryItems.Remove(this.Slot2);
                        return;
                    }
                    newItem = this.InventoryItems.Where(x => x.InventorySlot == 2).Except(list).First();
                    this.Slot2 = this.InventoryItems.Where(x => x.InventorySlot == 2).Except(list).First();
                    this.InventoryItems.Remove(this.Slot2);
                    break;
                case 3:
                    list = new List<IInventoryItem>() { this.Slot3 };
                    if (!this.InventoryItems.Where(x => x.InventorySlot == 3).Except(list).Any())
                    {
                        this.InventoryItems.Remove(this.Slot3);
                        return;
                    }
                    newItem = this.InventoryItems.Where(x => x.InventorySlot == 3).Except(list).First();
                    this.Slot3 = this.InventoryItems.Where(x => x.InventorySlot == 3).Except(list).First();
                    this.InventoryItems.Remove(this.Slot3);
                    break;
            }
        }
    }
}
