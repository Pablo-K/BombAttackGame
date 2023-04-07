using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.HUD;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame
{
    public class Game1 : Game
    {
        private SpriteFont _hpF;
        private SpriteFont _damageF;

        private Player _player;
        private Player _teamMate;
        private Player _enemy;

        private Bullet _bullet;

        private List<Player> _players;
        private List<Player> _enemies;
        private List<Player> _teamMates;
        private List<Player> _allPlayers;

        private List<Damage> _damages;

        private List<Bullet> _bullets;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _mousePosition;

        private int[] _mapSize = new int[2];

        private int _teamMateCount;
        private int _enemyCount;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _mapSize[0] = 800;
            _mapSize[1] = 800;

            _graphics.PreferredBackBufferWidth = _mapSize[0];
            _graphics.PreferredBackBufferHeight = _mapSize[1];
            _graphics.ApplyChanges();

            _enemyCount = 5;
            _teamMateCount = 4;

            base.Initialize();
        }

        protected override void LoadContent()
        { 
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _hpF = Content.Load<SpriteFont>("hp");
            _damageF = Content.Load<SpriteFont>("damage");

            _players = new List<Player>();
            _teamMates = new List<Player>();
            _allPlayers = new List<Player>();
            _enemies = new List<Player>();
            _bullets = new List<Bullet>();
            _damages = new List<Damage>();

            _player = new Player();
            _player.Texture = Content.Load<Texture2D>("player");
            _player.Location = Spawn.GenerateRandomSpawnPoint(_mapSize, _player.Texture);
            _players.Add(_player);
            _allPlayers.Add(_player);

            for (int i = 0; i < _teamMateCount; i++)
            {
                _teamMate = new Player();
                _teamMate.Texture = Content.Load<Texture2D>("teammate");
                _teamMate.Location = Spawn.GenerateRandomSpawnPoint(_mapSize, _teamMate.Texture);
                _teamMates.Add(_teamMate);
                _allPlayers.Add(_teamMate);
            }
            for (int i = 0; i < _enemyCount; i++)
            {
                _enemy = new Player();
                _enemy.Texture = Content.Load<Texture2D>("enemy");
                _enemy.Location = Spawn.GenerateRandomSpawnPoint(_mapSize, _enemy.Texture);
                _enemies.Add(_enemy);
                _allPlayers.Add(_enemy);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            if (_player?.ShotTime == null) _player.ShotTime = gameTime.TotalGameTime.TotalMilliseconds;

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            _mousePosition = mstate.Position.ToVector2();
            
            if (kstate.IsKeyDown(Keys.A)) { Move.PlayerMove(_player, Direction.Left, _mapSize); }
            if (kstate.IsKeyDown(Keys.S)) { Move.PlayerMove(_player, Direction.Down, _mapSize); }
            if (kstate.IsKeyDown(Keys.D)) { Move.PlayerMove(_player, Direction.Right, _mapSize); }
            if (kstate.IsKeyDown(Keys.W)) { Move.PlayerMove(_player, Direction.Up, _mapSize); }

            if (mstate.LeftButton == ButtonState.Pressed) { TryShoot(_player, gameTime, Content, _mousePosition.ToPoint()); }


            Move.BulletsMove(_bullets, out bool remove, out int index);
            if (remove) _bullets.RemoveAt(index);
            BulletsHit(gameTime);
            CheckIsDead();
            CheckIfTimeIsGone(gameTime);

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var player in _players) { _spriteBatch.Draw(player.Texture, player.Location, Color.CornflowerBlue); }
            foreach (var enemy in _enemies) { _spriteBatch.Draw(enemy.Texture, enemy.Location, Color.CornflowerBlue); }
            foreach (var teamMate in _teamMates) { _spriteBatch.Draw(teamMate.Texture, teamMate.Location, Color.CornflowerBlue); }
            foreach (var bullet in _bullets) { _spriteBatch.Draw(bullet.Texture, bullet.Location, Color.CornflowerBlue); }
            foreach (var damage in _damages) {  _spriteBatch.DrawString(_damageF, damage.Amount.ToString(), damage.Location, Color.Red); }
            _spriteBatch.DrawString(_hpF, _player.Health.ToString(), HudVector.HpVector(_mapSize), Color.Red);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void TryShoot(Player player, GameTime gameTime, Microsoft.Xna.Framework.Content.ContentManager content, Point point)
        {
            _bullet = null;
            _bullet = Shoot.PlayerShoot(player, gameTime, content, point); if( _bullet == null ) { return; } _bullets.Add(_bullet);
            _bullet.Trajectory = Shoot.SetTrajectory(_player.Location, point.ToVector2());
        }
        private void BulletsHit(GameTime gameTime)
        {
            foreach (Bullet bullet in _bullets.ToList())
            {
                if (Hit.BulletHit(bullet, _allPlayers, out Player _player))
                {
                    _player.Hit(bullet.Damage);
                    _bullets.Remove(bullet);
                    
                    Damage damage = new Damage(bullet.Damage, _player.Location);
                    damage.ShowTime = gameTime.TotalGameTime.TotalMilliseconds;
                    _damages.Add(damage);
                }
            }
        }
        private void CheckIsDead()
        {
            foreach (Player enemy in _enemies.ToList()) { if (enemy.Health <= 0) { _enemies.Remove(enemy); _allPlayers.Remove(enemy); } }
            foreach (Player player in _players.ToList()) { if (player.Health <= 0) { _players.Remove(player); _allPlayers.Remove(player); } }
            foreach (Player teamMate in _teamMates.ToList()) { if(teamMate.Health <= 0) { _teamMates.Remove(teamMate); _allPlayers.Remove(teamMate); } }
        }
        private void CheckIfTimeIsGone(GameTime gameTime)
        {
            foreach (Damage damage in _damages.ToList()) { if (gameTime.TotalGameTime.TotalMilliseconds - damage.ShowTime >= damage.ShowingTime) { _damages.Remove(damage);} }
        }
    }
}