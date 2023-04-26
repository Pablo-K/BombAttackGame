using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Abstracts
{
    internal abstract class Gun : IHoldableObject, IEventRaiser
    {
        public int Damage { get; set; }
        public int Latency { get; set; }
        public double ShotTime { get; set; }
        public int Magazine { get; set; }
        public int MagazineCapacity { get; set; }
        public int Ammo { get; set; }
        public int AmmoCapacity { get; set; }
        public bool IsReloading { get; set; }
        public abstract Texture2D Texture { get; }
        public Queue<Enums.Events> Event { get;  set; }

        public Gun()
        {
            this.ShotTime = 0;
            this.Event = new Queue<Enums.Events>();
        }

        public virtual void Reload() {
            var diff = this.MagazineCapacity - this.Magazine;
            bool hasEnoughAmmo = this.Ammo >= diff;
            this.Magazine += hasEnoughAmmo ? diff : this.Ammo;
            this.Ammo -= hasEnoughAmmo ? diff : this.Ammo;
        }

        public void AddReloadEvent() 
        {
            this.Event.Enqueue(Enums.Events.Reload);
        }

        public void AddShootEvent()
        {
            this.Event.Enqueue(Enums.Events.Shoot); 
        }
    }
}
