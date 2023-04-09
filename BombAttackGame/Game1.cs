using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.HUD;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;

namespace BombAttackGame
{
    public class Game1 : Game
    {
        private SpriteFont _hpF;
        private SpriteFont _damageF;

        private Texture2D _wall;

        private Player _player;

        private List<Player> _allPlayers;

        private List<Damage> _damages;

        private List<Bullet> _bullets;

        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        private Vector2 _mousePosition;

        private int[] _mapSize = new int[2];

        private int _teamMateAmount;
        private int _enemyAmount;
        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _mapSize[0] = 1000;
            _mapSize[1] = 1000;

            Window.AllowUserResizing = true;

            _graphics.PreferredBackBufferWidth = _mapSize[0];
            _graphics.PreferredBackBufferHeight = _mapSize[1];
            _graphics.ApplyChanges();

            _enemyAmount = 5;
            _teamMateAmount = 4;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _hpF = Content.Load<SpriteFont>("hp");
            _damageF = Content.Load<SpriteFont>("damage");

            _wall = Content.Load<Texture2D>("wall");

            _allPlayers = new List<Player>();

            _bullets = new List<Bullet>();
            _damages = new List<Damage>();

            _player = new Player();
            _player.Texture = Content.Load<Texture2D>("player");
            _player.Team = Team.Player;
            _player.Location = Spawn.GenerateRandomSpawnPoint(_mapSize, _player.Texture);

            _allPlayers.Add(_player);

            _allPlayers.AddRange(Player.AddPlayers(Team.TeamMate, Content, _teamMateAmount, _mapSize));
            _allPlayers.AddRange(Player.AddPlayers(Team.Enemy, Content, _enemyAmount, _mapSize));
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            _mapSize[0] = Window.ClientBounds.Width;
            _mapSize[1] = Window.ClientBounds.Height;

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            _mousePosition = mstate.Position.ToVector2();

            if (kstate.IsKeyDown(Keys.A)) { Move.PlayerMove(_player, Direction.Left, _mapSize); }
            if (kstate.IsKeyDown(Keys.S)) { Move.PlayerMove(_player, Direction.Down, _mapSize); }
            if (kstate.IsKeyDown(Keys.D)) { Move.PlayerMove(_player, Direction.Right, _mapSize); }
            if (kstate.IsKeyDown(Keys.W)) { Move.PlayerMove(_player, Direction.Up, _mapSize); }

            if (mstate.LeftButton == ButtonState.Pressed) { Player.TryShoot(_player, gameTime, Content, _mousePosition, _bullets); }

            Bullet.Tick(gameTime, _allPlayers, _bullets, _damages);
            Damage.Tick(gameTime, _damages);
            Player.Tick(_allPlayers);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach (var player in _allPlayers) { _spriteBatch.Draw(player.Texture, player.Location, Color.CornflowerBlue); }
            foreach (var bullet in _bullets) { _spriteBatch.Draw(bullet.Texture, bullet.Location, Color.CornflowerBlue); }
            foreach (var damage in _damages) { _spriteBatch.DrawString(_damageF, damage.Amount.ToString(), damage.Location, Color.Red); }
            _spriteBatch.DrawString(_hpF, _player.Health.ToString(), HudVector.HpVector(_mapSize), Color.Red);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}