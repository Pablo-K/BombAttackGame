using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Global
{
    public static class AnimationsContainer
    {
        private static ContentManager _contentManager;

        private static List<Texture2D> _handGrenadeBoom;
        private static List<Texture2D> _bombBoom;

        public static List<Texture2D> HandGrenadeBoom => _handGrenadeBoom ??= new List<Texture2D>
        {
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom1"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom2"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom3"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom4"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom5"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom6")
        };
        public static List<Texture2D> BombBoom => _bombBoom ??= new List<Texture2D>
        {
            _contentManager.Load<Texture2D>("BombBoomAnimation/bombboom1"),
            _contentManager.Load<Texture2D>("BombBoomAnimation/bombboom2"),
            _contentManager.Load<Texture2D>("BombBoomAnimation/bombboom3"),
            _contentManager.Load<Texture2D>("BombBoomAnimation/bombboom4"),
            _contentManager.Load<Texture2D>("BombBoomAnimation/bombboom5"),
            _contentManager.Load<Texture2D>("BombBoomAnimation/bombboom6")
        };
        public static void Initialize(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }
    }
}
