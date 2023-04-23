using BombAttackGame.Interfaces;
using System.Collections.Generic;

namespace BombAttackGame.Global
{
    internal class DeleteObject
    {
        public static List<IGameObject> FromGameObjects(IGameObject GameObject, List<IGameObject> GameObjects)
        {
            GameObjects.Remove(GameObject);
            return GameObjects;
        }
    }
}
