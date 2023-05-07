using BombAttackGame.Abstracts;
using BombAttackGame.Bonuses;
using BombAttackGame.Global;
using BombAttackGame.HUD;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.ComponentModel.DataAnnotations;

namespace BombAttackGame.Models.HoldableObjects.ThrowableObjects
{
    public abstract class Grenade : Explosive, IInventoryItem, IProjectile
    {
        public override Texture2D HudTexture => ContentContainer.GrenadeTexture;
        public Vector2 HudPosition => HudVector.HoldableVector();
        public virtual string HudDisplayName { get; }
        public int InventorySlot { get; }
        public int Speed { get; set; }
        public Player Owner { get; set; }
        public float Distance { get; set; }
        public float DistanceTravelled { get; set ; }
        public Vector2 StartLocation { get; set; }
        public Vector2 Direction { get; set; }
        public Vector2 Point { get; set; }
        public abstract double MaxTime { get; }

        public Grenade(Player owner) : base()
        {
            this.InventorySlot = 2;
            this.Speed = GameManager.GrenadeSpeed;
            this.Owner = owner;
        }

        public void Throw()
        {

        }

    }
}
