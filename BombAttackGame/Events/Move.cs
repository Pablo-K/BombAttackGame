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
        public static void PlayerMove(Player player, Direction direction)
        {
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
        }
        public static void BulletsMove(List<Bullet> bullets)
        {
            foreach (var bullet in bullets)
            {
                if (bullet.Direction == Direction.Left) { bullet.Location = new Vector2(bullet.Location.X - bullet.Speed, bullet.Location.Y); }
                if (bullet.Direction == Direction.Up) { bullet.Location = new Vector2(bullet.Location.X, bullet.Location.Y - bullet.Speed); }
                if (bullet.Direction == Direction.Right) { bullet.Location = new Vector2(bullet.Location.X + bullet.Speed, bullet.Location.Y); }
                if (bullet.Direction == Direction.Down) { bullet.Location = new Vector2(bullet.Location.X, bullet.Location.Y + bullet.Speed); }
            }

        }
    }
}
