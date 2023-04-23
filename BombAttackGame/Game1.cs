using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Global;
using BombAttackGame.HUD;
using BombAttackGame.Interfaces;
using BombAttackGame.Map;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Threading;

namespace BombAttackGame
{
    public class Game1 : Game
    {
        private MapManager _mapManager;
        private EventProcessor _eventProcessor;
        private SpriteFont _hpF;
        private Player _player;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _mousePosition;
        private Color _mainColor;
        private int[] _mapSize = new int[2];
        private int _teamMateAmount;
        private int _enemyAmount;
        private Texture2D _wall;
        private SpriteFont _damageF;
        private List<Rectangle> _mapCollision;
        public List<IGameObject> _gameObjects { get; private set; }
        public List<IGameSprite> _gameSprites { get; private set; }

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            _gameObjects = new List<IGameObject>();
            _gameSprites = new List<IGameSprite>();

            _mapSize[0] = 1000;
            _mapSize[1] = 1000;

            _mainColor = Color.Tomato;

            Window.AllowUserResizing = false;

            _graphics.PreferredBackBufferWidth = _mapSize[0];
            _graphics.PreferredBackBufferHeight = _mapSize[1];
            _graphics.ApplyChanges();

            _enemyAmount = 5;
            _teamMateAmount = 4;

            base.Initialize();

            _eventProcessor = new EventProcessor(_mapSize);
        }

        protected override void LoadContent()
        {
            _mapCollision = new List<Rectangle>();
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            _hpF = Content.Load<SpriteFont>("hp");
            _wall = Content.Load<Texture2D>("wall");
            _damageF = Content.Load<SpriteFont>("damage");

            _mapManager = new MapManager(_spriteBatch,_wall);
            CreateMapCollision();

            _gameObjects.Add(GameObject.AddMainSpeed(_mapSize, Content, Team.None, _mapManager));

            _player = GameObject.AddPlayer(Team.TeamMate, Content, _mapSize, _mapManager);
            _gameObjects.Add(_player);
            _player.Human = true;

            _gameObjects.AddRange(GameObject.AddPlayers(Team.TeamMate, Content, _teamMateAmount, _mapSize, _mapManager));
            _gameObjects.AddRange(GameObject.AddPlayers(Team.Enemy, Content, _enemyAmount, _mapSize, _mapManager));
            
        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();

            _mapSize[0] = Window.ClientBounds.Width;
            _mapSize[1] = Window.ClientBounds.Height;

            _eventProcessor.Update(_mapCollision, gameTime, _gameObjects, Content);
            _gameObjects.AddRange(_eventProcessor.GameObjects.OfType<Bullet>().Except(_gameObjects.OfType<Bullet>()));

            var kstate = Keyboard.GetState();
            var mstate = Mouse.GetState();

            _mousePosition = mstate.Position.ToVector2();

            foreach (IGameObject GameObject in _gameObjects.OfType<Player>().ToList()) { GameObject.Tick(gameTime, _gameObjects, _mapManager.Mirage.Rectangle); }
            if (kstate.IsKeyDown(Keys.A)) { _player.PlayerMove(Direction.Left); }
            if (kstate.IsKeyDown(Keys.S)) { _player.PlayerMove(Direction.Down); }
            if (kstate.IsKeyDown(Keys.D)) { _player.PlayerMove(Direction.Right); }
            if (kstate.IsKeyDown(Keys.W)) { _player.PlayerMove(Direction.Up); }
            if (kstate.IsKeyDown(Keys.A) && kstate.IsKeyDown(Keys.W)) { _player.PlayerMove(Direction.UpLeft); }
            if (kstate.IsKeyDown(Keys.A) && kstate.IsKeyDown(Keys.S)) { _player.PlayerMove(Direction.DownLeft); }
            if (kstate.IsKeyDown(Keys.D) && kstate.IsKeyDown(Keys.W)) { _player.PlayerMove(Direction.UpRight); }
            if (kstate.IsKeyDown(Keys.D) && kstate.IsKeyDown(Keys.S)) { _player.PlayerMove(Direction.DownRight); }

            if (mstate.LeftButton == ButtonState.Pressed) { _player.TryShoot(_mousePosition); }

            UpdateCollision();

            CheckAllBulletsEvent(gameTime);
            CheckAllPlayersEvent();

            foreach (IGameObject GameObject in _gameObjects.OfType<Bullet>().ToList()) { GameObject.Tick(gameTime, _gameObjects, _mapManager.Mirage.Rectangle); }
            foreach (IGameSprite GameSprite in _gameSprites.ToList()) { GameSprite.Tick(gameTime, _gameSprites); }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_mainColor);

