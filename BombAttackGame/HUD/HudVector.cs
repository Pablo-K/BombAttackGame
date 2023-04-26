using Microsoft.Xna.Framework;

namespace BombAttackGame.HUD
{
    internal class HudVector
    {
        private static int[] MapSize = { 1000, 1000 };
        public static Vector2 HpVector()
        {
            return new Vector2(MapSize[0]  - 30, MapSize[1] - 20);
        }
        public static Vector2 GunVector()
        {
            return new Vector2(MapSize[0]  - 80, MapSize[1] - 80);
        }
        public static Vector2 MagazineVector()
        {
            return new Vector2(MapSize[0]  - 100, MapSize[1] - 30);
        }
        public static Vector2 AmmoVector()
        {
            return new Vector2(MapSize[0]  - 60, MapSize[1] - 30);
        }
    }
}
