using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Abstracts
{
    internal abstract class Explosive : IHoldableObject, IEventRaiser
    {
        public int Damage { get; set; }
        public abstract Texture2D HudTexture { get; }
        public Queue<Enums.Events> Event { get;  set; }
        public Explosive()
        {
            this.Event = new Queue<Enums.Events>();
        }
    }
}
