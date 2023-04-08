using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombAttackGame.Models
{
    internal class Bullet 
    {
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public int Damage { get; set; }
        public int Speed { get; set; }
        public Vector2 Direction { get; set; }
        public float DistanceTravelled { get; set; }
        public Player Owner { get; set; }
        public Vector2 Point { get; set; }
        public Bullet(Vector2 location, Player owner, Vector2 point)
        {
            this.Location = new Vector2(location.X, location.Y);
            this.Speed = 3;
            this.Damage = 20;
            this.Owner = owner;
            this.Point = point;
        }
    }
}
