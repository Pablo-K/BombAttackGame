using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework.Graphics;

namespace BombAttackGame.Models.HoldableObjects
{
    internal class Sheriff : IGun
    {
        public Texture2D Texture => ContentContainer.SheriffTexture;
        public int Speed { get; set; }
        public int Damage { get; set; }
        public int Clip { get; set; }

        public Sheriff()
        {
            this.Speed = 200;
            this.Damage = 25;
            this.Clip = 15;
        }
    }
}
