using BombAttackGame.Abstracts;
using BombAttackGame.Enums;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Models.HoldableObjects
{
    internal class Sheriff : Gun
    {
        public override Texture2D Texture => ContentContainer.SheriffTexture;

        public Sheriff() : base()
        {
            this.Latency = 200;
            this.Damage = 25;
            this.Magazine = 6;
            this.Ammo = 42;
            this.MagazineCapacity = 6;
            this.AmmoCapacity = 60;
        }

        public void Shoot(IGameObject gameObject, GameTime gameTime, Vector2 point)
        {
            Events.Shoot.PlayerShoot(gameObject as Player, gameTime, point);
        }

    }
}
