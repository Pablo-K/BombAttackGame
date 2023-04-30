using BombAttackGame.Abstracts;
using BombAttackGame.Bonuses;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Models.HoldableObjects.ThrowableObjects
{
    internal class HandGrenade : Explosive, IGameObject, IProjectile
    {
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public Color Color { get; set; }
        public float Distance { get; set; }
        public int Speed { get; set; }
        public float DistanceTravelled { get; set; }
        public float MaxDistance { get; set; }
        public int Damage { get; set; }
        public double TeamDamage { get; set; }
        public double EnemyDamage { get; set; }
        public double OtherDamage { get; set; }
        public Player Owner { get; set; }
        public Vector2 StartLocation { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Point { get; set; }
        public Rectangle Rectangle { get; set; }
        public Queue<Enums.Events> Event { get; set; }
        public bool IsDead { get; set; }
        public int DamageDealt { get; set; }
        public override Texture2D HudTexture => ContentContainer.GrenadeTexture;

        public HandGrenade(Vector2 location, Player owner, Vector2 point)
        {
            Location = new Vector2(location.X, location.Y);
            StartLocation = new Vector2(location.X, location.Y);
            Owner = owner;
            Damage = 83;
            Point = point;
            Speed = 5;
            MaxDistance = 5000;
            TeamDamage = 0.5;
            EnemyDamage = 1;
            OtherDamage = 1;
            IsDead = false;
            Color = Color.AliceBlue;
            Event = new Queue<Enums.Events>();
        }

        public override void Explode()
        {
            this.Event.Enqueue(Enums.Events.Explode);
            this.Event.Enqueue(Enums.Events.Delete);
        }
        public int CalculateDamage(IGameObject gameObject)
        {
            float distance = 0;
            var newDamage = 0;
            distance = Vector2.Distance(Location, gameObject.Location);
            switch (distance)
            {
                case > 100:
                    newDamage = 0;
                    break;
                case > 90:
                    newDamage = (int)(Damage * 0.1); break;
                case > 80:
                    newDamage = (int)(Damage * 0.2); break;
                case > 70:
                    newDamage = (int)(Damage * 0.3); break;
                case > 60:
                    newDamage = (int)(Damage * 0.4); break;
                case > 50:
                    newDamage = (int)(Damage * 0.5); break;
                case > 40:
                    newDamage = (int)(Damage * 0.6); break;
                case > 30:
                    newDamage = (int)(Damage * 0.7); break;
                case > 20:
                    newDamage = (int)(Damage * 0.8); break;
                case > 10:
                    newDamage = (int)(Damage * 0.9); break;
            }
            if (gameObject is Player)
            {
                Player Player = gameObject as Player;
                if (Player.Team == Owner.Team) newDamage = (int)(newDamage * TeamDamage);
                if (Player.Team != Owner.Team) newDamage = (int)(newDamage * EnemyDamage);
                if (Player == this.Owner) newDamage = 0;
            }
            this.DamageDealt = newDamage;
            return DamageDealt;
        }
        public void UpdateRectangle()
        {
            Rectangle = new Rectangle((int)Location.X, (int)Location.Y, Texture.Width, Texture.Height);
        }

        public void Tick(GameTime GameTime, List<IGameObject> GameObjects, List<Rectangle> MapRectangle)
        {
            Move();
            UpdateRectangle();
            DistanceEvent();
        }
        private void Move()
        {
            Event.Enqueue(Enums.Events.Move);
        }
        private void DistanceEvent()
        {
            if (DistanceTravelled >= MaxDistance)
                Explode();
        }
    }
}
