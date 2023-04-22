using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Models
{
    internal class Damage : IGameSprite
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public Vector2 Location { get; set; }
        public Color Color { get; set; }
        public double ShowTime { get; set; }
        public double ShowingTime { get; set; }
        public Damage(int Text, Vector2 Location)
        {
            this.Text = Text.ToString();
            this.Location = new Vector2(Location.X, Location.Y - 15);
            this.ShowingTime = 100;
            this.Color = Color.White;
        }
        public void Tick(GameTime GameTime, List<IGameSprite> GameSprites)
        {
            if (GameTime.TotalGameTime.TotalMilliseconds - this.ShowTime >= this.ShowingTime)
            { GameSprites.Remove(this); }
        }
    }
}
