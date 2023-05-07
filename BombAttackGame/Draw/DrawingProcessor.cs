using BombAttackGame.Abstracts;
using BombAttackGame.Global;
using BombAttackGame.HUD;
using BombAttackGame.Interfaces;
using BombAttackGame.Map;
using BombAttackGame.Models;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Collections.Generic;
using System.Linq;

namespace BombAttackGame.Draw
{
    internal class DrawingProcessor
    {
        private readonly List<IGameObject> _gameObjects;
        private List<Animation> _animations;
        private readonly List<IGameSprite> _sprites;
        private readonly MapManager _mapManager;
        private readonly GameTime _gameTime;
        private readonly GameManager _gameManager;
        private float _alpha = 1f;
        private Color _color = Color.Indigo;

        public DrawingProcessor(List<IGameObject> gameObjects, List<IGameSprite> sprites, List<Animation> animations, MapManager mapManager, GameTime gameTime, GameManager gameManager)
        {
            _gameObjects = gameObjects;
            _sprites = sprites;
            _mapManager = mapManager;
            _gameTime = gameTime;
            _animations = animations;
            _gameManager = gameManager;
        }
        public void Draw(SpriteBatch spriteBatch, GraphicsDevice graphicsDevice)
        {
            graphicsDevice.Clear(_color);
            spriteBatch.Begin();
            DrawGameObjects(spriteBatch);
            DrawSprites(spriteBatch);
            DrawMap(spriteBatch);
            DrawHud(spriteBatch);
            DrawAnimations(spriteBatch);
            DrawFlash(spriteBatch);
            spriteBatch.End();
        }
        private void DrawAnimations(SpriteBatch spriteBatch)
        {
            foreach (var animation in _animations.ToList())
            {
                if (_gameTime.TotalGameTime.TotalMilliseconds >= animation.LastPartTime + animation.PartTime)
                {
                    spriteBatch.Draw(animation.AnimationTexture.ElementAt(animation.ActualPart), animation.Location, Color.Black * _alpha);
                    animation.ActualPart += 1;
                    animation.LastPartTime = _gameTime.TotalGameTime.TotalMilliseconds;
                }
                if (animation.ActualPart == animation.Parts)
                {
                    _animations.Remove(animation);
                }
            }
        }
        private void DrawHud(SpriteBatch spriteBatch)
        {
            if ((Player)_gameObjects.Where(x => x.IsHuman).FirstOrDefault() == null)
            {
                spriteBatch.DrawString(ContentContainer.GameResultFont, _gameManager.CTWinRounds.ToString(), HudVector.CTWinVector(), Color.Green);
                spriteBatch.DrawString(ContentContainer.GameResultFont, _gameManager.TTWinRounds.ToString(), HudVector.TTWinVector(), Color.Red);
                spriteBatch.DrawString(ContentContainer.GameResultFont, _gameManager.RoundMinutesLeft.ToString() + ":" + _gameManager.RoundSecondsLeft, HudVector.LeftTimeVector(), Color.Red);
                return;
            }
            Player player = (Player)_gameObjects.Where(x => x.IsHuman).First();
            spriteBatch.DrawString(ContentContainer.HpFont, player.Health.ToString(), HudVector.HpVector(), Color.Green);
            spriteBatch.DrawString(ContentContainer.HpFont, player.Inventory.SelectedItem.HudDisplayName, HudVector.HoldableNameVector(), Color.Green);
            spriteBatch.DrawString(ContentContainer.GameResultFont, _gameManager.CTWinRounds.ToString(), HudVector.CTWinVector(), Color.Green);
            spriteBatch.DrawString(ContentContainer.GameResultFont, _gameManager.TTWinRounds.ToString(), HudVector.TTWinVector(), Color.Red);
            spriteBatch.DrawString(ContentContainer.GameResultFont, _gameManager.RoundMinutesLeft.ToString(), HudVector.LeftTimeVector(), Color.Red);
            spriteBatch.DrawString(ContentContainer.GameResultFont, _gameManager.RoundMinutesLeft.ToString() + ":" + _gameManager.RoundSecondsLeft, HudVector.LeftTimeVector(), Color.Red);
            if (player.Inventory.SelectedItem is Gun gun)
            {
                spriteBatch.DrawString(ContentContainer.HpFont, gun.Magazine.ToString(), HudVector.MagazineVector(), Color.FloralWhite);
                spriteBatch.DrawString(ContentContainer.HpFont, gun.Ammo.ToString(), HudVector.AmmoVector(), Color.FloralWhite);
            }

        }
        private void DrawFlash(SpriteBatch spriteBatch)
        {
            if ((Player)_gameObjects.Where(x => x.IsHuman).FirstOrDefault() == null)
            {
                _alpha = 1f;
                return;
            }
            Player player = (Player)_gameObjects.Where(x => x.IsHuman).FirstOrDefault();
            if (player.IsFlashed)
            {
                _alpha = 1f - ((player.FlashTime - (float)player.Time) / 1000); ;
            }
            else
            {
                _alpha = 1f;
                _color = Color.Indigo;
            }
        }
        private void DrawMap(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _mapManager.Mirage.WallVector.Count; i++)
            {
                spriteBatch.Draw(MapManager.Wall, _mapManager.Mirage.WallVector[i], Color.Red * _alpha);
            }
        }
        private void DrawGameObjects(SpriteBatch spriteBatch)
        {
            if ((Player)_gameObjects.Where(x => x.IsHuman).FirstOrDefault() == null)
            {
                foreach (var gameObject in _gameObjects)
                {
                    spriteBatch.Draw(gameObject.Texture, gameObject.Location, gameObject.Color * _alpha);
                }
                return;
            }

            Player player = (Player)_gameObjects.Where(x => x.IsHuman).First();
            foreach (var gameObject in _gameObjects)
            {
                if (!gameObject.IsDead && player.VisibleObjects.Contains(gameObject))
                {
                    spriteBatch.Draw(gameObject.Texture, gameObject.Location, gameObject.Color * _alpha);
                }
            }
        }
        private void DrawSprites(SpriteBatch spriteBatch)
        {
            foreach (var gameSprite in _sprites)
            {
                spriteBatch.DrawString(gameSprite.Font, gameSprite.Text, gameSprite.Location, gameSprite.Color * _alpha);
            }

        }
    }
}
