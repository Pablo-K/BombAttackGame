using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace BombAttackGame.Interfaces
{
    public interface IOnGroundItem : IInventoryItem
    {
        public Enums.Events Event { get;  set; }
        public Point Position { get; set; }
        public Vector2 Location { get; set; }
    }
}
