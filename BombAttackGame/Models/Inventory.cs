using BombAttackGame.Events;
using BombAttackGame.Interfaces;
using System;
using System.Collections.Generic;

namespace BombAttackGame.Models
{
    internal class Inventory
    {
        public IInventoryItem Slot1 { get; set; }
        public IInventoryItem Slot2 { get; set; }
        public IInventoryItem Slot3 { get; set; }
        public int SelectedSlot { get; set; }
        public IInventoryItem SelectedItem { get => GetSelectedItem(); }

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

        public List<IInventoryItem> InventoryItems { get; set; }
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
    }
}
