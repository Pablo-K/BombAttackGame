using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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
        public Vector2 StartLocation { get; set; }
        public float Distance { get; set; }
        public Vector2 Point { get; set; }
        public Bullet(Vector2 location, Player owner, Vector2 point)
        {
            this.Location = new Vector2(location.X, location.Y);
            this.StartLocation = new Vector2(location.X, location.Y);
            this.Speed = 3;
            this.Damage = 20;
            this.Owner = owner;
            this.Point = point;
        }
        public float CalculateDamage()
        {
            switch (DistanceTravelled)
            {
                case > 40000:
                    return Damage * 0.1f;
                case > 35000:
                    return Damage * 0.2f;
                case > 30000:
                    return Damage * 0.3f;
                case > 25000:
                    return Damage * 0.4f;
                case > 20000:
                    return Damage * 0.5f;
                case > 15000:
                    return Damage * 0.6f;
                case > 10000:
                    return Damage * 0.7f;
                case > 8000:
                    return Damage * 0.8f;
                case > 5000:
                    return Damage * 0.9f;
            }
            return Damage;
        }
    }
}
