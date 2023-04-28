using BombAttackGame.Enums;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BombAttackGame.Global
{
    public static class ContentContainer
    {

        private static ContentManager _contentManager;

        private static SpriteFont _hpFont;
        private static SpriteFont _damageFont;
        private static SpriteFont _magazineFont;
        private static SpriteFont _ammoFont;
        private static Texture2D _wallTexture;
        private static Texture2D _sheriffTexture;
        private static Texture2D _teamMateTexture;
        private static Texture2D _enemyTexture;
        private static Texture2D _bulletTexture;
        private static Texture2D _mainSpeedTexture;
        private static Texture2D _grenadeTexture;

        public static SpriteFont HpFont => _hpFont ??= _contentManager.Load<SpriteFont>("hp");
        public static SpriteFont DamageFont => _damageFont ??= _contentManager.Load<SpriteFont>("damage");
        public static SpriteFont MagazineFont => _magazineFont ??= _contentManager.Load<SpriteFont>("magazine");
        public static SpriteFont AmmoFont => _ammoFont ??= _contentManager.Load<SpriteFont>("ammo");
        public static Texture2D WallTexture => _wallTexture ??= _contentManager.Load<Texture2D>("wall");
        public static Texture2D SheriffTexture => _sheriffTexture ??= _contentManager.Load<Texture2D>("sheriff");
        public static Texture2D BulletTexture => _bulletTexture ??= _contentManager.Load<Texture2D>("bullet");
        public static Texture2D MainSpeedTexture => _mainSpeedTexture ??= _contentManager.Load<Texture2D>("mainSpeed");
        public static Texture2D GrenadeTexture => _grenadeTexture ??= _contentManager.Load<Texture2D>("grenade");

        public static void Initialize(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public static Texture2D PlayerTexture(Team team)
        {
            return team switch
            {
                Team.TeamMate => _teamMateTexture ??= _contentManager.Load<Texture2D>(team.ToString()),
                Team.Enemy => _enemyTexture ??= _contentManager.Load<Texture2D>(team.ToString()),
                _ => null
            };
        }

    }
}
