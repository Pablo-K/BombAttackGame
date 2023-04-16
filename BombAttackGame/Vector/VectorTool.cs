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
        public static List<Vector2> Collision(Vector2 Location, Texture2D Texture)
        {
            List<Vector2> Collision = new List<Vector2>();
            for(int i = 0; i < Texture.Width; i++)
            {
                for(int j = 0; j < Texture.Height; j++)
                {
                    Collision.Add(new Vector2(Location.X + (float)i,Location.Y + (float)j));
                }
            }
            return Collision;
        }
        public static bool IsOnObject(List<Vector2> Collistion1, List<Vector2> Collision2)
        {
            if (Collistion1.Intersect(Collision2).Any())
            {
                return true;
            }
            return false;
        }
    }
}
