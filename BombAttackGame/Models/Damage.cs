using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombAttackGame.Models
{
    internal class Damage
    {
        public int Amount { get; set; }
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public double ShowTime { get; set; }
        public double ShowingTime { get; set; }
        public Damage() { }
        public Damage(int amount, Vector2 location)
        {
            this.Amount = amount;
            this.Location = new Vector2(location.X, location.Y - 15);
            ShowingTime = 100;
        }
    }
}
