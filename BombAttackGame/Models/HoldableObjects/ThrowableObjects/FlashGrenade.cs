using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Models.HoldableObjects.ThrowableObjects
{
    internal class FlashGrenade : Grenade, IGameObject
    {
        public override string HudDisplayName => "Flash Grenade";
        public int FlashTime => 2500; 
        public int TimeFlashed { get; set; }
        public double StartTime { get; set; }
        public override double MaxTime => 1300; 
        public override Texture2D HudTexture => ContentContainer.GrenadeTexture;

        public FlashGrenade(Vector2 location, Player owner, Vector2 point) : base (owner)
        {
            this.Location = new Vector2(location.X, location.Y);
            this.StartLocation = new Vector2(location.X, location.Y);
            this.Point = point;
            this.IsDead = false;
            this.Color = Color.AliceBlue;
            this.Event = new Queue<Enums.Events>();
        }
        public FlashGrenade(Player owner) : base (owner) { 
        
            this.Texture = ContentContainer.FlashGrenadeTexture;
            this.Color = Color.Blue;
        }

        public override void Explode()
        {
            this.Event.Enqueue(Enums.Events.Explode);
            this.Event.Enqueue(Enums.Events.Delete);
        }
        public int CalculateTime(IGameObject gameObject)
        {
            float distance = 0;
            var newTime = 0;
            distance = Vector2.Distance(Location, gameObject.Location);
            switch (distance)
            {
                case > 400:
                    newTime = 0;
                    break;
                case > 300:
                    newTime = (int)(FlashTime * 0.1); break;
                case > 250:
                    newTime = (int)(FlashTime * 0.2); break;
                case > 200:
                    newTime = (int)(FlashTime * 0.3); break;
                case > 180:
                    newTime = (int)(FlashTime * 0.4); break;
                case > 160:
                    newTime = (int)(FlashTime * 0.5); break;
                case > 140:
                    newTime = (int)(FlashTime * 0.6); break;
                case > 120:
                    newTime = (int)(FlashTime * 0.7); break;
                case > 100:
                    newTime = (int)(FlashTime * 0.8); break;
                case > 10:
                    newTime = (int)(FlashTime * 0.9); break;
            }
            this.TimeFlashed = newTime;
            return this.TimeFlashed;
        }
        public void UpdateRectangle()
        {
            Rectangle = new Rectangle((int)Location.X, (int)Location.Y, Texture.Width, Texture.Height);
        }

        public void Tick(GameTime GameTime, List<IGameObject> GameObjects, List<Rectangle> MapRectangle)
        {
            Move();
            UpdateRectangle();
            TimeEvent(GameTime.TotalGameTime.TotalMilliseconds);
        }
        private void Move()
        {
            Event.Enqueue(Enums.Events.Move);
        }
        private void TimeEvent(double time)
        {
            if (time >= this.StartTime + this.MaxTime)
                Explode();
        }
    }
}
