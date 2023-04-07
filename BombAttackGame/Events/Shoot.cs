using BombAttackGame.Enums;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BombAttackGame.Events
{
    internal class Shoot
    {
        public static Bullet PlayerShoot(Player player, GameTime gameTime, ContentManager content, Point point)
        {
            if (gameTime.TotalGameTime.TotalMilliseconds - player.ShotTime >= player.ShotLatency)
            {
                var bullet = new Bullet(player.Location, player, point);
                bullet.Texture = content.Load<Texture2D>("bullet");
                player.ShotTime = gameTime.TotalGameTime.TotalMilliseconds;
                return bullet;
            }
            return null;
        }
        public static List<Vector2> SetTrajectory(Vector2 PlayerLoc, Vector2 ShootLoc, int[] MapSize)
        {
            List<Vector2> Trajectory = new List<Vector2>();
            int xDiff = (int)PlayerLoc.X - (int)ShootLoc.X;
            int yDiff = (int)PlayerLoc.Y - (int)ShootLoc.Y;
            int width = MapSize[0];
            int height = MapSize[1];
            while(ShootLoc.X >= 0 && ShootLoc.Y >= 0 && ShootLoc.X <= width && ShootLoc.Y <= height)
            {
                ShootLoc.X -= xDiff;
                ShootLoc.Y -= yDiff;
            }
            
            int w = (int)ShootLoc.X - (int)PlayerLoc.X;
            int h = (int)ShootLoc.Y - (int)PlayerLoc.Y;
            int x = (int)PlayerLoc.X;
            int y = (int)PlayerLoc.Y;
            Vector2 d1 = new Vector2(0,0);
            Vector2 d2 = new Vector2(0,0);
            if (w < 0) d1.X = -1; else if (w > 0) d1.X = 1;
            if (h < 0) d1.Y = -1; else if (h > 0) d1.Y = 1;
            if (w < 0) d2.X = -1; else if (w > 0) d2.X = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) d2.Y = -1; else if (h > 0) d2.Y = 1;
                d2.X = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                Trajectory.Add(new Vector2(x,y));
                numerator += shortest;
                if (!(numerator < longest)) { numerator -= longest; x += (int)d1.X; y += (int)d1.Y; }
                else { x += (int)d2.X; y += (int)d2.Y; }
            }
            return Trajectory;
        }
    }
}
