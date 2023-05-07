using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Abstracts
{
    public abstract class Explosive : IGameObject, IHoldableObject, IEventRaiser
    {
        public int Damage { get; set; }
        public abstract Texture2D HudTexture { get; }
        public Queue<Enums.Events> Event { get;  set; }
        public Vector2 Location { get; set; }
        public Color Color { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsDead { get; set; }
        public Rectangle Rectangle { get; set; }
        public bool IsHuman { get; set; }

        public Explosive()
        {
            this.Event = new Queue<Enums.Events>();
        }
        public virtual void Explode() { }
    }
}
