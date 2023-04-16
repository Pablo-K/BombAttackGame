using BombAttackGame.Models;

namespace BombAttackGame.Global
{
    internal class DealDamage
    {
        public static void DealDamageToPlayer(Player Player, int Damage)
        {
            Player.Hit(Damage);
        }
    }
}
