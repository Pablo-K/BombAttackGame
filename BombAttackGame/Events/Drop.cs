using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using BombAttackGame.Models.HoldableObjects;
using System.Linq;

namespace BombAttackGame.Events
{
    internal class Drop
    {
        public static Bomb DropBomb(Player player)
        {
            Bomb bomb = (Bomb)player.Inventory.InventoryItems.Where(x => x is Bomb).FirstOrDefault();
            if(bomb == null)
            {
                if(player.Inventory.SelectedItem is Bomb) bomb = (Bomb)player.Inventory.SelectedItem;
            }
            if(bomb == null)
            {
                bomb = (Bomb)(player.Inventory.Slot4);
            }
            if (bomb == null) return new Bomb();
            
            bomb.Location = player.Location;
            return bomb;
        }
    }
}
