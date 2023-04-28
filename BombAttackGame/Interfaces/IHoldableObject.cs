using System.Collections.Generic;

namespace BombAttackGame.Interfaces
{
    public interface IHoldableObject
    {
        public Queue<Enums.Events> Event { get;  set; }
    }
}