            _spriteBatch.Begin();
            foreach (var gameObject in _gameObjects) { if (!gameObject.IsDead && _player.VisibleObjects.Contains(gameObject)) { _spriteBatch.Draw(gameObject.Texture, gameObject.Location, gameObject.Color); } }
            foreach (var gameSprite in _gameSprites) { _spriteBatch.DrawString(gameSprite.Font, gameSprite.Text, gameSprite.Location, gameSprite.Color); }
            for (int i = 0; i < _mapManager.Mirage.WallVector.Count; i++)
            { _spriteBatch.Draw(MapManager.Wall, _mapManager.Mirage.WallVector[i], Color.Red); }

            _spriteBatch.DrawString(_hpF, _player.Health.ToString(), HudVector.HpVector(_mapSize), Color.Green);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void UpdateCollision()
        {
        }
        private void CreateMapCollision()
        {
            AddCollisions(_mapManager.Mirage.Rectangle);
        }
        private void AddCollisions(List<Rectangle> Collision)
        {
            _mapCollision.AddRange(Collision);
        }
        private void CheckAllPlayersEvent()
        {
            Bullet Bullet;
            Bullet = null;
            foreach (var player in _gameObjects.OfType<Player>())
            {
                while (player.Event.TryDequeue(out Event result))
                {
                    switch (result)
                    {
                        case Event.None:
                            break;
                        case Event.Move:
                            _eventProcessor.Move(player);
                            break;
                        case Event.TryShoot:
                            _eventProcessor.TryShoot(player, out Bullet);
                            break;
                        case Event.Shoot:
                            break;
                        default:
                            break;
                    }
                }
            }
            CheckBullet(Bullet);
        }
        private void CheckAllBulletsEvent(GameTime GameTime)
        {
            foreach (var bullet in _gameObjects.OfType<Bullet>().ToList())
            {
                while (bullet.Event.TryDequeue(out Event result))
                    switch (result)
                    {
                        case Event.Move:
                            _eventProcessor.Move(bullet);
                            break;
                        case Event.ObjectHitted:
                            if (bullet.ObjectHitted?.GetType() == typeof(Player))
                            {
                                DealDamage.DealDamageToPlayer(bullet.ObjectHitted as Player, bullet.DamageDealt);
                                CreateDamage(bullet, GameTime);
                                RemoveBullet(bullet);
                            }
                            else
                            {
                                RemoveBullet(bullet);
                            }
                            break;
                        case Event.Delete:
                            DeleteObject(bullet);
                            break;

                    }
            }
        }


        private void DeleteObject(IGameObject GameObject)
        {
            _gameObjects.Remove(GameObject);
        }

        private void CheckBullet(Bullet Bullet)
        {
            if (Bullet is null) return;
            _gameObjects.Add(Bullet);
        }
        private void RemoveBullet(Bullet Bullet)
        {
            _gameObjects.Remove(Bullet);
        }
        private void CreateDamage(Bullet Bullet, GameTime GameTime)
        {
            Damage Damage = new Damage(Bullet.DamageDealt, Bullet.Location);
            Damage.Font = _damageF;
            Damage.Location = Bullet.Location;
            Damage.ShowTime = GameTime.TotalGameTime.TotalMilliseconds;
            _gameSprites.Add(Damage);
        }
    }
}