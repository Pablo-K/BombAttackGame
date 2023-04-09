using Microsoft.Xna.Framework;

namespace BombAttackGame.Vector
{
    internal class VectorTool
    {
        public static Vector2 ExtendVector(Vector2 xVector, Vector2 xVector2, float xDistance)
        {
            float pDistance;
            Vector2 VectorEnd;
            pDistance = (float)Vector2.Distance(xVector, xVector2);
            VectorEnd = new Vector2();
            VectorEnd.X = xVector.X + (xVector.X - xVector2.X) / pDistance * xDistance;
            VectorEnd.Y = xVector.Y + (xVector.Y - xVector2.Y) / pDistance * xDistance;
            return VectorEnd;
        }
    }
}
