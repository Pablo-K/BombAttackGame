using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Interfaces
{
    public interface IGameSprite
    {
        public Vector2 Location { get; set; }
        public Color Color { get; set; }
        public SpriteFont Font { get; set; }
        public string Text { get; set; }
        public void Tick(GameTime GameTime, List<IGameSprite> GameObjects) { }
    }
}
