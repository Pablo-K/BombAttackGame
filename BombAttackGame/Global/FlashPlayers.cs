using BombAttackGame.Models;

namespace BombAttackGame.Global
{
    internal class FlashPlayers
    {
        public static void Flash(Player player, int time)
        {
            player.IsFlashed = true;
            player.FlashTime = time;
        }
    }
}
