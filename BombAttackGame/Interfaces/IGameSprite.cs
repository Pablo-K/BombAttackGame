using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombAttackGame.Interfaces
{
    public interface IGameSprite
    {
        public Vector2 Location { get; set; }
        public SpriteFont Font { get; set; }
        public Color Color { get; set; }
        public string Text { get; set; }
    }
}
