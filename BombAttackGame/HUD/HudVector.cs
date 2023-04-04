using Microsoft.Xna.Framework;

namespace BombAttackGame.HUD
{
    internal class HudVector
    {
        public static Vector2 HpVector(int[] _mapSize)
        {
            return new Vector2(_mapSize[0] - 30, _mapSize[1] - 20);
        }
    }
}
