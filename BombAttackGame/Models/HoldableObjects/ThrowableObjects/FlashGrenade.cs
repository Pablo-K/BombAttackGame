using BombAttackGame.Abstracts;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Models.HoldableObjects.ThrowableObjects
{
    internal class FlashGrenade : Explosive, IGameObject, IProjectile
    {
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public float Distance { get; set; }
        public int Speed { get; set; }
        public float DistanceTravelled { get; set; }
        public float MaxDistance { get; set; }
        public int FlashTime { get; set; }
        public int TimeFlashed { get; set; }
        public double StartTime { get; set; }
        public double MaxTime { get; set; }
        public Player Owner { get; set; }
        public Vector2 StartLocation { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Point { get; set; }
        public Rectangle Rectangle { get; set; }
        public Queue<Enums.Events> Event { get; set; }
        public bool IsDead { get; set; }
        public override Texture2D HudTexture => ContentContainer.GrenadeTexture;

        public FlashGrenade(Vector2 location, Player owner, Vector2 point)
        {
            this.Location = new Vector2(location.X, location.Y);
            this.StartLocation = new Vector2(location.X, location.Y);
            this.Owner = owner;
            this.FlashTime = 2500;
            this.Point = point;
            this.Speed = 5;
            this.MaxDistance = 5000;
            this.IsDead = false;
            this.Color = Color.AliceBlue;
            this.Event = new Queue<Enums.Events>();
            this.MaxTime = 1300;
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
