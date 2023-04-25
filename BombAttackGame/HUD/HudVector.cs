using Microsoft.Xna.Framework;

namespace BombAttackGame.HUD
{
    internal class HudVector
    {
        public static Vector2 HpVector(int[] MapSize)
        {
            return new Vector2(MapSize[0]  - 30, MapSize[1] - 20);
        }
        public static Vector2 GunVector(int[] MapSize)
        {
            return new Vector2(MapSize[0]  - 80, MapSize[1] - 80);
        }
    }
}
