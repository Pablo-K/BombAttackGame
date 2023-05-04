using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Models
{
    internal class Bullet : IGameObject, IProjectile
    {
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public bool IsDead { get; set; }
        public int Speed { get; set; }
        public float Distance { get; set; }
        public float DistanceTravelled { get; set; }
        public float MaxDistance { get; set; }
        public int FlashTime { get; set; }
        public double TeamDamage { get; set; }
        public double EnemyDamage { get; set; }
        public double OtherDamage { get; set; }
        public Player Owner { get; set; }
        public Vector2 StartLocation { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Point { get; set; }
        public Rectangle Rectangle { get; set; }
        public Queue<Enums.Events> Event { get; set; }
        public IGameObject ObjectHitted { get; set; }
        public int DamageDealt { get; set; }

        public Bullet(Vector2 location, Player owner, Vector2 point, int damage)
        {
            this.Location = new Vector2(location.X, location.Y);
            this.StartLocation = new Vector2(location.X, location.Y);
            this.Speed = 5;
            this.Owner = owner;
            this.FlashTime = damage;
            this.Point = point;
            this.MaxDistance = 120000;
            this.TeamDamage = 0.5;
            this.EnemyDamage = 1;
            this.OtherDamage = 1;
            this.IsDead = false;
            this.Color = Color.AliceBlue;
            this.Event = new Queue<Enums.Events>();
        }
        public int CalculateDamage(IGameObject GameObject)
        {
            switch (this.DistanceTravelled)
            {
                case > 80000:
                    FlashTime = (int)(FlashTime * 0.1); break;
                case > 55000:
                    FlashTime = (int)(FlashTime * 0.2); break;
                case > 30000:
                    FlashTime = (int)(FlashTime * 0.3); break;
                case > 20000:
                    FlashTime = (int)(FlashTime * 0.4); break;
                case > 15000:
                    FlashTime = (int)(FlashTime * 0.5); break;
                case > 12000:
                    FlashTime = (int)(FlashTime * 0.6); break;
                case > 9000:
                    FlashTime = (int)(FlashTime * 0.7); break;
                case > 7000:
                    FlashTime = (int)(FlashTime * 0.8); break;
                case > 4000:
                    FlashTime = (int)(FlashTime * 0.9); break;
            }
            if(GameObject is Player)
            {
                Player Player = GameObject as Player;
                if (Player.Team == this.Owner.Team) FlashTime = (int)(FlashTime * this.TeamDamage);
                if (Player.Team != this.Owner.Team) FlashTime = (int)(FlashTime * this.EnemyDamage);
            }
            return (int)FlashTime;
        }
        public void UpdateRectangle()
        {
            this.Rectangle = new Rectangle((int)Location.X, (int)Location.Y, Texture.Width, Texture.Height);
        }

        public void Tick(GameTime GameTime, List<IGameObject> GameObjects, List<Rectangle> MapRectangle)
        {
            Move();
            UpdateRectangle();
            BulletHitted(GameObjects);
            DistanceDelete();
        }
        private void Move()
        {
            Event.Enqueue(Enums.Events.Move);
        }
        private void BulletHitted(List<IGameObject> GameObjects)
        {
            foreach (IGameObject obj in GameObjects.OfType<Player>().Where(x => x.IsDead == false))
            {
                if (Hit.InHitBox(this, obj as Player))
                {
                    this.Event.Enqueue(Enums.Events.ObjectHitted);
                    this.ObjectHitted = obj;
                    this.DamageDealt = CalculateDamage(obj);
                }
            }
        }
        private void DistanceDelete()
        {
            if (this.DistanceTravelled >= this.MaxDistance) this.Event.Enqueue(Enums.Events.Delete);
        }
    }
}
