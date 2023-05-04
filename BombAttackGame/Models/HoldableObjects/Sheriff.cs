using BombAttackGame.Abstracts;
using BombAttackGame.Global;
using BombAttackGame.HUD;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombAttackGame.Models.HoldableObjects
{
    internal class Sheriff : Gun, IInventoryItem
    {

        public Vector2 HudPosition => HudVector.HoldableVector();
        public string HudDisplayName { get; }
        public override Texture2D HudTexture => ContentContainer.SheriffTexture;
        public int InventorySlot { get; }
        public Vector2 Location { get; set; }
        public override int Damage { get; set; }

        public Sheriff() : base()
        {
            this.Latency = 200;
            this.Damage = 25;
            this.Magazine = 6;
            this.Ammo = 42;
            this.MagazineCapacity = 6;
            this.AmmoCapacity = 60;
            this.HudDisplayName = "Sheriff";
            this.InventorySlot = 1;
        }

        public void Shoot(IGameObject gameObject, GameTime gameTime, Vector2 point)
        {
            Events.Shoot.PlayerShoot(gameObject as Player, gameTime, point);
        }

    }
}
