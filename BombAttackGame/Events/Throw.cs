using BombAttackGame.Abstracts;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using BombAttackGame.Models.HoldableObjects.ThrowableObjects;
using Microsoft.Xna.Framework;

namespace BombAttackGame.Events
{
    internal class Throw
    {
        public static Explosive PlayerThrow(Player player)
        {
            Explosive exp = (Explosive)player.Inventory.SelectedItem;
            var point = player.ShootLocation;
            if (player.Inventory.SelectedItem is HandGrenade handGrenade)
            {
                handGrenade.Location = new Vector2(player.Location.X, player.Location.Y);
                handGrenade.StartLocation = new Vector2(player.Location.X, player.Location.Y);
                handGrenade.Point = point;
                handGrenade.Texture = ContentContainer.HandGrenadeTexture;
                handGrenade.Distance = Vector2.Distance(point, handGrenade.Location);
                player.RemoveFromInventory((IInventoryItem)exp);
                return handGrenade;
            }
            if (player.Inventory.SelectedItem is FlashGrenade flashGrenade)
            {
                flashGrenade.Location = new Vector2(player.Location.X, player.Location.Y);
                flashGrenade.StartLocation = new Vector2(player.Location.X, player.Location.Y);
                flashGrenade.Point = point;
                flashGrenade.Texture = ContentContainer.FlashGrenadeTexture;
                flashGrenade.Distance = Vector2.Distance(point, flashGrenade.Location);
                player.RemoveFromInventory((IInventoryItem)exp);
                return flashGrenade;
            }
            return null;
        }
    }
}
