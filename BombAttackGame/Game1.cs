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
using System.Security.Cryptography.X509Certificates;

namespace BombAttackGame
{
    public class Game1 : Game
    {
        Player _player;
        Player _teamMate;
        Player _enemy;

        Bullet _bullet;

        private List<Player> _players;
        private List<Player> _enemies;
        private List<Player> _teamMates;
        private List<Player> _allPlayers;

        private List<Bullet> _bullets;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private int _width;
        private int _height;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _width = _graphics.PreferredBackBufferWidth;
            _height = _graphics.PreferredBackBufferHeight;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here

            _players = new List<Player>();
            _teamMates = new List<Player>();
            _allPlayers = new List<Player>();
            _enemies = new List<Player>();
            _bullets = new List<Bullet>();

            _player = new Player(new Vector2(50, 50));
            _player.Texture = Content.Load<Texture2D>("player");
            _players.Add(_player);
            _allPlayers.Add(_player);

            for (int i = 0; i < 4; i++)
            {
                _teamMate = new Player(new Vector2(10, 40 * i));
                _teamMate.Texture = Content.Load<Texture2D>("teammate");
                _teamMates.Add(_teamMate);
                _allPlayers.Add(_teamMate);
            }
            for (int i = 0; i < 5; i++)
            {
                _enemy = new Player(new Vector2(790, 40 * i));
                _enemy.Texture = Content.Load<Texture2D>("enemy");
                _enemies.Add(_enemy);
                _allPlayers.Add(_enemy);
            }
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (_player?.ShotTime == null) _player.ShotTime = gameTime.TotalGameTime.TotalMilliseconds;

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            if (kstate.IsKeyDown(Keys.A)) { Move.PlayerMove(_player, Direction.Left, _width, _height); }
            if (kstate.IsKeyDown(Keys.S)) { Move.PlayerMove(_player, Direction.Down, _width, _height); }
            if (kstate.IsKeyDown(Keys.D)) { Move.PlayerMove(_player, Direction.Right, _width, _height); }
            if (kstate.IsKeyDown(Keys.W)) { Move.PlayerMove(_player, Direction.Up, _width, _height); }

            if (mstate.LeftButton == ButtonState.Pressed) { TryShoot(_player, gameTime, Content, mstate.Position); }
            Move.BulletsMove(_bullets,_width,_height, out bool remove, out int index);
            if (remove) _bullets.RemoveAt(index);
            BulletsHit();
            CheckIsDead();


            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var player in _players) { _spriteBatch.Draw(player.Texture, player.Location, Color.CornflowerBlue); }
            foreach (var enemy in _enemies) { _spriteBatch.Draw(enemy.Texture, enemy.Location, Color.CornflowerBlue); }
            foreach (var teamMate in _teamMates) { _spriteBatch.Draw(teamMate.Texture, teamMate.Location, Color.CornflowerBlue); }
            foreach (var bullet in _bullets)
            {
                _spriteBatch.Draw(bullet.Texture, bullet.Location, Color.CornflowerBlue);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void TryShoot(Player player, GameTime gameTime, Microsoft.Xna.Framework.Content.ContentManager content, Point point)
        {
            _bullet = null;
            _bullet = Shoot.PlayerShoot(player, gameTime, content, point); if( _bullet == null ) { return; } _bullets.Add(_bullet);
            _bullet.Trajectory = SetTrajectory(_player.Location, point.ToVector2());
        }
        private void BulletsHit()
        {
            foreach (Bullet bullet in _bullets.ToList())
            {
                if (Hit.BulletHit(bullet, _allPlayers, out Player _player))
                {
                    _player.Hit(bullet.Damage);
                    _bullets.Remove(bullet);
                }
            }
        }
        private void CheckIsDead()
        {
            foreach (Player enemy in _enemies.ToList()) { if (enemy.Health <= 0) { _enemies.Remove(enemy); _allPlayers.Remove(enemy); } }
            foreach (Player player in _players.ToList()) { if (player.Health <= 0) { _players.Remove(player); _allPlayers.Remove(player); } }
            foreach(Player teamMate in _teamMates.ToList()) { if(teamMate.Health <= 0) { _teamMates.Remove(teamMate); _allPlayers.Remove(teamMate); } }
        }
        private List<Vector2> SetTrajectory(Vector2 PlayerLoc, Vector2 ShootLoc)
        {
            List<Vector2> Trajectory = new List<Vector2>();

            int w = Convert.ToInt32(ShootLoc.X - PlayerLoc.X);
            int h = Convert.ToInt32(ShootLoc.Y - PlayerLoc.Y);
            int x = Convert.ToInt32(PlayerLoc.X);
            int y = Convert.ToInt32(PlayerLoc.Y);
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                Vector2 Traj = new Vector2(x, y);
                Trajectory.Add(Traj);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    x += dx1;
                    y += dy1;
                }
                else
                {
                    x += dx2;
                    y += dy2;
                }
            }


            return Trajectory;
        }
    }
}