using BombAttackGame.Abstracts;
using BombAttackGame.Global;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Concurrent;

namespace BombAttackGame.Models.HoldableObjects.ThrowableObjects
{
    internal class Grenade : Explosive
    {
        public override Texture2D HudTexture => ContentContainer.GrenadeTexture;

        public Grenade() : base()
        {
            this.Damage = 100;
        }

        public void Throw()
        {

        }

    }
}
