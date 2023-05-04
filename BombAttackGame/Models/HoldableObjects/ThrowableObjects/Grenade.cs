using BombAttackGame.Abstracts;
using BombAttackGame.Global;
using BombAttackGame.HUD;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombAttackGame.Models.HoldableObjects.ThrowableObjects
{
    internal class Grenade : Explosive, IInventoryItem
    {
        public override Texture2D HudTexture => ContentContainer.GrenadeTexture;
        public string Type { get; set; }
        public Vector2 HudPosition => HudVector.HoldableVector();
        public string HudDisplayName { get; }
        public int InventorySlot { get; }

        public Grenade(string type) : base()
        {
            this.Type = type;
            this.InventorySlot = 2;
            this.HudDisplayName = type;
        }

        public void Throw()
        {

        }

    }
}
