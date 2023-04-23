using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

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
        public static bool CheckLineIntersection(Vector2 player, Vector2 player2, List<Rectangle> Rectangle)
        {
            player.Y++;
            player.X++;
            player2.X++;
            player2.Y++;
            foreach (var rect in Rectangle)
            {
                Vector2 topLeft = new Vector2(rect.Left, rect.Top);
                Vector2 topRight = new Vector2(rect.Right, rect.Top);
                Vector2 bottomLeft = new Vector2(rect.Left, rect.Bottom);
                Vector2 bottomRight = new Vector2(rect.Right, rect.Bottom);

                if (LineIntersectsLine(player, player2, topLeft, topRight))
                    return true;
                if (LineIntersectsLine(player, player2, topRight, bottomRight))
                    return true;
                if (LineIntersectsLine(player, player2, bottomRight, bottomLeft))
                    return true;
                if (LineIntersectsLine(player, player2, bottomLeft, topLeft))
                    return true;
            }

            return false;
        }
        private static bool LineIntersectsLine(Vector2 line1Start, Vector2 line1End, Vector2 line2Start, Vector2 line2End)
        {
            float denominator = ((line2End.Y - line2Start.Y) * (line1End.X - line1Start.X)) - ((line2End.X - line2Start.X) * (line1End.Y - line1Start.Y));
            if (denominator == 0)
                return false;

            float ua = (((line2End.X - line2Start.X) * (line1Start.Y - line2Start.Y)) - ((line2End.Y - line2Start.Y) * (line1Start.X - line2Start.X))) / denominator;
            float ub = (((line1End.X - line1Start.X) * (line1Start.Y - line2Start.Y)) - ((line1End.Y - line1Start.Y) * (line1Start.X - line2Start.X))) / denominator;

            if (ua < 0 || ua > 1 || ub < 0 || ub > 1)
                return false;

            return true;
        }
    }
}
