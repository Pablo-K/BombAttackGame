using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Global;
using BombAttackGame.HUD;
using BombAttackGame.Interfaces;
using BombAttackGame.Models;
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
        private Player _player;
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;
        private Vector2 _mousePosition;
        private Color _mainColor;
        private int[] _mapSize = new int[2];
        private int _teamMateAmount;
        private int _enemyAmount;
        private SpriteBatch _map1;
        private Texture2D _wall;
        private SpriteFont _damageF;
        private List<Rectangle> _mapCollision;
        private bool isempty;
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

            Window.AllowUserResizing = true;

            _graphics.PreferredBackBufferWidth = _mapSize[0];
            _graphics.PreferredBackBufferHeight = _mapSize[1];
            _graphics.ApplyChanges();

            _enemyAmount = 1;
            _teamMateAmount = 0;

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

            _gameObjects.Add(GameObject.AddMainSpeed(_mapSize, Content, _mapCollision));

            _player = GameObject.AddPlayer(Team.Player, Content, _mapSize, _mapCollision);
            _gameObjects.Add(_player);

            _gameObjects.AddRange(GameObject.AddPlayers(Team.TeamMate, Content, _teamMateAmount, _mapSize, _mapCollision));
            _gameObjects.AddRange(GameObject.AddPlayers(Team.Enemy, Content, _enemyAmount, _mapSize, _mapCollision));

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

            foreach (IGameObject GameObject in _gameObjects.OfType<Player>().ToList()) { GameObject.Tick(gameTime, _gameObjects); }
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

            foreach (IGameObject GameObject in _gameObjects.OfType<Bullet>().ToList()) { GameObject.Tick(gameTime, _gameObjects); }
            foreach (IGameSprite GameSprite in _gameSprites.ToList()) { GameSprite.Tick(gameTime, _gameSprites); } 

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(_mainColor);

            _spriteBatch.Begin();

            Map1(_spriteBatch);

            foreach (var gameObject in _gameObjects) { if (!gameObject.IsDead) { _spriteBatch.Draw(gameObject.Texture, gameObject.Location, gameObject.Color); } }
            foreach (var gameSprite in _gameSprites) { _spriteBatch.DrawString(gameSprite.Font, gameSprite.Text, gameSprite.Location, gameSprite.Color); }

            _spriteBatch.DrawString(_hpF, _player.Health.ToString(), HudVector.HpVector(_mapSize), Color.Green);

            _spriteBatch.End();

            base.Draw(gameTime);
        }
        private void UpdateCollision()
        {
        }
        private SpriteBatch Map1(SpriteBatch SpriteBatch)
        {
            isempty = _mapCollision.Count <= 0; 
            for (int i = 0; i < 1000; i += 20)
            {
                DrawWall(SpriteBatch, new Vector2(0, i));
                DrawWall(SpriteBatch, new Vector2(980, i));
                DrawWall(SpriteBatch, new Vector2(i, 0));
                DrawWall(SpriteBatch, new Vector2(i, 980));
                if (isempty)
                {
                    _mapCollision.Add(new Rectangle(0,i,20,20));
                    _mapCollision.Add(new Rectangle(980,i,20,20));
                    _mapCollision.Add(new Rectangle(i,0,20,20));
                    _mapCollision.Add(new Rectangle(i,980,20,20));
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
        private void CheckAllPlayersEvent()
        {
            Bullet Bullet;
            Bullet = null;
            foreach (var player in _gameObjects.OfType<Player>())
            {
                for (int i = 0; i <player.Event.Count; i++)
                {
                    switch (player.Event[i])
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
                switch(bullet.Event)
                {
                    case Event.None:
                        break;
                    case Event.ObjectHitted:
                        if (bullet.ObjectHitted.GetType() == typeof(Player)) { 
                            DealDamage.DealDamageToPlayer(bullet.ObjectHitted as Player, bullet); 
                            CreateDamage(bullet, GameTime); 
                            RemoveBullet(bullet); };
                        break;
                }
            }
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