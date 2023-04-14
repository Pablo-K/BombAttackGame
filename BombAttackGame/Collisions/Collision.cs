using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Collisions
{
    internal class Collision
    {
        public List<IGameObject> GameObjectCollision = new List<IGameObject>();
        public Collision()
        { 
        }
        public void AddCollision(IGameObject GameObject)
        {
            GameObjectCollision.Add(GameObject);
        }
        //public bool CheckCollision(Rectangle Rectangle)
        //{
        //    return AllCollisions.Intersect(Rectangle);
        //}
    }
}
