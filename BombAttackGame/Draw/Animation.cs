using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

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
        public Animation(List<Texture2D> animationTexture, Vector2 location)
        {
            AnimationTexture = animationTexture;
            Parts = animationTexture.Count;
            PartTime = 30;
            LastPartTime = 0;
            Location = new Vector2(location.X - (int)animationTexture.ElementAt(0).Width / 2,
                        location.Y - (int)animationTexture.ElementAt(0).Height / 2);
        }
    }
}
