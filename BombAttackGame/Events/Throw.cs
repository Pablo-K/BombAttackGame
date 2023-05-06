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
            if (exp.GetType() == typeof(Grenade))
            {
                var grenade = (Grenade)exp;
                if(grenade.Type  == "handgrenade")
                {
                    var handGrenade = new HandGrenade(player.Location, player, point);
                    handGrenade.Texture = ContentContainer.HandGrenadeTexture;
                    handGrenade.Distance = Vector2.Distance(handGrenade.Point, handGrenade.Location);
                    player.RemoveFromInventory((IInventoryItem)exp);
                    return handGrenade;
                } 
                if(grenade.Type  == "flashgrenade")
                {
                    var flashGrenade = new FlashGrenade(player.Location, player, point);
                    flashGrenade.Texture = ContentContainer.FlashGrenadeTexture;
                    flashGrenade.Distance = Vector2.Distance(flashGrenade.Point, flashGrenade.Location);
                    player.RemoveFromInventory((IInventoryItem)exp);
                    return flashGrenade;
                } 
            }
            return null;
        }
    }
}
