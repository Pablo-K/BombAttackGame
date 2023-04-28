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
        private readonly List<IGameSprite> _sprites;
        private readonly MapManager _mapManager;

        public DrawingProcessor(List<IGameObject> gameObjects, List<IGameSprite> sprites, MapManager mapManager)
        {
            _gameObjects = gameObjects;
            _sprites = sprites;
            _mapManager = mapManager;
        }
        public void Draw(SpriteBatch spriteBatch)
        {
            DrawGameObjects(spriteBatch);
            DrawSprites(spriteBatch);
            DrawMap(spriteBatch);
            DrawHud(spriteBatch);
        }
        private void DrawHud(SpriteBatch spriteBatch)
        {
            Player player = (Player)_gameObjects.ElementAt(0);
            spriteBatch.DrawString(ContentContainer.HpFont, player.Health.ToString(), HudVector.HpVector(), Color.Green);
            spriteBatch.Draw(player.Inventory.SelectedItem.HudTexture, player.Inventory.SelectedItem.HudPosition, Color.FloralWhite);
            if (player.Inventory.SelectedItem is Gun gun)
            {
                spriteBatch.DrawString(ContentContainer.HpFont, gun.Magazine.ToString(), HudVector.MagazineVector(), Color.FloralWhite);
                spriteBatch.DrawString(ContentContainer.HpFont, gun.Ammo.ToString(), HudVector.AmmoVector(), Color.FloralWhite);
            }

        }
        private void DrawMap(SpriteBatch spriteBatch)
        {
            for (int i = 0; i < _mapManager.Mirage.WallVector.Count; i++)
            {
                spriteBatch.Draw(MapManager.Wall, _mapManager.Mirage.WallVector[i], Color.Red);
            }
        }
        private void DrawGameObjects(SpriteBatch spriteBatch)
        {
            Player player = (Player)_gameObjects.ElementAt(0);

            foreach (var gameObject in _gameObjects)
            {
                if (!gameObject.IsDead && player.VisibleObjects.Contains(gameObject))
                {
                    spriteBatch.Draw(gameObject.Texture, gameObject.Location, gameObject.Color);
                }
            }
        }
        private void DrawSprites(SpriteBatch spriteBatch)
        {
            foreach (var gameSprite in _sprites)
            {
                spriteBatch.DrawString(gameSprite.Font, gameSprite.Text, gameSprite.Location, gameSprite.Color);
            }

        }
    }
}
