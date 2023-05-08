using BombAttackGame.Enums;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Global
{
    public static class ContentContainer
    {

        private static ContentManager _contentManager;

        private static SpriteFont _hpFont;
        private static SpriteFont _gameResultFont;
        private static SpriteFont _damageFont;
        private static SpriteFont _magazineFont;
        private static SpriteFont _ammoFont;
        private static Texture2D _wallTexture;
        private static Texture2D _groundTexture;
        private static Texture2D _waterTexture;
        private static Texture2D _sheriffTexture;
        private static Texture2D _teamMateTexture;
        private static Texture2D _enemyTexture;
        private static Texture2D _bulletTexture;
        private static Texture2D _mainSpeedTexture;
        private static Texture2D _grenadeTexture;
        private static Texture2D _handGrenadeTexture;
        private static Texture2D _flashGrenadeTexture;
        const string PA = "PlayerAnimation/";

        public static SpriteFont HpFont => _hpFont ??= _contentManager.Load<SpriteFont>("hp");
        public static SpriteFont GameResultFont => _gameResultFont ??= _contentManager.Load<SpriteFont>("gameresult");
        public static SpriteFont DamageFont => _damageFont ??= _contentManager.Load<SpriteFont>("damage");
        public static SpriteFont MagazineFont => _magazineFont ??= _contentManager.Load<SpriteFont>("magazine");
        public static SpriteFont AmmoFont => _ammoFont ??= _contentManager.Load<SpriteFont>("ammo");
        public static Texture2D WallTexture => _wallTexture ??= _contentManager.Load<Texture2D>("wall");
        public static Texture2D WaterTexture => _waterTexture ??= _contentManager.Load<Texture2D>("water");
        public static Texture2D GroundTexture => _groundTexture ??= _contentManager.Load<Texture2D>("ground");
        public static Texture2D SheriffTexture => _sheriffTexture ??= _contentManager.Load<Texture2D>("sheriff");
        public static Texture2D BulletTexture => _bulletTexture ??= _contentManager.Load<Texture2D>("bullet");
        public static Texture2D MainSpeedTexture => _mainSpeedTexture ??= _contentManager.Load<Texture2D>("mainSpeed");
        public static Texture2D GrenadeTexture => _grenadeTexture ??= _contentManager.Load<Texture2D>("grenade");
        public static Texture2D HandGrenadeTexture => _handGrenadeTexture ??= _contentManager.Load<Texture2D>("handgrenade");
        public static Texture2D FlashGrenadeTexture => _flashGrenadeTexture ??= _contentManager.Load<Texture2D>("flashgrenade");
        public static List<Texture2D> TeamMateUp { get { return new List<Texture2D>() { 
            _contentManager.Load<Texture2D>(PA +"teammateup1"), 
            _contentManager.Load<Texture2D>(PA +"teammateup2") }; } }
        public static List<Texture2D> TeamMateDown { get { return new List<Texture2D>() { 
            _contentManager.Load<Texture2D>(PA + "teammatedown1"),
            _contentManager.Load<Texture2D>(PA +"teammatedown2") }; } }
        public static List<Texture2D> TeamMateLeft { get { return new List<Texture2D>() { 
            _contentManager.Load<Texture2D>(PA + "teammateleft1"), 
            _contentManager.Load<Texture2D>(PA + "teammateleft2") }; } }
        public static List<Texture2D> TeamMateRight { get { return new List<Texture2D>() { 
            _contentManager.Load<Texture2D>(PA + "teammateright1"), 
            _contentManager.Load<Texture2D>(PA + "teammateright2") }; } }
        public static List<Texture2D> EnemyUp { get { return new List<Texture2D>() { 
            _contentManager.Load<Texture2D>(PA + "enemyup1"), 
            _contentManager.Load<Texture2D>(PA + "enemyup2") }; } }
        public static List<Texture2D> EnemyDown { get { return new List<Texture2D>() { 
            _contentManager.Load<Texture2D>(PA + "enemydown1"), 
            _contentManager.Load<Texture2D>(PA + "enemydown2") }; } }
        public static List<Texture2D> EnemyLeft { get { return new List<Texture2D>() { 
            _contentManager.Load<Texture2D>(PA + "enemyleft1"), 
            _contentManager.Load<Texture2D>(PA + "enemyleft2") }; } }
        public static List<Texture2D> EnemyRight { get { return new List<Texture2D>() { 
            _contentManager.Load<Texture2D>(PA + "enemyright1"), 
            _contentManager.Load<Texture2D>(PA + "enemyright2") }; } }

        public static void Initialize(ContentManager contentManager)
        {
            _contentManager = contentManager;
        }

        public static Texture2D PlayerTexture(Team team)
        {
            return team switch
            {
                Team.TeamMate => _teamMateTexture ??= _contentManager.Load<Texture2D>("PlayerAnimation/" + team.ToString() + "down1"),
                Team.Enemy => _enemyTexture ??= _contentManager.Load<Texture2D>("PlayerAnimation/" + team.ToString() + "down1"),
                _ => null
            };
        }
        public static Texture2D PlayerTextureMove(Team team, Direction direction, int value)
        {
            switch (direction)
            {
                case Direction.Left:
                    if (team == Team.TeamMate) return TeamMateLeft.ElementAt(value);
                    return EnemyLeft.ElementAt(value);
                case Direction.Right:
                    if (team == Team.TeamMate) return TeamMateRight.ElementAt(value);
                    return EnemyRight.ElementAt(value);
                case Direction.Down:
                    if (team == Team.TeamMate) return TeamMateDown.ElementAt(value);
                    return EnemyDown.ElementAt(value);
                case Direction.DownRight:
                    if (team == Team.TeamMate) return TeamMateDown.ElementAt(value);
                    return EnemyDown.ElementAt(value);
                case Direction.DownLeft:
                    if (team == Team.TeamMate) return TeamMateDown.ElementAt(value);
                    return EnemyDown.ElementAt(value);
                case Direction.Up:
                    if (team == Team.TeamMate) return TeamMateUp.ElementAt(value);
                    return EnemyUp.ElementAt(value);
                case Direction.UpRight:
                    if (team == Team.TeamMate) return TeamMateUp.ElementAt(value);
                    return EnemyUp.ElementAt(value);
                case Direction.UpLeft:
                    if (team == Team.TeamMate) return TeamMateUp.ElementAt(value);
                    return EnemyUp.ElementAt(value);
            }
            return null;
        }

    }
}
