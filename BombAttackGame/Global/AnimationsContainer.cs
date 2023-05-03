using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;

namespace BombAttackGame.Global
{
    public static class AnimationsContainer
    {
        private static ContentManager _contentManager;

        private static List<Texture2D> _handGrenadeBoom;

        public static List<Texture2D> HandGrenadeBoom => _handGrenadeBoom ??= new List<Texture2D>
        {
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom1"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom2"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom3"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom4"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom5"),
            _contentManager.Load<Texture2D>("HandGrenadeBoomAnimation/handgrenadeboom6")
        };
        public static void Initialize(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }
    }
}
