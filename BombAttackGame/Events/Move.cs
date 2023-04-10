using BombAttackGame.Enums;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;

namespace BombAttackGame.Events
{
    internal class Move
    {
        public static void PlayerMove(Player player, Direction direction, int[] mapSize)
        {
            Vector2 oldLoc = player.Location;
            int width = mapSize[0];
            int height = mapSize[1];
            if (direction == Direction.Left)
            {
                player.Location = new Vector2(player.Location.X - (int)player.Speed, player.Location.Y);
                player.Direction = Direction.Left;
            }
            if (direction == Direction.Right)
            {
                player.Location = new Vector2(player.Location.X + (int)player.Speed, player.Location.Y);
                player.Direction = Direction.Right;
            }
            if (direction == Direction.Down)
            {
                player.Location = new Vector2(player.Location.X, player.Location.Y + (int)player.Speed);
                player.Direction = Direction.Down;
            }
            if (direction == Direction.Up)
            {
                player.Location = new Vector2(player.Location.X, player.Location.Y - (int)player.Speed);
                player.Direction = Direction.Up;
            }
            if(player.Location.X < 0 || player.Location.Y < 0 || player.Location.X+player.Texture.Width > width || player.Location.Y+player.Texture.Height > height)
            {   
                player.Location = new Vector2(oldLoc.X, oldLoc.Y);
            }
        }
    }
}
