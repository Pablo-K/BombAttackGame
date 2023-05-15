using BombAttackGame.Global;
using BombAttackGame.HUD;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace BombAttackGame.Models.HoldableObjects
{
    internal class Bomb : IInventoryItem, IOnGroundItem
    {
        public Vector2 HudPosition => HudVector.HoldableVector();
        public string HudDisplayName { get; }
        public Texture2D HudTexture => ContentContainer.BombHudTexture;
        public Texture2D PlantedTexture => ContentContainer.BombPlantedTexture;
        public Texture2D GroundTexture => ContentContainer.BombGroundTexture;
        public bool Planted { get; private set; }
        public Vector2 Location { get; set; }
        public int Damage { get; set; }
        public double Time { get; }
        public double BoomTime { get; private set; }
        public int InventorySlot { get; }
        public int DamageDealt { get; set; }
        public Queue<Enums.Events> Event { get; set ; }

        public Bomb() : base()
        {
            this.Time = 3000;
            this.Damage = 500;
            this.HudDisplayName = "Bomb";
            this.InventorySlot = 4;
        }
        public void Plant(Vector2 loc, double time)
        {
            this.Planted = true;
            this.Location = loc;
            this.BoomTime = time + this.Time;
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
            this.DamageDealt = newDamage;
            return DamageDealt;
        }

    }
}
