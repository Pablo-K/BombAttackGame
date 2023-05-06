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
        private readonly List<Rectangle> _mapCollision;
        private readonly EventProcessor _eventProcessor;
        private readonly GameTime _gameTime;
        private readonly InputHandler _inputHandler;
        private readonly DrawingProcessor _draw;
        private readonly MapManager _mapManager;
        private readonly GraphicsDeviceManager _graphics;
        private readonly GameManager _gameManager;
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
            _animations = new List<Animation>();
            _mapManager = new MapManager();
            _inputHandler = new InputHandler();
            _gameManager = new GameManager(_gameObjects);
            _eventProcessor = new EventProcessor(_gameObjects, _mapCollision, _gameTime, _sprites, _holdableObjects, _animations, _gameManager, _mapManager);
            _draw = new DrawingProcessor(_gameObjects, _sprites, _animations, _mapManager, _gameTime, _gameManager);
        }

        protected override void Initialize()
        {
            base.Window.AllowUserResizing = false;
            _graphics.PreferredBackBufferWidth = 1000;
            _graphics.PreferredBackBufferHeight = 1000;
            _graphics.ApplyChanges();
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentContainer.Initialize(base.Content);
            AnimationsContainer.Initialize(base.Content);
            _mapManager.GenerateMirage();
            _mapCollision.AddRange(_mapManager.Mirage.Rectangle);
            StartRound();
        }

        protected override void Update(GameTime gameTime)
        {
            if (_gameObjects.Where(x => x.IsHuman).Any()) _player = (Player)_gameObjects.Where(x => x.IsHuman).FirstOrDefault();
            _gameTime.TotalGameTime = gameTime.TotalGameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            _inputHandler.HandleInputs(_player);
            _gameManager.Process();
            _eventProcessor.ProcessEvents();
            Tick();
            base.Update(gameTime);
        }

        private new void Tick()
        {
            foreach (IGameObject gameObject in _gameObjects.ToList())
            {
                gameObject.Tick(_gameTime, _gameObjects, _mapManager.Mirage.Rectangle);
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
            _gameObjects.Add(GameObject.AddMainSpeed(Team.None, _mapManager));
            _gameObjects.AddRange(GameObject.AddPlayers(Team.TeamMate, _gameManager.TeamMatesCount, _mapManager));
            _gameObjects.AddRange(GameObject.AddPlayers(Team.Enemy, _gameManager.EnemyCount, _mapManager));

            foreach (var player in _gameObjects.OfType<Player>())
            {
                var gun = new Sheriff();
                var flash = new Grenade("flashgrenade");
                var nade = new Grenade("handgrenade");
                player.Inventory.InventoryItems.Add(gun);
                player.Inventory.Equip(gun);
                player.Inventory.Equip(nade);
                player.Inventory.Equip(flash);
                player.Inventory.Select(1);
                _holdableObjects.Add(gun);
            }
        }
    }
}
