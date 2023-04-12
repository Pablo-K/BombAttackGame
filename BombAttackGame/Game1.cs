using BombAttackGame.Bonuses;
using BombAttackGame.Collisions;
using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.HUD;
using BombAttackGame.Models;
using BombAttackGame.Vector;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame
{
    public class Game1 : Game
    {
        private EventProcessor _eventProcessor;
        private SpriteFont _hpF;
        private SpriteFont _damageF;
        private MainSpeed _mainSpeed;
        private Player _player;
        private List<Player> _allPlayers;
        private List<Damage> _damages;
        private List<Bullet> _bullets;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _mousePosition;
        private Color _mainColor;
        private int[] _mapSize = new int[2];
        private int _teamMateAmount;
        private int _enemyAmount;
        private SpriteBatch _map1;
        private List<Vector2> _map1Collisions;
        private Texture2D _wall;
        private Collision _collision;
        private List<Vector2> MapCol;
        private bool isempty;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _collision = new Collision();

            _mapSize[0] = 1000;
            _mapSize[1] = 1000;

            _mainColor = Color.Tomato;

            Window.AllowUserResizing = true;

            _graphics.PreferredBackBufferWidth = _mapSize[0];
            _graphics.PreferredBackBufferHeight = _mapSize[1];
            _graphics.ApplyChanges();

            _enemyAmount = 0;
            _teamMateAmount = 0;

            base.Initialize();

            _eventProcessor = new EventProcessor(_mapSize);
        }

        protected override void LoadContent()
        {
            MapCol = new List<Vector2>();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _map1Collisions = new List<Vector2>();

            _hpF = Content.Load<SpriteFont>("hp");
            _damageF = Content.Load<SpriteFont>("damage");
            _wall = Content.Load<Texture2D>("wall");

            _mainSpeed = new MainSpeed();
            _mainSpeed.Texture = Content.Load<Texture2D>("mainSpeed");
            _mainSpeed.Location = Spawn.GenerateRandomSpawnPoint(_mapSize, _mainSpeed.Texture, _collision);
            _mainSpeed.Collision = VectorTool.Collision(_mainSpeed.Location, _mainSpeed.Texture);
            _mainSpeed.IsDead = false;

            _allPlayers = new List<Player>();

            _bullets = new List<Bullet>();
            _damages = new List<Damage>();

            _player = Player.AddPlayer(Team.Player, Content, _mapSize, _collision);
            _allPlayers.Add(_player);

            _allPlayers.AddRange(Player.AddPlayers(Team.TeamMate, Content, _teamMateAmount, _mapSize, _collision));
            _allPlayers.AddRange(Player.AddPlayers(Team.Enemy, Content, _enemyAmount, _mapSize, _collision));

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            
            _mapSize[0] = Window.ClientBounds.Width;
            _mapSize[1] = Window.ClientBounds.Height;

            _collision.AddCollision(MapCol);

            _eventProcessor.Update(_collision, gameTime, _bullets, Content);
            _bullets.AddRange(_eventProcessor.Bullets.Except(_bullets));

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            _mousePosition = mstate.Position.ToVector2();

            if (kstate.IsKeyDown(Keys.A)) { _player.PlayerMove(Direction.Left); }
            if (kstate.IsKeyDown(Keys.S)) { _player.PlayerMove(Direction.Down); }
            if (kstate.IsKeyDown(Keys.D)) { _player.PlayerMove(Direction.Right); }
            if (kstate.IsKeyDown(Keys.W)) { _player.PlayerMove(Direction.Up); }

            if (mstate.LeftButton == ButtonState.Pressed) { _player.TryShoot(_mousePosition); }

            UpdateCollision();
            Bullet.Tick(gameTime, _allPlayers, _bullets, _damages);
            //MainSpeed.Tick(gameTime, _mainSpeed, _mapSize, _collision);
            //Damage.Tick(gameTime, _damages);


            foreach (var player in _allPlayers)
            {
                switch (player.Event)
                {
                    case Event.None:
                        break;
                    case Event.Move:
                        _eventProcessor.Move(player);
                        break;
                    case Event.TryShoot:
                        _eventProcessor.TryShoot(player);
                        break;
                    case Event.Shoot:
                        break;
                    default:
                        break;
                }
                player.Tick();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_mainColor);

            _spriteBatch.Begin();

            Map1(_spriteBatch);
            foreach (var player in _allPlayers) { _spriteBatch.Draw(player.Texture, player.Location, player.Color); }
            foreach (var bullet in _bullets) { _spriteBatch.Draw(bullet.Texture, bullet.Location, _mainColor); }
            foreach (var damage in _damages) { _spriteBatch.DrawString(_damageF, damage.Amount.ToString(), damage.Location, Color.Yellow); }
            _spriteBatch.DrawString(_hpF, _player.Health.ToString(), HudVector.HpVector(_mapSize), Color.Green);
            if (!_mainSpeed.IsDead) _spriteBatch.Draw(_mainSpeed.Texture, _mainSpeed.Location, _mainColor);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void UpdateCollision()
        {
            _collision.AddCollision(_map1Collisions);
        }
        private SpriteBatch Map1(SpriteBatch SpriteBatch)
        {
            isempty = MapCol.Count <= 0;
            for (int i = 0; i < 1000; i += 20)
            {
                DrawWall(SpriteBatch, new Vector2(0, i));
                DrawWall(SpriteBatch, new Vector2(980, i));
                DrawWall(SpriteBatch, new Vector2(i, 0));
                DrawWall(SpriteBatch, new Vector2(i, 980));
                if (isempty)
                {
                    //MapCol = MapCol.Except(_map1Collisions).ToList();
                    //MapCol.AddRange(VectorTool.Collision(new Vector2(0,i+1), _wall));
                    //MapCol.AddRange(VectorTool.Collision(new Vector2(980,i+1), _wall));
                    //MapCol.AddRange(VectorTool.Collision(new Vector2(i,1), _wall));
                    //MapCol.AddRange(VectorTool.Collision(new Vector2(i,981), _wall));
                    //_map1Collisions.AddRange(MapCol);
                }
            }
            isempty = false;
            return SpriteBatch;
        }
        private SpriteBatch DrawWall(SpriteBatch SpriteBatch, Vector2 Position)
        {
            SpriteBatch.Draw(
                texture: _wall,
                position: Position,
                sourceRectangle: null,
                color: Color.BlanchedAlmond,
                rotation: 0f,
                origin: Vector2.Zero,
                scale: new Vector2(1, 1),
                effects: SpriteEffects.None,
                layerDepth: 0f);
            return SpriteBatch;
        }
    }
}