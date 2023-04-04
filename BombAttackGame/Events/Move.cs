using BombAttackGame.Enums;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Events
{
    internal class Move
    {
        public static void PlayerMove(Player player, Direction direction, int width, int height)
        {
            Vector2 oldLoc = player.Location;
            if (direction == Direction.Left)
            {
                player.Location = new Vector2(player.Location.X - player.Speed, player.Location.Y);
                player.Direction = Direction.Left;
            }
            if (direction == Direction.Right)
            {
                player.Location = new Vector2(player.Location.X + player.Speed, player.Location.Y);
                player.Direction = Direction.Right;
            }
            if (direction == Direction.Down)
            {
                player.Location = new Vector2(player.Location.X, player.Location.Y + player.Speed);
                player.Direction = Direction.Down;
            }
            if (direction == Direction.Up)
            {
                player.Location = new Vector2(player.Location.X, player.Location.Y - player.Speed);
                player.Direction = Direction.Up;
            }
            if(player.Location.X < 0 || player.Location.Y < 0 || player.Location.X+player.Texture.Width > width || player.Location.Y+player.Texture.Height > height)
            {   
                player.Location = new Vector2(oldLoc.X, oldLoc.Y);
            }
        }
        public static void BulletsMove(List<Bullet> bullets, int width, int height, out bool remove, out int index)
        {
            index = -1;
            remove = false;
            foreach (var bullet in bullets)
            {
                int speed = bullet.Speed;
                if (bullet.TrajectoryIndex >= bullet.Trajectory.Count - 1) { index += 1; remove = true; break; }
                if (bullet.TrajectoryIndex >= bullet.Trajectory.Count) bullet.TrajectoryIndex = bullet.Trajectory.Count -1;
                bullet.Location = bullet.Trajectory.ElementAt(bullet.TrajectoryIndex);
                bullet.TrajectoryIndex += speed;
            }

        }
    }
}
