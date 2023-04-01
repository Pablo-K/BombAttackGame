using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Interfaces;
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
        Player _player;
        Player _teamMate;
        Player _enemy;

        Bullet _bullet;

        private List<Player> _players;
        private List<Bullet> _bullets;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _players = new List<Player>();
            _bullets = new List<Bullet>();

            _player = new Player(10, 10);
            _teamMate = new Player(10, 20);
            _enemy = new Player(100, 10);

            _player.Texture = Content.Load<Texture2D>("player");
            _enemy.Texture = Content.Load<Texture2D>("enemy");
            _teamMate.Texture = Content.Load<Texture2D>("teammate");

            _players.Add(_player);
            _players.Add(_teamMate);
            _players.Add(_enemy);
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if(_player?.ShotTime == null) _player.ShotTime = gameTime.TotalGameTime.TotalMilliseconds;

            var kstate = Keyboard.GetState();

            if (kstate.IsKeyDown(Keys.A)) { Move.PlayerMove(_player, Direction.Left); }
            if (kstate.IsKeyDown(Keys.S)) { Move.PlayerMove(_player, Direction.Down); }
            if (kstate.IsKeyDown(Keys.D)) { Move.PlayerMove(_player, Direction.Right); }
            if (kstate.IsKeyDown(Keys.W)) { Move.PlayerMove(_player, Direction.Up); }
            if (kstate.IsKeyDown(Keys.Space)) { TryShoot(_player, gameTime, Content); }

            Move.BulletsMove(_bullets);
            BulletsHit();
            CheckIsDead();

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var player in _players)
            {
                _spriteBatch.Draw(player.Texture, player.Location, Color.CornflowerBlue);
            }
            foreach (var bullet in _bullets)
            {
                _spriteBatch.Draw(bullet.Texture, bullet.Location, Color.CornflowerBlue);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void TryShoot(Player player, GameTime gameTime, Microsoft.Xna.Framework.Content.ContentManager content)
        {
            _bullet = Shoot.PlayerShoot(player, gameTime,content); if (_bullet.Location.X < 0) return; _bullets.Add(_bullet);
        }
        private void BulletsHit()
        {
            foreach (Bullet bullet in _bullets.ToList())
            {
                if (Hit.BulletHit(bullet, _players, out Player _player))
                {
                    _player.Hit(bullet.Damage);
                    _bullets.Remove(bullet);
                }
            }
        }
        private void CheckIsDead()
        {
            foreach(Player player in _players.ToList())
            {
                if(player.Health <= 0) _players.Remove(player);
            }
        }
    }
}