using BombAttackGame.Draw;
using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using BombAttackGame.Map;
using BombAttackGame.Models;
using BombAttackGame.Models.HoldableObjects;
using BombAttackGame.Models.HoldableObjects.ThrowableObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame
{

    public class BombAttackGame : Game
    {
        private readonly List<IGameObject> _gameObjects;
        private readonly List<IGameSprite> _sprites;
        private readonly List<IHoldableObject> _holdableObjects;
        private readonly List<IOnGroundItem> _onGroundItems;
        private readonly List<Rectangle> _mapCollision;
        private readonly EventProcessor _eventProcessor;
        private readonly GameTime _gameTime;
        private readonly InputHandler _inputHandler;
        private readonly DrawingProcessor _draw;
        private readonly MapManager _mapManager;
        private readonly GraphicsDeviceManager _graphics;
        private readonly GameManager _gameManager;
        private BotPoints _botPoints;
        private List<Animation> _animations;
        private Player _player;
        private SpriteBatch _spriteBatch;
        private (int Width, int Height) _mapSize;

        public BombAttackGame()
        {
            base.Content.RootDirectory = "Content";
            base.IsMouseVisible = true;
            _mapSize.Width = 1000;
            _mapSize.Height = 1000;
            _graphics = new GraphicsDeviceManager(this);
            _gameObjects = new List<IGameObject>();
            _sprites = new List<IGameSprite>();
            _holdableObjects = new List<IHoldableObject>();
            _mapCollision = new List<Rectangle>();
            _gameTime = new GameTime();
            _holdableObjects = new List<IHoldableObject>();
            _onGroundItems = new List<IOnGroundItem>();
            _animations = new List<Animation>();
            _mapManager = new MapManager();
            _inputHandler = new InputHandler();
            _gameManager = new GameManager(_gameObjects);
            _eventProcessor = new EventProcessor(_gameObjects, _mapCollision, _gameTime, _sprites, _holdableObjects, _animations, _gameManager, _mapManager, _onGroundItems);
            _draw = new DrawingProcessor(_gameObjects, _sprites, _animations, _mapManager, _gameTime, _gameManager,_onGroundItems);
        }

        protected override void Initialize()
        {

            base.Window.AllowUserResizing = false;
            _graphics.PreferredBackBufferWidth = _mapSize.Width;
            _graphics.PreferredBackBufferHeight = _mapSize.Height;
            _graphics.ApplyChanges();
            base.Initialize();

        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentContainer.Initialize(base.Content);
            _mapManager.LoadMap();
            AnimationsContainer.Initialize(base.Content);
            _mapCollision.AddRange(_mapManager.MapCollisions);
            _botPoints = new BotPoints();
            StartRound();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_gameManager.RoundStartedTime == 0) _gameManager.SetTime(gameTime);
            if (_gameObjects.Where(x => x.IsHuman).Any()) _player = (Player)_gameObjects.Where(x => x.IsHuman).FirstOrDefault();
            if (_gameManager.IsOnTimeLapse)
            {
                var x = gameTime.ElapsedGameTime.TotalMilliseconds;
                gameTime.TotalGameTime += new TimeSpan((long)x * 15000);
            }
            _gameTime.TotalGameTime = gameTime.TotalGameTime;
            _gameTime.ElapsedGameTime = gameTime.ElapsedGameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            _inputHandler.HandleInputs(_player);
            _eventProcessor.ProcessEvents();
            if (_onGroundItems.Where(x => x is Bomb).Any())
            {
                Bomb b = (Bomb)_onGroundItems.Where(x => x is Bomb).FirstOrDefault();
                if (b.Planted ) _gameManager.UpdateTime(gameTime, b.BoomTime);
                else { _gameManager.UpdateTime(gameTime); }
            }
            else { _gameManager.UpdateTime(gameTime); }
            _gameManager.Process();
            Tick();
            base.Update(gameTime);
        }

        private new void Tick()
        {
            foreach (IGameObject gameObject in _gameObjects.ToList())
            {
                gameObject.Tick(_gameTime, _gameObjects, _mapManager.MapCollisions);
            }
            foreach (IGameSprite gameSprite in _sprites.ToList())
            {
                gameSprite.Tick(_gameTime, _sprites);
            }
        }

        protected override void Draw(GameTime gameTime)
        {
            _draw.Draw(_spriteBatch, GraphicsDevice);
            base.Draw(gameTime);
        }
        private void StartRound()
        {
            this._holdableObjects.Clear();
            this._gameObjects.Clear();
            _player = GameObject.AddPlayer(Team.TeamMate, _mapManager);
            _gameObjects.Add(_player);
            _player.IsHuman = true;


            _player.Color = Color.Tomato;
            _gameObjects.AddRange(GameObject.AddPlayers(Team.TeamMate, _gameManager.TeamMatesCount, _mapManager));
            _gameObjects.AddRange(GameObject.AddPlayers(Team.Enemy, _gameManager.EnemyCount, _mapManager));

            foreach (var player in _gameObjects.OfType<Player>())
            {
                var gun = new Sheriff();
                player.Inventory.InventoryItems.Add(gun);
                player.Inventory.Equip(gun);
                player.Inventory.Equip(new FlashGrenade(player));
                player.Inventory.Equip(new HandGrenade(player));
                player.Inventory.Select(1);
                _holdableObjects.Add(gun);
            }
            List<Player> tplayers = new List<Player>();
            tplayers.AddRange(this._gameObjects.OfType<Player>().Where(x => x.Team == Team.Enemy));
            int r = new Random().Next(0, tplayers.Count);
            tplayers.ElementAt(r).Inventory.Equip(new Bomb());
        }
    }
}
