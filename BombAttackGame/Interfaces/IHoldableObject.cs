using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Interfaces
{
    public interface IHoldableObject
    {
        public Texture2D Texture { get; }
        public Queue<Enums.Events> Event { get;  set; }
    }
}
