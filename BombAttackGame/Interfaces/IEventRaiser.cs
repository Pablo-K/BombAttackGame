using BombAttackGame.Enums;
using System.Collections.Generic;

namespace BombAttackGame.Interfaces
{
    public interface IEventRaiser
    {
        public Queue<Enums.Events> Event { get; }
    }
}

