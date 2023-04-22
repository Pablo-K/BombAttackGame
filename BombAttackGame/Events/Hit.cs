using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Events
{
    internal class Hit
    {
        public static bool BulletHit(Bullet bullet, List<IGameObject> GameObjects, out Player hitted) 
        {
            hitted = null;
            foreach (Player p in GameObjects.OfType<Player>())
            {
                if (InHitBox(bullet,p)) { hitted = p; return true; }
            }
            return false;
        }
        public static bool InHitBox(Bullet bullet, Player player)
        {
            if ( (Math.Abs(bullet.Location.X - player.Location.X) < player.Texture.Width) && 
                    (bullet.Location.Y - player.Location.Y <= player.Texture.Height) && 
                    (bullet.Location.Y >= player.Location.Y) &&
                    bullet.Owner != player)
                { return true; }
            return false;
        }
        public static bool InHitBox(Vector2 Location1, Vector2 Location2, Texture2D Texture)
        {
            if ( (Math.Abs(Location1.X - Location2.X) < Texture.Width) && 
                    (Location1.Y - Location2.Y <= Texture.Height) && 
                    (Location1.Y >= Location2.Y))
                { return true; }
            return false;
        }
    }
}
