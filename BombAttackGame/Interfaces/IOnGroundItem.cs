using System.Collections.Generic;

namespace BombAttackGame.Interfaces
{
    public interface IOnGroundItem : IInventoryItem
    {
        public Queue<Enums.Events> Event { get;  set; }
    }
}
