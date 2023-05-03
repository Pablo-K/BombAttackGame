using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Draw
{
    internal class Animation
    {
        public double PartTime { get; set; }
        public double LastPartTime { get; set; }
        public int ActualPart { get; set; }  
        public Vector2 Location { get; set; }
        public int Parts { get; set; }
        public List<Texture2D> AnimationTexture { get; set; }
        public Animation(List<Texture2D> animationTexture, int parts, int partTime, Vector2 location)
        {
            AnimationTexture = animationTexture;
            Parts = parts;
            PartTime = partTime;
            LastPartTime = 0;
            Location = location;
        }
    }
}
