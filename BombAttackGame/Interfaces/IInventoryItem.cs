using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace BombAttackGame.Interfaces
{
    public interface IInventoryItem 
    {
        public Vector2 HudPosition { get; }
        public string HudDisplayName { get; }
        public Texture2D HudTexture { get; }
        public int InventorySlot { get; }

    }
}
