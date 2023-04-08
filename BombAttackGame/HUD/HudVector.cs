using Microsoft.Xna.Framework;

namespace BombAttackGame.HUD
{
    internal class HudVector
    {
        public static Vector2 HpVector(GameWindow Graphics)
        {
            return new Vector2(Graphics.ClientBounds.Width- 30, Graphics.ClientBounds.Height- 20);
        }
    }
}
