using BombAttackGame.Models;

namespace BombAttackGame.Global
{
    internal class DealDamage
    {
        public static void DealDamageToPlayer(Player Player, Bullet Bullet)
        {
            int Damage = 0;
            if (Player.Team == Bullet.Owner.Team) Damage = (int)(Bullet.DamageDealt * Bullet.TeamDamage);
            if (Player.Team == Team.Enemy && (Bullet.Owner.Team == Team.TeamMate || Bullet.Owner.Team == Team.Player)) Damage = (int)(Bullet.DamageDealt * Bullet.EnemyDamage);
            if (Player.Team == Team.TeamMate && Bullet.Owner.Team == Team.Player)  Damage = (int)(Bullet.DamageDealt * Bullet.TeamDamage);
            Player.Hit(Damage);
        }
    }
}
