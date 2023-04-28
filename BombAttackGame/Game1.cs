using BombAttackGame.Draw;
using BombAttackGame.Enums;
using BombAttackGame.Events;
using BombAttackGame.Global;
using BombAttackGame.Interfaces;
using BombAttackGame.Map;
using BombAttackGame.Models;
using BombAttackGame.Models.HoldableObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame
{

    public class Game1 : Game
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
        private Player _player;
        private SpriteBatch _spriteBatch;
        private int _teamMateAmount;
        private int _enemyAmount;
        private (int Width, int Height) _mapSize;

        public Game1()
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
            _mapManager = new MapManager();
            _inputHandler = new InputHandler();
            _eventProcessor = new EventProcessor(_gameObjects, _mapCollision, _gameTime, _sprites, _holdableObjects);
            _draw = new DrawingProcessor(_gameObjects, _sprites, _mapManager);
        }

        protected override void Initialize()
        {
            base.Window.AllowUserResizing = false;
            _graphics.PreferredBackBufferWidth = _mapSize.Width;
            _graphics.PreferredBackBufferHeight = _mapSize.Height; 
            _graphics.ApplyChanges();
            _enemyAmount = 5;
            _teamMateAmount = 4;
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            ContentContainer.Initialize(base.Content);
            _mapManager.GenerateMirage();
            _mapCollision.AddRange(_mapManager.Mirage.Rectangle);
            _player = GameObject.AddPlayer(Team.TeamMate, _mapSize, _mapManager);
            _gameObjects.Add(_player);
            _player.IsHuman = true;
            _player.Color = Color.Tomato;
            _gameObjects.Add(GameObject.AddMainSpeed(Team.None, _mapManager));
            _gameObjects.AddRange(GameObject.AddPlayers(Team.TeamMate, _teamMateAmount, _mapManager));
            _gameObjects.AddRange(GameObject.AddPlayers(Team.Enemy, _enemyAmount, _mapManager));

            foreach (var player in _gameObjects.OfType<Player>())
            {
                var gun = new Sheriff();
                player.Inventory.InventoryItems.Add(gun);
                player.Inventory.Equip(gun);
                player.Inventory.Select(1);
                _holdableObjects.Add(gun);
            }

        }

        protected override void Update(GameTime gameTime)
        {
            _gameTime.TotalGameTime = gameTime.TotalGameTime;
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape)) Exit();
            _inputHandler.HandleInputs(_player);
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
            GraphicsDevice.Clear(Color.Indigo);
            _spriteBatch.Begin();
            _draw.Draw(_spriteBatch);

            _spriteBatch.End();
            base.Draw(gameTime);
        }

    }
}
