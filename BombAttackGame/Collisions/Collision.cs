using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Collisions
{
    internal class Collision
    {
        public HashSet<Vector2> AllCollisions = new HashSet<Vector2>();
        public Collision()
        { 
        }

        public void AddCollision(Vector2 Collision)
        {
            if (!AllCollisions.Contains(Collision)) AllCollisions.Add(Collision);
        }
        public void AddCollision(List<Vector2> Collision)
        {
            List<Vector2> NCollision = new List<Vector2>();
            NCollision = Collision.Except(AllCollisions).ToList();
            AllCollisions.UnionWith(NCollision);
        }
        public void AddCollision(Vector2 Location, Texture2D Texture)
        {
            //HashSet<Vector2> Collision = (VectorTool.CollisionH(Location, Texture));
            //foreach (var item in Collision)
            //{
            //    if(!AllCollisions.Contains(item)) AllCollisions.Union(item);
            //}
        }
        public void RemoveCollision(Vector2 Location)
        {
            AllCollisions.Remove(Location);
        }
        public void RemoveCollision(List<Vector2> Collision)
        {
            foreach(Vector2 Item in Collision)
            {
                AllCollisions.Remove(Item);
            }
        }
        public bool CheckCollision(List<Vector2> Collision)
        {
            return AllCollisions.Intersect(Collision).Any();
        }
        public bool CheckCollision(Vector2 Location)
        {
            return AllCollisions.Contains(Location);
        }
        public bool CheckCollision(Vector2 Location, Texture2D Texture)
        {
            List<Vector2> Collision = new List<Vector2>();
            Collision.AddRange(VectorTool.Collision(Location, Texture));
            return AllCollisions.Intersect(Collision).Any();
        }
    }
}
